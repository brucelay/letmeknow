using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace LetMeKnowAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkflowController : ControllerBase
{
    [HttpPost("run")]
    public Boolean RunWorkflow([FromBody] dynamic jsonData)
    {
        JArray data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());
        foreach (var entry  in data)
        {
            Console.WriteLine(entry);
        }
        return true;
    }
}