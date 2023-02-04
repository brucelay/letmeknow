namespace LetMeKnowAPI.Models.Functions;

public class TextResult
{
    public Boolean Success { get; }

    public TextResult(Boolean success)
    {
        Success = success;
    }
}