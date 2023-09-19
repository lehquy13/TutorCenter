using TutorCenter.Application.Contracts.Models;

namespace TutorCenter.Application.Contracts.Users;
public class UserForListDto : FullAuditedAggregateRootDto<int>
{
    //User information
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = "Male";
    public int BirthYear { get; set; } = 1960;
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    //Account References
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    //is tutor related informtions
    public string Role { get; set; } = "Learner";

}

