using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses;
using TutorCenter.Domain.Subscribers;
using TutorCenter.Domain.Users;

namespace TutorCenter.Domain;

/// <summary>
/// TODO: insert the request getting class record 
/// </summary>
public class DataSeeder
{
    public List<User> Users { get; private set; } = new List<User>();
    public List<Tutor> Tutors { get; private set; } = new List<Tutor>();
    public List<TutorMajor> TutorMajors { get; private set; } = new List<TutorMajor>();
    public List<Learner> Leanrner { get; private set; } = new List<Learner>();
    public List<Subject> Subjects { get; private set; } = new List<Subject>();
    public List<Subscriber> Subscribers { get; private set; } = new List<Subscriber>();
    public List<Course> ClassInformations { get; private set; } = new List<Course>();

    public DataSeeder()
    {
        
    }

    
}