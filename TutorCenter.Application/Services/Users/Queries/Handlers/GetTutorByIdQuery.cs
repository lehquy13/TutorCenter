using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace TutorCenter.Application.Services.Users.Queries.Handlers
{
    public class GetTutorByIdQuery : IRequest<Result<TutorForDetailDto>>
    {
       public int ObjectId { get; set; }
        public GetTutorByIdQuery() { }
    }
}
