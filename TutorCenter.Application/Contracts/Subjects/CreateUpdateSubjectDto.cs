using System.ComponentModel.DataAnnotations;
using TutorCenter.Application.Contracts.Models;

namespace TutorCenter.Application.Contracts.Subjects;

public class CreateUpdateSubjectDto : FullAuditedAggregateRootDto<int>
{
    public CreateUpdateSubjectDto()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    [Required] [StringLength(128)] public string Name { get; set; }

    public string Description { get; set; }
}