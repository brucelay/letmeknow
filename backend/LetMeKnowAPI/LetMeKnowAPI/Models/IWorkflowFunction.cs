namespace LetMeKnowAPI.Models;

public interface IWorkflowFunction<T>
{
    Task RunFunction();
    string GetResult();
}