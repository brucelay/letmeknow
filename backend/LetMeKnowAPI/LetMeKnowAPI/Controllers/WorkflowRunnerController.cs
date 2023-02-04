using System.Net;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.SharedModels;


namespace LetMeKnowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkflowRunnerController
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private static HttpClient _client;


        public WorkflowRunnerController(ILogger<WeatherForecastController> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
         
        }


        [HttpPost ("Text", Name = "TextUser")]
        public HttpStatusCode Text(string targetPhoneNumber, string outgoingPhoneNumber, string SID, string AuthToken, string OutMessage)
        {
            TwilioClient.Init(SID, AuthToken);
            
            var message = MessageResource.Create(
                to: new PhoneNumber(targetPhoneNumber), // Replace with your recipient's phone number
                from: new PhoneNumber(outgoingPhoneNumber), // Replace with a Twilio-provided phone number
                body: OutMessage);
            return HttpStatusCode.OK;
        }
        
        [HttpGet("Scrape", Name = "ScrapeFromURl")]
        public static async Task<string> GetContentFromUrl(string url)
        {
            string content = string.Empty;

            try
            {
                using (var response = await _client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        content = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting content from URL: " + ex.Message);
            }

            return content;
        }

        [HttpGet("OpenAi", Name = "Summerise")]
        public static async Task<ChoiceResponse?> Summerise(String input, string key)
        {
            Console.WriteLine(input);
            input += "\nTl;dr";
            var openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey =  key
            });

            var completionResult = await openAiService.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = input,
                Model = OpenAI.GPT3.ObjectModels.Models.TextDavinciV3,
                MaxTokens = 200
            });
            if (completionResult.Successful)
            {
                var respone = completionResult.Choices.FirstOrDefault();
                Console.WriteLine(completionResult.Choices.FirstOrDefault());
                return respone;
            }
            else
            {
                if (completionResult.Error == null)
                {
                    throw new Exception("Unknown Error");
                }
                Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
                return null;
            }
        }
    }
}