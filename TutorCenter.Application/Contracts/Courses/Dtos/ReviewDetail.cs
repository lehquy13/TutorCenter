namespace TutorCenter.Application.Contracts.Courses.Dtos;

public class ReviewDetailDto
{
    public int CourseId { get; set; }
    public int LearnerId { get; set; }
    public short Rate { get; set; } = 5;
    public string Detail { get; set; } = "";
}