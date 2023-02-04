namespace LetMeKnowAPI.Models;

public interface IWorkflowFunction<T>
{
    void RunFunction();
    T GetResult();
}