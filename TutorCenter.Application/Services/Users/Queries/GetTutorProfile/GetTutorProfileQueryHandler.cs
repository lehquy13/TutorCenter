using FluentResults;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.GetTutorProfile
{
    public class
    GetTutorProfileQueryHandler : GetByIdQueryHandler<GetTutorProfileQuery, TutorProfileDto>
    {
        private readonly ITutorRepository _tutorRepository;

        public GetTutorProfileQueryHandler(
            ITutorRepository tutorRepository,
            IMapper mapper) : base(mapper)
        {
            _tutorRepository = tutorRepository;
        }

        public override async Task<Result<TutorProfileDto>> Handle(
            GetTutorProfileQuery query, CancellationToken cancellationToken)
        {
            //This task need to pull tutor data: basic information, class information, verification information, major information
            try
            {
                var tutor = await _tutorRepository.GetById(query
                    .id); // This query includes verification information, major information, requests getting class
                if (tutor is null)
                {
                    return Result.Fail(new NonExistTutorError());
                }

                return _mapper.Map<TutorProfileDto>(tutor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
