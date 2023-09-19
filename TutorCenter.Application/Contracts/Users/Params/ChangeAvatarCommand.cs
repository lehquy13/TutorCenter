namespace TutorCenter.Application.Contracts.Users.Params;

public class ChangeAvatarCommand
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public Stream Stream { get; set; } = null!;
}