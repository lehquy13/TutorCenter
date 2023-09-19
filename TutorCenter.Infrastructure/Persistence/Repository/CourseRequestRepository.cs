using Microsoft.EntityFrameworkCore;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Infrastructure.Entity_Framework_Core;

namespace TutorCenter.Infrastructure.Persistence.Repository;

public class CourseRequestRepository : Repository<CourseRequest>, ICourseRequestRepository
{
    public CourseRequestRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<bool> IsRequested(int tutorId, int classId)
    {
        return await AppDbContext.CourseRequests.Where(x => x.CourseId == classId /* && x.TutorId == tutorId*/).AnyAsync();

    }
}