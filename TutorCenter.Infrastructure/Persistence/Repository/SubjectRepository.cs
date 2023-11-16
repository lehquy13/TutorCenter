using Microsoft.EntityFrameworkCore;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Infrastructure.Entity_Framework_Core;

namespace TutorCenter.Infrastructure.Persistence.Repository;

public class SubjectRepository : Repository<Subject>, ISubjectRepository
{
    public SubjectRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<Subject?> GetSubjectByName(string name)
    {
        try
        {
            return await Context.Set<Subject>().FirstOrDefaultAsync(o => o.Name.ToLower() == name.ToLower());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<Subject>> GetTutorMajors(int tutorId)
    {
        //throw new NotImplementedException();
        try
        {
            return await Context.Tutors.Where(x => x.Id == tutorId)
                .Include(x => x.Subjects)
                .SelectMany(x => x.Subjects)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

