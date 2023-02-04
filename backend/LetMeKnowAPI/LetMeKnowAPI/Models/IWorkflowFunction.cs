using LetMeKnowAPI.Models.Functions;

namespace LetMeKnowAPI.Models;

public interface IWorkflowFunction<T>
{
    Task RunFunction();
    TextResult GetResult();
}