namespace TutorCenter.Domain.Users.Errors.Courses;

public class CourseError 
{
    public static string NonExistCourseError { get; } = "This course doesn't exist!";
    public static string UnAvailableClassError { get; } = "This course isn't available!";
    public static string NonExistSubjectError { get; } = "This subject doesn't exist!";
    public static string IncorrectUserOfCourseError { get; } = "This user doesn't a learner of this course!";
    public static string NonExistCourseRequestError { get; } = "This request doesn't exist!";
    public static string RequestedCourseError { get; } = "This course was requested by the user!";

}
