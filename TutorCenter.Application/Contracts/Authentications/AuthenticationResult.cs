namespace TutorCenter.Application.Contracts.Authentications;

public class AuthenticationResult 
{
    public AuthenticationResult(object value, string v1, bool v2, string v3)
    {
        Value = value;
        V1 = v1;
        V2 = v2;
        V3 = v3;
    }

    public UserLoginDto? User { get; set; }
    public string Token { get; set; } = string.Empty;
    public object Value { get; }
    public string V1 { get; }
    public bool V2 { get; }
    public string V3 { get; }
}

