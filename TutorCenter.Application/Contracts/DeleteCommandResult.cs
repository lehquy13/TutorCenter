namespace TutorCenter.Application.Contracts;

public class DeleteUpdateCommandResult 
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public int Id { get; set; }
    public DeleteUpdateCommandResult(bool isSuccess, string message, int id)
    {
        IsSuccess = isSuccess;
        Message = message;
        Id = id;
    }
    public DeleteUpdateCommandResult()
    {
        Message = string.Empty;
        IsSuccess = false;
        Id = 0;
    }
}