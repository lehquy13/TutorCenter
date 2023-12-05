using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Application.Contracts.Users;

namespace TutorCenter.Administrator.Models;

public class ProfileViewModel
{
    public UserDto UserDto { get; set; } = new();
    public ChangePasswordCommand ChangePasswordCommand { get; set; }
    
}