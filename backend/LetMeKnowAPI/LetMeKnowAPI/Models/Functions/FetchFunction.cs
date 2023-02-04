namespace LetMeKnowAPI.Models.Functions;

public class FetchFunction : IWorkflowFunction<IFetchResult>
{
    private IResult<IFetchResult> result;

    public void RunFunction()
    {
        this.result = new FetchResult("data");
    }

    public IResult<IFetchResult> GetResult()
    {
        return this.result;
    }
}

class FetchResult : IResult<IFetchResult>
{
    private IFetchResult fetchResult;

    public FetchResult(string data)
    {
        this.fetchResult.Data = data;
    }
    
    public IFetchResult GetResult()
    {
        return fetchResult;
    }
}