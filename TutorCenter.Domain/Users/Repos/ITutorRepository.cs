using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Repository;

namespace TutorCenter.Domain.Users.Repos;

public interface ITutorRepository : IRepository<Tutor>
{
    Task<Tutor?> GetUserByEmail(string email);
    Task<List<ReviewDetail>> GetReviewsOfTutor(int tutorId);
    Task<List<Tutor>> GetPopularTutors();

}

