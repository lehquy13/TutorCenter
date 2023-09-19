using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Common.Models;
using TutorCenter.Domain.Courses;

namespace TutorCenter.Domain.Users;
public class User : FullAuditedAggregateRoot<int>
{
    //User information
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; } = Gender.Male;
    public int BirthYear { get; set; } = 1960;
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string Image { get; set; } = @"https://res.cloudinary.com/dhehywasc/image/upload/v1686121404/default_avatar2_ws3vc5.png";

    //Account References
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; } = false;

    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = "1q2w3E*";

    public UserRole Role { get; set; } = UserRole.Learner;
    public ICollection<Course> LearningCourses { get; set; } 
   

    // constructor
    public User() { }

    /// <summary>
    /// Update standard user's information
    /// </summary>
    /// <param name="user"></param>
    public void UpdateUserInformation(User user)
    {
        FirstName = user.FirstName;
        LastName = user.LastName;
        Gender = user.Gender;
        BirthYear = user.BirthYear;
        Address = user.Address;
        Description = user.Description;

        Role = user.Role;

        Email = user.Email;
        PhoneNumber = user.PhoneNumber;
        Image = user.Image;
        
        LastModificationTime = DateTime.Now;
    }
    public void UpdateUserInformationExceptImage(User user)
    {
        FirstName = user.FirstName;
        LastName = user.LastName;
        Gender = user.Gender;
        BirthYear = user.BirthYear;
        Address = user.Address;
        Description = user.Description;

        Role = user.Role;

        Email = user.Email;
        PhoneNumber = user.PhoneNumber;
        LastModificationTime = DateTime.Now;

    }
    /// <summary>
    /// Update current user to be tutor
    /// </summary>
    /// <param name="tutor"></param>
    public Tutor RegisterToBeTutor(Tutor tutor)
    {
        tutor.Id = Id;
        return tutor;
    }
    public string GetFullNAme()
    {
        return this.FirstName + " " + this.LastName;
    }
  
}

