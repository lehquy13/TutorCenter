using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts.Users;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace TutorCenter.Application.Services.Users.Queries.Handlers
{
    public class GetUserByIdQuery: IRequest<Result<UserDto>>
    {
        public int ObjectId { get; set; }
        public GetUserByIdQuery () { }
    }
}
