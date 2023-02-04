namespace LetMeKnowAPI.Models;

public interface IResult<T>
{
    public T GetResult();
}