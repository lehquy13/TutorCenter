using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorCenter.Application.Services.Courses.Tutor.Queries.GetTeachingClassDetailQuery
{
    public class GetTeachingClassDetailQueryValidator : AbstractValidator<GetTeachingClassDetailQuery>
    {
        public GetTeachingClassDetailQueryValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
            RuleFor(x => x.ObjectId).NotEmpty();
        }
    }
}
