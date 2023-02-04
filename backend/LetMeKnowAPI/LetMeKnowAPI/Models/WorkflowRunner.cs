using Newtonsoft.Json.Linq;

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

    public void Execute()
    {
        foreach (var function in _functions)
        {
            var functionName = function["function"].ToString();
            var options = function["options"];
            Execute(functionName, options);
        }
    }

    public void Execute(string functionName, JToken options)
    {
        switch (functionName)
        {
            
        }
    }
}