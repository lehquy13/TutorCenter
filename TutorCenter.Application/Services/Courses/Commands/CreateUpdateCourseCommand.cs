using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts.Courses.Dtos;

namespace TutorCenter.Application.Services.Courses.Commands;

public class CreateUpdateCourseCommand
    : IRequest<Result<bool>>
{
    public CourseForDetailDto CourseDto { get; set; } = null!;
    public string Email { get; set; } = null!;
}