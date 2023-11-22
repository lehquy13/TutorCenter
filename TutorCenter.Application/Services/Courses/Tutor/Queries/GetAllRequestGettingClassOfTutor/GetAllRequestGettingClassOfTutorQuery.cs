using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace TutorCenter.Application.Services.Courses.Tutor.Queries.GetAllRequestGettingClassOfTutor
{
    public class GetAllRequestGettingClassOfTutorQuery :IRequest<Result<PaginatedList<RequestGettingClassForListDto>>>
    {
        public int ObjectId { get; set; }
        

        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 100;
        public GetAllRequestGettingClassOfTutorQuery()
        {
            PageIndex = 1;
        }
    }
}
