using TutorCenter.Domain.Common.Models;

namespace TutorCenter.Domain.Subscribers;
public class Subscriber : Entity<int>
{
    public int TutorId { get; set; }
}

