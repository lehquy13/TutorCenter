using FluentResults;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Contracts.Users.Learners;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Users.Repos;
using TutorCenter.Domain.Users;

namespace TutorCenter.Application.Services.Users.Queries.Handlers
{
    //Not using currently
    public class GetStudentByIdQueryHandler : GetByIdQueryHandler<GetStudentByIdQuery, LearnerDto>
    {
        private readonly IUserRepository _userRepository;
        public GetStudentByIdQueryHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
        {
            _userRepository = userRepository;
        }
        public override async Task<Result<LearnerDto>> Handle(GetStudentByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                User? user = await _userRepository.GetById(query.ObjectId);
                if (user is null) { return Result.Fail(new NonExistUserError()); }
                LearnerDto result = _mapper.Map<LearnerDto>(user);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
