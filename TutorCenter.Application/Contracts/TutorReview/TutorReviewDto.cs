using TutorCenter.Application.Contracts.Models;

namespace TutorCenter.Application.Contracts.TutorReview;

public class TutorReviewDto : FullAuditedAggregateRootDto<int>
{
    public int TutorId { get; set; }
    public int LearnerId { get; set; }
    public string LearnerName { get; set; } = string.Empty;
    public int ClassInformationId { get; set; }

    public short Rate { get; set; } = 5;
    public string Description { get; set; } = string.Empty;
}

public class TutorReviewRequestDto : FullAuditedAggregateRootDto<int>
{
    public string TutorEmail { get; set; } = string.Empty;
    public string ClassId { get; set; } = string.Empty;
    public short Rate { get; set; } = 5;
    public string Description { get; set; } = "";
}