using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using FluentResults;
using TutorCenter.Application.Contracts;

namespace TutorCenter.Application.Services.Users.Queries.Handlers
{
    public class GetAllTutorsQuery : IRequest<Result<PaginatedList<TutorForDetailDto>>>
    {
    }
}
