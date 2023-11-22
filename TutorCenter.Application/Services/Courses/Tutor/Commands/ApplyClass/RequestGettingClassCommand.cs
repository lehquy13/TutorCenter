using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorCenter.Application.Services.Courses.Tutor.Commands.ApplyClass
{
    public record RequestGettingClassCommand
(
    int TutorId,
    int ClassId
    ) : IRequest<Result<bool>>;

}
