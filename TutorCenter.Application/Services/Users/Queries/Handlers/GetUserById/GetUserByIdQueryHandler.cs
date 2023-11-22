using FluentResults;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Contracts.Users;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.Users.Repos;
using TutorCenter.Domain.Users;

namespace TutorCenter.Application.Services.Users.Queries.Handlers
{
    public class GetUserByIdQueryHandler : GetByIdQueryHandler<GetUserByIdQuery, UserDto>
    {

        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository,
            IMapper mapper) : base(mapper)
        {
            _userRepository = userRepository;
        }
        public override async Task<Result<UserDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                User? user = await _userRepository.GetById(query.ObjectId);
                if (user is null) { return Result.Fail(new NonExistUserError()); }
                UserDto result = _mapper.Map<UserDto>(user);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


}
