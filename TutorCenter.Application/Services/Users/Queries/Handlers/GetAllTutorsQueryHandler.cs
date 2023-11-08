using EduSmart.Domain.Repository;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts.Users.Tutors;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Users.Repos;
using TutorCenter.Domain.Users;
using FluentResults;
using TutorCenter.Application.Contracts;

namespace TutorCenter.Application.Services.Users.Queries.Handlers
{ 
    public class GetAllTutorsQueryHandler : GetAllQueryHandler<GetAllTutorsQuery, TutorForDetailDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<TutorMajor> _tutorMajorRepository;
        private readonly ISubjectRepository _subjectRepository;


        public GetAllTutorsQueryHandler(IUserRepository userRepository, ISubjectRepository subjectRepository,
            IRepository<TutorMajor> tutorMajorRepository,
            IMapper mapper):base(mapper) 
        {
            _subjectRepository = subjectRepository;

            _userRepository = userRepository;
            _tutorMajorRepository = tutorMajorRepository;

        }

        public async override Task<Result<PaginatedList<TutorForDetailDto>>> Handle(GetAllTutorsQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            throw new InvalidOperationException("No more using!!!");
        }
    }
}
