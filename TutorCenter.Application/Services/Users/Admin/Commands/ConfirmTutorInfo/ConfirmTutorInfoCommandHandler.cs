﻿using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Admin.Commands.ConfirmTutorInfo;

public class ConfirmTutorInfoCommandHandler : IRequestHandler<ConfirmTutorInfoCommand, Result<bool>>
{
    private readonly IAppCache _cache;
    private readonly ILogger<ConfirmTutorInfoCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;

    private readonly ITutorRepository _userRepository;

    //TODO: đổi IUnitOfWork namespace
    public ConfirmTutorInfoCommandHandler(ITutorRepository userRepository,
        ILogger<ConfirmTutorInfoCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork, IPublisher publisher,
        IAppCache cache)
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
        _cache = cache;
    }

    public async Task<Result<bool>> Handle(ConfirmTutorInfoCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        var user = await _userRepository.GetUserByEmail(command.TutorForDetailDto.Email);
        if (user is null) throw new Exception("Tutor with an email doesn't exist");
        //if (user.Role != UserRole.Tutor) return false;

        command.TutorForDetailDto.IsVerified = true;
        user.UpdateTutorInformation(_mapper.Map<Domain.Users.Tutor>(command.TutorForDetailDto));

        var afterUpdatedUser = _userRepository.Update(user);

        if (afterUpdatedUser is null) return false;

        return true;
    }
}