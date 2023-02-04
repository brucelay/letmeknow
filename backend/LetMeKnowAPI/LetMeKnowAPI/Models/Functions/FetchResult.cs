namespace LetMeKnowAPI.Models.Functions;

public class FetchResult
{
    public string Content { get; }

    public FetchResult(String content)
    {
        Content = content;
    }
}