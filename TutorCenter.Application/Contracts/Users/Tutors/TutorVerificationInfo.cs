using TutorCenter.Application.Contracts.Models;

namespace TutorCenter.Application.Contracts.Users.Tutors;

public class TutorVerificationInfoDto : BasicAuditedEntityDto<int>
{
    public int TutorId { get; set; }
    public string Image { get; set; } = "doc_contract.png";
}