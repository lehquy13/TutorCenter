using FluentResults;

namespace TutorCenter.Application.Common.Errors.User;

public class NonExistUserError : IError
{
    public string Message { get; init; } = "This user doesn't exist!";
    public Dictionary<string, object> Metadata { get; } = new();
    public List<IError> Reasons { get; } = new();
}