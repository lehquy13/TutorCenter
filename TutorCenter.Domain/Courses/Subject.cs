using TutorCenter.Domain.Common.Models;

namespace TutorCenter.Domain.Courses;

public class Subject : Entity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

}

