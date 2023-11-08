using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Application.Contracts.Users.Tutors;
using MediatR;
using FluentResults;

namespace TutorCenter.Application.Services.Users.Queries.GetTutorProfile
{
    public class GetTutorProfileQuery : IRequest<Result<TutorProfileDto>>
    {
        public int id { get; set; }
        public GetTutorProfileQuery() { }
    }
}
