using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Queries.CustomerQueries
{
    public class PopularTutorsQueryHandler : IRequestHandler<PopularTutorsQuery, List<TutorForDetailDto>>
    {
        private readonly ITutorRepository _tutorRepository;
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;

        public PopularTutorsQueryHandler(ITutorRepository tutorRepository,
            IMapper mapper,
            ICourseRepository courseRepository
        )
        {
            _tutorRepository = tutorRepository;
            _mapper = mapper;
            _courseRepository  = courseRepository;
        }

        public async Task<List<TutorForDetailDto>> Handle(PopularTutorsQuery request, CancellationToken cancellationToken)
        {
            var thisMonth = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                1
            );

            var topTutorIdWithClassCount = _courseRepository
                .GetAll()
                .Where(x => x.CreationTime >= thisMonth && x.Status == Status.Confirmed && x.TutorId != null && x.TutorId != null)
                .GroupBy(x => x.TutorId)
                .Select(x => new
                {
                    tutorId = x.Key,
                    classCount = x.Count()
                })
                .ToList();

            var tutorWithFullInfoList = await _tutorRepository.GetPopularTutors();
            var topTutorWithClassCount =
                tutorWithFullInfoList
                    .Join(
                        topTutorIdWithClassCount,
                        tutor => tutor.Id,
                        classesInMonth => classesInMonth.tutorId,
                        (tutor, cs) => new
                        {
                            user = tutor,
                            count = cs.classCount
                        }
                    );
            var tutors = topTutorWithClassCount
                .OrderByDescending(x => x.count)
                .Select(x => x.user)
                .Take(6).ToList();
            return _mapper.Map<List<TutorForDetailDto>>(tutors);
        }
    }
}
