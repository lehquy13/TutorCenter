using EduSmart.Domain.Repository;
using FluentResults;
using MapsterMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Courses.Queries.GetCourseQuery
{
    public class GetCourseQueryHandler : GetCourseQueryHandler<GetCourseQuery, CourseForDetailDto>
    {

        private readonly ICourseRepository _courseRepository;
        public GetCourseQueryHandler(ICourseRepository courseRepository,
            IMapper mapper) : base(mapper)
        {
            _courseRepository = courseRepository;                               
        }
        public override async Task<Result<CourseForDetailDto>> Handle(GetCourseQuery query, CancellationToken cancellationToken)
        {
            var classInformation = await _courseRepository.GetById(query.Id);
            if (classInformation == null)
            {
                return Result.Fail("The class doesn't exist");
            }
            var classDto = _mapper.Map<CourseForDetailDto>(classInformation);
            return classDto;
        }
    }
}
