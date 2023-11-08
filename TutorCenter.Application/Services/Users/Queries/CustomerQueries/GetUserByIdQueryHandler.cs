using FluentResults;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Contracts.Users;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Users.Repos;
using TutorCenter.Domain.Users;
using TutorCenter.Application.Common.Errors.User;

namespace TutorCenter.Application.Services.Users.Queries.CustomerQueries
{
    public class GetUserByIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<UserForDetailDto>, UserForDetailDto>
    {

        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository,
            IMapper mapper) : base(mapper)
        {
            _userRepository = userRepository;
        }
        public override async Task<Result<UserForDetailDto>> Handle(GetObjectQuery<UserForDetailDto> query, CancellationToken cancellationToken)
        {
            try
            {
                User? user = await _userRepository.GetById(query.ObjectId);
                if (user is null) { return Result.Fail(new NonExistUserError()); }
                UserForDetailDto result = _mapper.Map<UserForDetailDto>(user);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
