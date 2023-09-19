using TutorCenter.Domain.Common.Models;

namespace TutorCenter.Domain.Users;

public class TutorMajor : Entity<int>
{
    public int TutorId { get; set; }
    public int SubjectId { get; set; }
  
}