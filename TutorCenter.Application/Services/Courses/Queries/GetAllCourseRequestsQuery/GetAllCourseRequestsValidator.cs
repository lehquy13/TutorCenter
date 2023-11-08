using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorCenter.Application.Services.Courses.Queries.GetAllCourseRequestsQuery
{
    internal class GetAllCourseRequestsValidator : AbstractValidator<GetAllCourseRequests>
    {
        public GetAllCourseRequestsValidator()
        {
            RuleFor(x => x.ClassId).NotEmpty();
        }
    }
}
