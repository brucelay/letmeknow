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


    public WorkflowRunner(JArray functions)
    {
        _functions = functions;
        _varibles = new Stack<dynamic>();
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
                //var result = await getContentFromURL(url);
                //Console.WriteLine(result);
                _varibles.Push(url);
                break;
            case "summarise":
                var MaxTokens = int.Parse(options["maxtokens"].ToString());
                var text = _varibles.Pop()?.ToString();
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
        }
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