using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
using LetMeKnowAPI.Controllers;
using Newtonsoft.Json.Linq;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.SharedModels;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;
using Twilio.Types;

namespace LetMeKnowAPI.Models;

public class WorkflowRunner
{
    private JArray _functions;
    private Stack<dynamic> _varibles;
    private Stack<dynamic?> _urls;


    public WorkflowRunner(JArray functions)
    {
        _functions = functions;
        _varibles = new Stack<dynamic>();
        _urls = new Stack<dynamic?>();
    }

    public async Task Execute()
    {
        foreach (var function in _functions)
        {
            var functionName = function["function"].ToString();
            var options = function["options"];
            Console.WriteLine("Exicuting function:" + functionName + "\nWith options: " + options);
            await Interpreter(functionName, options);
        }
    }

    private async Task Interpreter(string functionName, JToken options)
    {
        switch (functionName)
        {
            case "fetch":
                var url = options["url"]?.ToString();
                Console.WriteLine("URL:" + url);
                _urls.Push(url);
                var result = await GetContentFromUrl(url);
                _varibles.Push(result);
                break;
            case "summarise":
                var MaxTokens = int.Parse(options["maxtokens"].ToString());
                var text = _urls.Pop()?.ToString();
                var summarisedText = await SummariseText(text, MaxTokens);
                Console.WriteLine("SummerisedText: " + summarisedText);
                _varibles.Push(summarisedText);
                break;
            case "text":
                Console.WriteLine("Texting Number");
                var number = options["number"]?.ToString();
                var message = _varibles.Pop()?.ToString();
                var textResult = sendText(message, number);
                Console.WriteLine(textResult);
                // call text message
                break;
            case "if":
                Console.WriteLine("ConditionalStatement");
                var predicate = options["predicate"];
                var predFunctionName = predicate["function"].ToString();
                var predOptions = predicate["options"];
                var predResult = evalPredicate(predFunctionName, predOptions);
                if (predResult)
                {
                    var functionOne = options["function1"]?.ToString();
                    var optionsOne = options["options1"];
                    await Interpreter(functionOne, optionsOne);
                }
                else
                {
                    var functionTwo = options["function2"]?.ToString();
                    var optionsTwo = options["options2"];
                    await Interpreter(functionTwo, optionsTwo);
                }
                break;
            case "queueMessage":
                Console.WriteLine("Queueing Message");
                var messageToBeQueued = options["message"].ToString();
                _varibles.Push(messageToBeQueued);
                break;
            case "regexCheck":
                Console.WriteLine("Comparing with regex");
                var regexStr = options["regex"].ToString();
                compairWithRegex(regexStr);
                break;
        }
    }

    private void compairWithRegex(string regexStr)
    {
        var strToBeChecked = _varibles.Pop()?.ToString();
        MatchCollection matches = Regex.Matches(strToBeChecked, regexStr);
        var output = "";
        foreach (Match match in matches)
        {
            output += match.Value + "\n";
        }
        _varibles.Push(output);
    }

    public async Task<string> GetContentFromUrl(string url)
    {
        using (var client = new HttpClient())
        {
            string content = await client.GetStringAsync(url);
            var output = StripHtml(content);
            Console.WriteLine(output.Substring(0,100));
            return output;
        }
        
        string StripHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }

    private bool evalPredicate(string functionName, JToken options)
    {
        Console.WriteLine("Evaluating Predicate: " + functionName + ":" + options);
        switch (functionName)
        {
            case "contains":
                Console.WriteLine("Predicate is a contains predicate");
                var compareStr = options["compare"]?.ToString();
                var result =  Contains(compareStr, _varibles.Peek().ToString());
                Console.WriteLine(result);
                return result;
            default:
                return false;
        }
    }

    public void addStrToStack(string str)
    {
        _varibles.Push(str);
    }
    
    private static bool Contains(string compareStr, string body)
    {
        Console.WriteLine("===== Contains Method =====");
        if (body.Length > 100)
        {
            Console.WriteLine("CompairStr = "  + compareStr + "\nbody = " + body.Substring(0,100));
        }
        else
        {
            Console.WriteLine("CompairStr = "  + compareStr + "\nbody = " + body);
        }
        var result = body.Contains(compareStr);
        Console.WriteLine("contains result = " + result);
        return result;
    }

    public bool sendText(string TextMessage, string number)
    {
        Console.WriteLine("Text To Be Sent: " + TextMessage);
        var messages = SplitString(TextMessage);
        
        var SID = Environment.GetEnvironmentVariable("AccountSID");
        var AuthToken = Environment.GetEnvironmentVariable("AuthToken");
        var outgoingPhoneNumber = Environment.GetEnvironmentVariable("TwilioNumber");
        
        Console.WriteLine(SID + " : " + AuthToken + ":" + outgoingPhoneNumber);
        
        Console.WriteLine(TextMessage);
        TwilioClient.Init(SID, AuthToken);
        foreach (var messageToSend in messages)
        {
            var message = MessageResource.Create(
                to: new PhoneNumber(number), // Replace with your recipient's phone number
                from: new PhoneNumber(outgoingPhoneNumber), // Replace with a Twilio-provided phone number
                body: messageToSend);   
        }
        
        return true;
    }

    public async Task<string> SummariseText(string url, int charLimit)
    {
        // Console.WriteLine("INput Text: " + input);
        var input = "please summarise this article at the following url: " + url;
        var key = Environment.GetEnvironmentVariable("APIKey");

        var openAiService = new OpenAIService(new OpenAiOptions()
        {
            ApiKey =  key
        });
        
        var completionResult = await openAiService.Completions.CreateCompletion(new CompletionCreateRequest()
        {
            Prompt = input,
            Model = OpenAI.GPT3.ObjectModels.Models.TextDavinciV3,
            MaxTokens = charLimit
        });
        
        Console.WriteLine("Result: " + completionResult.Choices.FirstOrDefault());

        return completionResult.Choices.FirstOrDefault().Text;
    }


    public static string[] SplitString(string input)
    {
        Console.WriteLine(input);
        const int maxLength = 1500;
        var result = new List<string>();
        for (int i = 0; i < input.Length; i += maxLength)
        {
            result.Add(input.Substring(i, Math.Min(maxLength, input.Length - i)));
        }
        return result.ToArray();
    }
    
    private static string StripHtmlCssJs(string input)
    {
        input = Regex.Replace(input, "<style.*?</style>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        input = Regex.Replace(input, "<script.*?</script>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        input = Regex.Replace(input, "<.*?>", string.Empty);
        return input;
    }
    
    private static string RemoveLineBreaks(string input)
    {
        return input.Replace("\r", string.Empty).Replace("\n", string.Empty);
    }
}