using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts.Users.Learners;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using Microsoft.AspNetCore.Http.HttpResults;
using FluentResults;

namespace TutorCenter.Application.Services.Users.Queries.Handlers
{
    public class GetAllStudentsQuery : IRequest<Result<PaginatedList<LearnerDto>>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 100;
        public GetAllStudentsQuery()
        {
            PageIndex = 1;
        }
    }
}
