using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Users;

namespace TutorCenter.Application.Services.Users.Queries.Handlers
{
    public class GetUsersQuery : IRequest<Result<PaginatedList<UserDto>>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 100;
        public GetUsersQuery()
        {
            PageIndex = 1;
        }
    }
}
