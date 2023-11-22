using FluentValidation;

namespace TutorCenter.Application.Services.Courses.Queries.GetAllCourseRequestsQuery;

internal class GetAllCourseRequestsValidator : AbstractValidator<GetAllCourseRequests>
{
    public GetAllCourseRequestsValidator()
    {
        RuleFor(x => x.ClassId).NotEmpty();
    }
}