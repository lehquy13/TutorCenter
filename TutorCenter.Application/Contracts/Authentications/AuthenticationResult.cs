namespace TutorCenter.Application.Contracts.Authentications;

public class AuthenticationResult 
{
    public UserLoginDto? User { get; set; }
    public string Token { get; set; } = string.Empty;
}

