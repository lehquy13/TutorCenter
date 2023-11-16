using TutorCenter.Domain.Common.Models;
using TutorCenter.Domain.Courses;

namespace TutorCenter.Domain.Review
{
    public class TutorReview : AuditedEntity<int>
    {
        public int ClassInformationId { get; set; }
        public Course ClassInformation { get; set; } = null!;
        public short Rate { get; set; } = 5;
        public string Description { get; set; } = "";
    }
}
