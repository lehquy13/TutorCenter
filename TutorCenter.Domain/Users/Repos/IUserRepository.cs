using EduSmart.Domain.Repository;

namespace TutorCenter.Domain.Users.Repos;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmail(string email);
    Task<List<User>> GetLearners();
    Task<List<User>> GetTutors();
}