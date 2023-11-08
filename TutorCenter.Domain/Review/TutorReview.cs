using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Domain.ClassInformations;
using TutorCenter.Domain.Common.Models;

namespace TutorCenter.Domain.Review
{
    public class TutorReview : AuditedEntity<Guid>
    {
        public Guid ClassInformationId { get; set; }
        public ClassInformation ClassInformation { get; set; } = null!;
        public short Rate { get; set; } = 5;
        public string Description { get; set; } = "";
    }
}
