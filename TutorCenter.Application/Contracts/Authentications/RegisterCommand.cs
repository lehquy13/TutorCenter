namespace TutorCenter.Application.Contracts.Authentications;

public record RegisterCommand
(
    string FirstName,
    string LastName,
    string Email,
    string Password
);