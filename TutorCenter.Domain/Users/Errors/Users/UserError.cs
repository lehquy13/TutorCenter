namespace TutorCenter.Domain.Users.Errors.Users;

public class UserError 
{
    public static string NonExistUserError { get; } = "This user doesn't exist!";
    public static string NonExistTutorError { get; } = "This tutor doesn't exist!";
    public static string FailToUpdateUserError(string email)
    {
        return $"Fail to update user with email + {email}!";
    }
    public static string FailTCreateUserError(string email)
    {
        return $"Fail to create user with email + {email}!";
    } 
    public static string FailToUpdateTutorError(string email)
    {
        return $"Fail to update tutor with email + {email}!";
    }
    public static string FailTCreateTutorError(string email)
    {
        return $"Fail to create tutor with email + {email}!";
    } 
    public static string FailTCreateOrUpdateUserError(string email, string message)
    {
        return $"Fail to create user with email + {email} because {message}!";
    } 
}
