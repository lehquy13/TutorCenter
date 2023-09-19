
namespace TutorCenter.Domain.Users.Errors.Courses;

public class RequestError
{
    public static string NonExistRequestError { get; } = "This request in the course doesn't exist!";
    public static string ExistedRequestError { get;  } = "Tutor has already requested this course!";

}
