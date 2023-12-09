using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Application.Contracts.Users;
using TutorCenter.Application.Services.Authentication.Admin.Commands.ChangePassword;

namespace TutorCenter.Administrator.Models;

public class ProfileViewModel
{
    public UserForDetailDto UserDto { get; set; } = new();
    public ChangePasswordCommand ChangePasswordCommand { get; set; }
    
}