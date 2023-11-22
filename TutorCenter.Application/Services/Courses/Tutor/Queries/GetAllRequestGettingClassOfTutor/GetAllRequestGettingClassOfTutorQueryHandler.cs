using FluentResults;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Courses.Tutor.Queries.GetAllRequestGettingClassOfTutor
{

        public class
        GetAllRequestGettingClassOfTutorQueryHandler : GetAllQueryHandler<GetAllRequestGettingClassOfTutorQuery,
            RequestGettingClassForListDto>
        {
            private readonly ICourseRepository _courseRepository;
            private readonly IUserRepository _userRepository;

            public GetAllRequestGettingClassOfTutorQueryHandler(
                ICourseRepository courseRepository,
                IUserRepository userRepository,
                IMapper mapper) : base(mapper)
            {
                _courseRepository = courseRepository;
                _userRepository = userRepository;
            }

            public override async Task<Result<PaginatedList<RequestGettingClassForListDto>>> Handle(
                GetAllRequestGettingClassOfTutorQuery query, CancellationToken cancellationToken)
            {
                await Task.CompletedTask;
                try
                {
                    //Check if user exists or not
                    var tutor = await _userRepository.GetById(query.ObjectId);
                    if (tutor is null)
                    {
                        return Result.Fail(new NonExistUserError());
                    }
                    // Get all the requests of the user
                    var teachingClassRequestsFromDb = await _courseRepository.GetAllCourseRequestsByTutorId(query.ObjectId);
                    var teachingClassRequestDtos = _mapper.Map<List<RequestGettingClassForListDto>>(teachingClassRequestsFromDb);

                    var resultPaginatedList = PaginatedList<RequestGettingClassForListDto>.CreateAsync(teachingClassRequestDtos,
                        query.PageIndex, query.PageSize, teachingClassRequestDtos.Count);

                    return resultPaginatedList;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    
}
