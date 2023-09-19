using TutorCenter.Domain.Common.Models;

namespace TutorCenter.Domain.Users;

public class TutorVerificationInfo : Entity<int>
{
    public int TutorId { get; set; }
    public Tutor Tutor { get; set; } = null!;
    public string Image { get; set; } = "doc_contract.png";
}