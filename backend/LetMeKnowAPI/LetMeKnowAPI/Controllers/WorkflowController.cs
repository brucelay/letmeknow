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
    
    [HttpPost("CreateWorkFlow")]
    public async Task<OkResult> RunWorkflow([FromBody] dynamic jsonData)
    {
        JArray data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());
        var workFlowRunner = new WorkflowRunner(data);
        await workFlowRunner.Execute();
        return Ok();
    }
    
    [HttpPost("CreateEvent")]
    public Task<OkResult> CreateNewWorkFlowEvent([FromBody] dynamic jsonData, float freqInMins, int amount)
    {
        JArray data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());
        var workflowRunner = new WorkflowRunner(data);
        ScheduleWorkFlow(workflowRunner, TimeSpan.FromMinutes(freqInMins), amount);
        return Task.FromResult(Ok());
    }
    private async void ScheduleWorkFlow(WorkflowRunner workflow, TimeSpan interval, int amountOfTimes)
    {
        for (int i = 0; i < amountOfTimes; i++)
        {
            await Task.Delay(interval);
            await workflow.Execute();
        }
    }
}