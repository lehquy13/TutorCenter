using TutorCenter.Domain.Common.Models;

namespace TutorCenter.Domain.Courses;

public class ReviewDetail : ValueObject
{
    public short Rate { get; set; } = 5;
    public string Detail { get; set; } = "";
    public override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}