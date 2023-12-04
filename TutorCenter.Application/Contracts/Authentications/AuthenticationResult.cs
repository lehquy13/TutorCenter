namespace TutorCenter.Application.Contracts.Authentications;

public class AuthenticationResult
{
    public AuthenticationResult(UserLoginDto map, string loginToken,bool isSuccess,string message)
    {
        User = map;
        Token = loginToken;
    }

    public UserLoginDto? User { get; set; }
    public string Token { get; set; }

    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}