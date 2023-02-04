using System.Net;
using LetMeKnowAPI.Models;
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
    public async Task<bool> RunWorkflow([FromBody] dynamic jsonData)
    {
        JArray data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());
        var workFlowRunner = new WorkflowRunner(data);
        await workFlowRunner.Execute();
        return true;
    }
}