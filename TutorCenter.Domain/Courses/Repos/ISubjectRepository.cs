using EduSmart.Domain.Repository;

namespace TutorCenter.Domain.Courses.Repos;

public interface ISubjectRepository : IRepository<Subject>
{
    public Task<Subject?> GetSubjectByName(string name);
    public Task<List<Subject>> GetTutorMajors(int tutorId);
}

