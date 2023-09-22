using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Users;
using TutorCenter.Infrastructure.Authentication;
using TutorCenter.Infrastructure.Entity_Framework_Core;
using Newtonsoft.Json;

namespace TutorCenter.DBMigrator;

internal static class Program
{
    public static void Main(string[] args)
    {
        var factory = new AppDbContextFactory();
        var context = factory.CreateDbContext(args);
        Console.WriteLine("Checking database is created or not...");
        context.Database.EnsureCreated();
        Console.WriteLine("Checked!");

        Console.WriteLine("Checking subject table is migrated or not...");

        // Look for any subjects.
        if (!context.Subjects.Any())
        {
            var programming = new Subject { Id = 0, Name = "Programming", Description = "Basic subject" };
            var java = new Subject { Id = 0, Name = "Java programming", Description = "Li" };
            var informatics = new Subject { Id = 0, Name = "Informatics", Description = "Alonso" };
            var otherSubject = new Subject { Id = 0, Name = "Other", Description = "Other subject" };
            var korean = new Subject { Id = 0, Name = "Korean", Description = "25 Bucket List Adventures" };
            var spain = new Subject { Id = 0, Name = "Spanish", Description = "25 Bucket List Adventures" };
            var vietnameses = new Subject { Id = 0, Name = "Vietnamese for foreigner", Description = "Justice" };
            var german = new Subject { Id = 0, Name = "German", Description = "25 Bucket List Adventures" };
            var english = new Subject { Id = 0, Name = "English", Description = "25 Bucket List Adventures" };
            var guitar = new Subject { Id = 0, Name = "Guitar", Description = "25 Bucket List Adventures" };
            var chemistry = new Subject { Id = 0, Name = "Chemistry", Description = "Alonso" };
            var dance = new Subject { Id = 0, Name = "Dance", Description = "Private & Custom Tours" };
            var piano = new Subject { Id = 0, Name = "Piano", Description = "Private & Custom Tours" };
            var fit = new Subject { Id = 0, Name = "Fitness", Description = "Private & Custom Tours" };
            var paint = new Subject { Id = 0, Name = "Paint", Description = "25 Bucket List Adventures" };
            var math = new Subject { Id = 0, Name = "Mathematics", Description = "Private & Custom Tours" };
            var cook = new Subject { Id = 0, Name = "Cooking", Description = "Top Food Experiences" };

            var subjects = new List<Subject>()
            {
                new() { Id = 0, Name = "Physics", Description = "Alexander" },
                new() { Id = 0, Name = "Biology", Description = "Anand" },
                new() { Id = 0, Name = "Geography", Description = "Barzdukas" },
                new() { Id = 0, Name = "Information Technology", Description = "Li" },
                new() { Id = 0, Name = "Fine Art", Description = "Justice" },
                new() { Id = 0, Name = "Literature", Description = "Norman" },
                new() { Id = 0, Name = "History", Description = "Olivetto" },
                new() { Id = 0, Name = "Engineering", Description = "Alexander" },
                new() { Id = 0, Name = "Technology", Description = "Anand" },
                new() { Id = 0, Name = "Politics", Description = "Barzdukas" },
                new() { Id = 0, Name = "Psychology", Description = "Li" },
                new() { Id = 0, Name = "Economics", Description = "Justice" },
                new() { Id = 0, Name = "Physical Education", Description = "Norman" },
                new() { Id = 0, Name = "C# programming", Description = "Barzdukas" },
                new() { Id = 0, Name = "Python programming", Description = "Justice" },
                new() { Id = 0, Name = "Web programming", Description = "Norman" },
                new() { Id = 0, Name = "HTML,CSS & Javascript", Description = "Olivetto" },
                informatics, chemistry, programming, java, english,
                guitar, dance, piano, otherSubject, german, korean,
                vietnameses, spain,
                fit, paint, math, cook,
            };

            context.Subjects.AddRange(subjects);
            context.SaveChanges();

            //subjectData = context.Subjects.ToList();
            var file = File.ReadAllText(Path.GetFullPath("../../../15_random_courses_female_noAcc.json"));
            var courseData = JsonConvert.DeserializeObject<List<Course>>(file);
            if (courseData == null) return;
            file = File.ReadAllText(Path.GetFullPath("../../../15_random_courses_male_noAcc.json"));
            courseData.AddRange(JsonConvert.DeserializeObject<List<Course>>(file));
            file = File.ReadAllText(Path.GetFullPath("../../../30_random_course_having_account.json"));
            var random30courses = JsonConvert.DeserializeObject<List<Course>>(file);
            courseData.AddRange(random30courses);

            file = File.ReadAllText(Path.GetFullPath("../../../15_random_female_account.json"));
            var userData = JsonConvert.DeserializeObject<List<User>>(file);
            if (userData == null) return;
            file = File.ReadAllText(Path.GetFullPath("../../../15_random_male_account.json"));
            userData.AddRange(JsonConvert.DeserializeObject<List<User>>(file));

            file = File.ReadAllText(Path.GetFullPath("../../../20_random_tutor.json"));
            var tutorData = JsonConvert.DeserializeObject<List<Tutor>>(file);
            if (tutorData == null) return;
            file = File.ReadAllText(Path.GetFullPath("../../../request_course_random.json"));
            var requestData = JsonConvert.DeserializeObject<List<CourseRequest>>(file);
            if (requestData == null) return;
            Console.WriteLine("Hash password for users...");
            foreach (var u in userData)
            {
                u.Password = (new Validator()).HashPassword(u.Password);
            }
            foreach (var u in tutorData)
            {
                u.Password = (new Validator()).HashPassword(u.Password);
            }
            Console.WriteLine("Done hashing password. Adding users...");

            context.Users.AddRange(userData);
            context.SaveChanges();
            context.Tutors.AddRange(tutorData);
            context.SaveChanges();
            context.Courses.AddRange(courseData);
            context.SaveChanges();

            file = File.ReadAllText(Path.GetFullPath("../../../tutorMajorRandom.json"));
            var majorData = JsonConvert.DeserializeObject<List<TutorMajor>>(file);
            if (majorData == null) return;
            context.TutorMajors.AddRange(majorData);
            context.SaveChanges();

            context.CourseRequests.AddRange(requestData);
            context.SaveChanges();


            Console.WriteLine("All done! Enjoy my website!");
        }
        else
        {
            Console.WriteLine("Nothing is added");
        }
    }
}