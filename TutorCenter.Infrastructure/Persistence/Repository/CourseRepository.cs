using Microsoft.EntityFrameworkCore;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Infrastructure.Entity_Framework_Core;

namespace TutorCenter.Infrastructure.Persistence.Repository;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public override async Task<Course?> GetById(int id)
    {
        var result = await Context.Courses
            .Where(x => x.Id == id)
            .Include(x => x.CourseRequests)
            .Include(x => x.Subject)
            .Include(x => x.Tutor)
            //.Include(x => x.Learner)
            .SingleOrDefaultAsync();
        return result;
    }
    public async Task<Course?> GetLearningCourseById(int id)
    {
        var result = await Context.Courses
            .Where(x => x.Id == id)
            .Include(x => x.CourseRequests)
            .Include(x => x.Subject)
            .Include(x => x.Learner)
            .Include(x => x.Subject)
            .Include(x => x.CourseRequests.Where(cr => cr.RequestStatus == RequestStatus.Success))
            .ThenInclude(x => x.Tutor)
            .SingleOrDefaultAsync();
        return result;
    }

    public async Task<List<Course>> GetAllTeachingClassesOfTutor(int tutorId)
    {
        var result = await Context.Courses
            //.Where(x => x.TutorId == tutorId && x.Status == Status.Confirmed)
            .Include(x => x.ReviewDetail)
            .OrderByDescending(x => x.CreationTime)
            .Where(x => x.IsDeleted == false)
            .ToListAsync();
        return result;
    }

    public async Task<List<Course>> GetLearningCoursesByUserId(int learnerId)
    {
        var result = await Context.Courses
            .Where(x => x.LearnerId == learnerId && x.IsDeleted == false)
            .Include(x => x.Subject)
            .Include(x => x.CourseRequests.Where(cr => cr.RequestStatus == RequestStatus.Success))
            .ThenInclude(x => x.Tutor)
            .OrderByDescending(x => x.CreationTime)
            .ToListAsync();
        return result;
    }


    public async Task<List<CourseRequest>> GetAllCourseRequestsByTutorId(int tutorId)
    {
        var result = await Context.Set<CourseRequest>()
            .Where(x => x.TutorId == tutorId)
            .Include(x => x.Course)
            .ThenInclude(x => x.Subject)
            .ToListAsync();
        return result;
    }

    public async Task<Course?> GetAllClassWithRequest(int classId)
    {
        var result = await Context.Courses
            .Where(x => x.Id == classId)
            .Include(x => x.CourseRequests)
            .Include(x => x.Subject)
            .SingleOrDefaultAsync();

        return result;
    }

    public async Task<ReviewDetail?> GetReviewByClassId(int classId)
    {
        var result = await Context.Courses.SingleOrDefaultAsync(x => x.Id == classId);
        if (result != null) return result.ReviewDetail;
        return null;
    }

    public async Task<Course?> GetCourseByRequestId(int requestId)
    {
        var result = await Context.CourseRequests
            .Include(x => x.Course)
            .SingleOrDefaultAsync(x => x.Id == requestId);
        return result?.Course;
    }

    public async Task<List<CourseRequest>> GetCourseRequestsByClassId(int classId)
    {
        var result = await Context.CourseRequests
            .Where(x => x.CourseId == classId)
            // .Include(x => x.Tutor)
            .ToListAsync();
        return result;
    }
}