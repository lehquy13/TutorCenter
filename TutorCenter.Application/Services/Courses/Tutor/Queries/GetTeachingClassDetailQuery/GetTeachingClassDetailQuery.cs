using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace TutorCenter.Application.Services.Courses.Tutor.Queries.GetTeachingClassDetailQuery
{
    public class GetTeachingClassDetailQuery : IRequest<Result<RequestGettingClassExtendDto>>
    {

        public int CourseId { get; set; }
        public int ObjectId {  get; set; }
    }
}
