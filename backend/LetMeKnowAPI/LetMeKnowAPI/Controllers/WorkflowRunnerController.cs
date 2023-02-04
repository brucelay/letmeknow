using Microsoft.AspNetCore.Mvc;

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
        
        
        
    }
}