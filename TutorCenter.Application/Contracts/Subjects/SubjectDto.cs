using TutorCenter.Application.Contracts.Models;

namespace TutorCenter.Application.Contracts.Subjects;

public class SubjectDto : FullAuditedAggregateRootDto<int>
{

    public string Name { get; set; }
    public string Description { get; set; }

    public SubjectDto() {
        Name = string.Empty;
        Description = string.Empty;
    }
  
}

