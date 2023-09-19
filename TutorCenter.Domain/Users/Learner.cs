using TutorCenter.Domain.ClassInformationConsts;

namespace TutorCenter.Domain.Users;

public class Learner : User
{
    public Learner()
    {
        Role = UserRole.Learner;
    }
   
}