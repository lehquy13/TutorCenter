using FluentValidation;

namespace TutorCenter.Application.Services.Courses.Tutor.Queries.GetTeachingClassDetailQuery;

public class GetTeachingClassDetailQueryValidator : AbstractValidator<GetTeachingClassDetailQuery>
{
    public GetTeachingClassDetailQueryValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty();
        RuleFor(x => x.ObjectId).NotEmpty();
    }
}