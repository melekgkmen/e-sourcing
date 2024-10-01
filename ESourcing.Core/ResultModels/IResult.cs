namespace ESourcing.Core.ResultModels
{
    public interface IResult
    {
        bool IsSuccess { get; }
        string Message { get; }
    }
}
