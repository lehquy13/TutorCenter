using FluentResults;

namespace TutorCenter.Application.Common.Errors.Courses;

public class NonExistClassError : IError
{
    public string Message { get; init; } = "This class doesn't exist!";
    public Dictionary<string, object> Metadata { get; } = new();
    public List<IError> Reasons { get; } = new();
}

public class UnAvailableClassError : IError
{
    public string Message { get; init; } = "This class isn't available!";
    public Dictionary<string, object> Metadata { get; } = new();
    public List<IError> Reasons { get; } = new();
}