using EduSmart.Domain.Repository;

namespace TutorCenter.Domain.Courses.Repos;

public interface ICourseRequestRepository : IRepository<CourseRequest>
{
    Task<bool> IsRequested(int tutorId, int classId);
}