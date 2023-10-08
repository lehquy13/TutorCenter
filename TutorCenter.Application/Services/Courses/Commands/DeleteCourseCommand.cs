using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorCenter.Application.Services.Courses.Commands;

public record DeleteCourseCommand
(
    int GuidId
) : IRequest<Result<bool>>;
