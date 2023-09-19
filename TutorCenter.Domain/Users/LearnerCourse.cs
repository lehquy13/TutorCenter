using TutorCenter.Domain.Common.Models;

namespace TutorCenter.Domain.Users;

public class LearnerCourse : Entity<int>
{
    public int LearnerId { get; set; }
    public int CourseId { get; set; }
  
}