using System.ComponentModel.DataAnnotations;

namespace TutorCenter.Contracts.Authentication;
public class ChangePasswordRequest
{
    [Required]
    public int Id { get; set; } 

    [Required]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required]
    public string NewPassword { get; set; } = string.Empty;

    [Required]
    [Compare("NewPassword")]
    public string ConfirmedPassword { get; set; } = string.Empty;
}

