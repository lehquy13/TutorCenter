using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.ClassInformationConsts;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using FluentResults;

namespace TutorCenter.Application.Services.Users.Queries.CustomerQueries
{
    public class GetAllTutorInformationsAdvancedQuery : IRequest<Result<PaginatedList<TutorForListDto>>>
    {
        public string SubjectName { get; set; } = string.Empty;
        public string TutorName { get; set; } = string.Empty;
        public int BirthYear { get; set; } = 0;
        public AcademicLevel Academic { get; set; } = AcademicLevel.Optional;
        public Gender Gender { get; set; } = Gender.None;
        public string Address { get; set; } = string.Empty;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 100;

        public GetAllTutorInformationsAdvancedQuery()
        {
            PageIndex = 1;
        }

    }
}
