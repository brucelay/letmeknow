using System.Net;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace LetMeKnowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkflowRunnerController
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WorkflowRunnerController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public HttpStatusCode Text(string targetPhoneNumber, string outgoingPhoneNumber, string SID, string AuthToken, string OutMessage)
        {
            TwilioClient.Init(SID, AuthToken);
            
            var message = MessageResource.Create(
                to: new PhoneNumber(targetPhoneNumber), // Replace with your recipient's phone number
                from: new PhoneNumber(outgoingPhoneNumber), // Replace with a Twilio-provided phone number
                body: OutMessage);
            return HttpStatusCode.OK;
        }
    }
}