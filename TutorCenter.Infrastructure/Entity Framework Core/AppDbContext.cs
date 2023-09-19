using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Notifications;
using TutorCenter.Domain.Subscribers;
using TutorCenter.Domain.Users;

namespace TutorCenter.Infrastructure.Entity_Framework_Core;

public class AppDbContext : DbContext
{
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
   // public DbSet<ReviewDetail> ReviewDetails { get; set; } = null!;
    public DbSet<CourseRequest> CourseRequests { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Tutor> Tutors { get; set; } = null!;

    public DbSet<TutorVerificationInfo> TutorVerificationInfos { get; set; } = null!;

    public DbSet<TutorMajor> TutorMajors { get; set; } = null!;
    //public DbSet<LearnerCourse> LearnerCourses { get; set; } = null!;

    public DbSet<Subscriber> Subscribers { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>().ToTable("Subject");
      
        modelBuilder.Entity<Course>(re =>
        {
            re.ToTable("Course");
            re.HasKey(r => r.Id);
            re.Property(r => r.Title).IsRequired().HasMaxLength(128);
            re.Property(r => r.Description).IsRequired().IsUnicode();
            re.Property(r => r.Fee).IsRequired();
            re.OwnsOne(r => r.ReviewDetail);

        });
        modelBuilder.Entity<CourseRequest>(re =>
        {
            re.ToTable("CourseRequest");
        });
        
        
        modelBuilder.Entity<User>(re =>
        {
            re.ToTable("User");
            re.Property(x => x.FirstName).IsRequired().HasMaxLength(128);
            re.Property(x => x.LastName).IsRequired().HasMaxLength(128);
            re.Property(x => x.Email).IsRequired().HasMaxLength(128);
            re.Property(x => x.Password).IsRequired().HasMaxLength(128);
            re.Property(x => x.Role).IsRequired();
            re.Property(x => x.Gender).IsRequired();

        });
        modelBuilder.Entity<Tutor>(re =>
        {
            re.ToTable("Tutor");
            re.HasOne<User>().WithOne().HasForeignKey<Tutor>(x => x.Id).IsRequired();
            re.HasMany(x=>x.Subjects).WithMany().UsingEntity<TutorMajor>();
        });
        modelBuilder.Entity<TutorMajor>(re =>
        {
            re.ToTable("TutorMajor");
            re.HasKey(r => r.Id);
        });
        // modelBuilder.Entity<LearnerCourse>(re =>
        // {
        //     re.ToTable("LearnerCourse");
        //     re.HasKey(r => r.Id);
        // });
        modelBuilder.Entity<TutorVerificationInfo>(re =>
        {
            re.ToTable("TutorVerificationInfos");
        });
        modelBuilder.Entity<Subscriber>(re =>
        {
            re.ToTable("Subscriber");
            re.HasOne<Tutor>().WithMany().HasForeignKey(x => x.TutorId).IsRequired();
        });
        modelBuilder.Entity<Notification>(re =>
        {
            re.ToTable("Notification");
            re.Property(x => x.Message).IsRequired();
        });
    }
}

//using to support addmigration
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB; Database=SE347_DB; Trusted_Connection=True;MultipleActiveResultSets=true"
            );

        return new AppDbContext(optionsBuilder.Options);
    }
}