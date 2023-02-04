namespace LetMeKnowAPI.Models.Functions;

public class FetchResult
{
    private String Content { get; }

    public FetchResult(String content)
    {
        Content = content;
    }
}