namespace TutorCenter.Application.Contracts.Authentications;

public class AuthenticationResult
{
    public AuthenticationResult(UserLoginDto map, string loginToken)
    {
        User = map;
        Token = loginToken;
    }

    public UserLoginDto? User { get; set; }
    public string Token { get; set; }
}