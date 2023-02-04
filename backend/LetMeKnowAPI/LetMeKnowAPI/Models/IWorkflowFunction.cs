namespace LetMeKnowAPI.Models;

public interface IWorkflowFunction<T>
{
    void RunFunction();
    IResult<T> GetResult();
}