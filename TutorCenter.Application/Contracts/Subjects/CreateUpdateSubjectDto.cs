using System.ComponentModel.DataAnnotations;
using TutorCenter.Application.Contracts.Models;

namespace TutorCenter.Application.Contracts.Subjects;

public class CreateUpdateSubjectDto : FullAuditedAggregateRootDto<int>
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; }
    public string Description { get; set; }

    public CreateUpdateSubjectDto() {
        Name = string.Empty;
        Description = string.Empty;
    }
}

