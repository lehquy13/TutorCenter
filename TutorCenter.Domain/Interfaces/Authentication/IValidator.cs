namespace TutorCenter.Domain.Interfaces.Authentication;

public interface IValidator
{
    public string GenerateValidationCode();
    public string HashPassword(string input);

}