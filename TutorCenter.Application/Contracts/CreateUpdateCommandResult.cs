﻿namespace TutorCenter.Application.Contracts;

public class CreateUpdateCommandResult 
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public int Id { get; set; }
    public CreateUpdateCommandResult(bool isSuccess, string message, int id)
    {
        IsSuccess = isSuccess;
        Message = message;
        Id = id;
    }
    public CreateUpdateCommandResult()
    {
        Message = string.Empty;
        IsSuccess = false;
        Id = 0;
    }
}