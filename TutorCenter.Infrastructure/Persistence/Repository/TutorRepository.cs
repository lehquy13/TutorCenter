using Microsoft.EntityFrameworkCore;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Users;
using TutorCenter.Domain.Users.Repos;
using TutorCenter.Infrastructure.Entity_Framework_Core;

namespace TutorCenter.Infrastructure.Persistence.Repository;

public class TutorRepository : Repository<Tutor>, ITutorRepository
{
    public TutorRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public override async Task<Tutor?> GetById(int id)
    {
        try
        {
            //TODO: still lack of tutor learning class
            var tutor = await AppDbContext.Tutors
                .Where(o => o.Id == id && o.IsDeleted == false)
                .Include(x => x.Subjects)
                .Include(x => x.TutorVerificationInfos)
                .Include(x => x.CourseRequests)
                .ThenInclude(x => x.Course)
                .FirstOrDefaultAsync();

            return tutor;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public override async Task<List<Tutor>> GetAllList()
    {
        try
        {
            return await AppDbContext.Tutors
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Subjects)
                .Include(x => x.TutorVerificationInfos)
                .Include(x => x.CourseRequests)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Tutor?> GetUserByEmail(string email)
    {
        try
        {
            return await AppDbContext.Tutors.FirstOrDefaultAsync(o => o.Email == email);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public async Task<List<ReviewDetail>> GetReviewsOfTutor(int tutorId)
    {
        var result = await AppDbContext.Courses
            .Where(x => x.Status == Status.Confirmed && x.IsDeleted == false)
            .Include(x =>
                x.CourseRequests.Where(cq => cq.TutorId == tutorId && cq.RequestStatus == RequestStatus.Success))
            .OrderByDescending(x => x.CreationTime)
            .Select(x => x.ReviewDetail)
            .ToListAsync();
        return result;
    }

    public async Task<List<Tutor>> GetPopularTutors()
    {
        var thisMonth = new DateTime(
            DateTime.Now.Year,
            DateTime.Now.Month,
            1
        );
        var result = await AppDbContext.CourseRequests
            .Where(x => x.RequestStatus == RequestStatus.Success)
            .Include(x => x.Course)
            .Where(x => x.Course.CreationTime > thisMonth)
            .Include(x => x.Tutor)
            .GroupBy(x => x.TutorId)
            .OrderByDescending(x => x.Count())
            .Select(x => x.Select(cR => cR.Tutor).Single())
            .ToListAsync();
        return result;
    }
}