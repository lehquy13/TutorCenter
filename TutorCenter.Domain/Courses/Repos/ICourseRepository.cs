using EduSmart.Domain.Repository;

namespace TutorCenter.Domain.Courses.Repos;

public interface ICourseRepository : IRepository<Course>
{
    Task<List<CourseRequest>> GetAllCourseRequestsByTutorId(int tutorId);
    Task<List<Course>> GetAllTeachingClassesOfTutor (int tutorId);
    Task<List<Course>> GetLearningCoursesByUserId(int learnerId);
    Task<List<CourseRequest>> GetCourseRequestsByClassId(int classId);
    Task<Course?> GetAllClassWithRequest(int classId);
    Task<Course?> GetLearningCourseById(int courseId);
    
    //Reviews of Tutor
    Task<ReviewDetail?> GetReviewByClassId(int classId);
    Task<Course?> GetCourseByRequestId(int requestId);
}