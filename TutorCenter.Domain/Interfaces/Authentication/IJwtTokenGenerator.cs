using TutorCenter.Domain.Users;

namespace TutorCenter.Domain.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        bool ValidateToken(string token);
    }
}
