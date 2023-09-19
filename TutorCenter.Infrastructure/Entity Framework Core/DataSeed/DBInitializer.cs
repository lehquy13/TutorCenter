using TutorCenter.Domain;

namespace TutorCenter.Infrastructure.Entity_Framework_Core.DataSeed;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();
        
        // Look for any subjects.
        if (!context.Subjects.Any())
        {
            var seeder = new DataSeeder();
           
            context.SaveChanges();
        }
    }
    
    
}

