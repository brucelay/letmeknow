using System.ComponentModel.DataAnnotations;

namespace LetMeKnowAPI.Models.Functions;

public class FetchFunction : IWorkflowFunction<FetchResult>
{
    private readonly string _url;
    private readonly HttpClient _httpClient;

    private FetchResult _fetchResult;

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

        _fetchResult = new FetchResult(content);
    }

    public FetchResult GetResult()
    {
        return _fetchResult;
    }
}