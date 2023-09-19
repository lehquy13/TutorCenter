namespace TutorCenter.Application.Contracts.Authentications;

public record LoginQuery
(
    string Email,
    string Password);

