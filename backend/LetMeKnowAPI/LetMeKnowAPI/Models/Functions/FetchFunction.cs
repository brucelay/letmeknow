using System.ComponentModel.DataAnnotations;

namespace LetMeKnowAPI.Models.Functions;

public class FetchFunction : IWorkflowFunction<IFetchResult>
{
    private IResult<IFetchResult> _result;

    private readonly string _url;
    private readonly HttpClient _httpClient;

    public FetchFunction(String url, HttpClient httpClient)
    {
        _url = url;
        _httpClient = httpClient;
    }
    
    public async void RunFunction()
    {
        var content = string.Empty;
        
        try
        {
            using var response = await _httpClient.GetAsync(_url);
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error getting content from URL: " + ex.Message);
        }

        this._result = new FetchResult(content);
    }

    public IResult<IFetchResult> GetResult()
    {
        return this._result;
    }
}

class FetchResult : IResult<IFetchResult>
{
    private IFetchResult fetchResult;

    public FetchResult(string data)
    {
        fetchResult.Data = data;
    }
    
    public IFetchResult GetResult()
    {
        return fetchResult;
    }
}