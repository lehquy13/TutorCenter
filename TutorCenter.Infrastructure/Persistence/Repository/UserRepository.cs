using Microsoft.EntityFrameworkCore;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Users;
using TutorCenter.Domain.Users.Repos;
using TutorCenter.Infrastructure.Entity_Framework_Core;

namespace TutorCenter.Infrastructure.Persistence.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public override async Task<User?> GetById(int id)
    {
        try
        {
            //TODO: still lack of tutor learning class
            var user = await AppDbContext.Users
                .Where(o => o.Id == id && o.IsDeleted == false)
                .Include(x => x.LearningCourses)
                .FirstOrDefaultAsync();

            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<List<User>> GetTutors()
    {
        try
        {
            var users = await AppDbContext.Set<User>().Where(x => x.IsDeleted == false && x.Role == UserRole.Tutor).ToListAsync();
            return users;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task< List<User>> GetLearners()
    {
        try
        {
            var users = await AppDbContext.Set<User>().Where(o => o.Role == UserRole.Learner && o.IsDeleted == false).ToListAsync();
            return users;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> ExistenceCheck(string? email)
    {
        try
        {
            if (email != null)
            {
                return await AppDbContext.Set<User>().AnyAsync(o => o.Email.Equals(email));
            }

            return false;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        try
        {
            var user = await AppDbContext.Users.FirstOrDefaultAsync(o => o.Email == email);

            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
}