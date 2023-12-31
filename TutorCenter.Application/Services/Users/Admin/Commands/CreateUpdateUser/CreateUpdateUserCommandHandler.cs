﻿using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorCenter.Application.Services.Courses.Commands;
using TutorCenter.Domain.Interfaces.Services;
using TutorCenter.Domain.NotificationConsts;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Admin.Commands.CreateUpdateUser;

public class CreateUpdateUserCommandHandler : IRequestHandler<CreateUpdateUserCommand, Result<bool>>
{
    private readonly IAppCache _cache;
    private readonly ICloudinaryFile _cloudinaryFile;
    private readonly ILogger<CreateUpdateUserCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public CreateUpdateUserCommandHandler(IUserRepository userRepository,
        ILogger<CreateUpdateUserCommandHandler> logger, IPublisher publisher,
        ICloudinaryFile cloudinaryFile,
        IMapper mapper, IAppCache cache, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _logger = logger;
        _publisher = publisher;
        _cloudinaryFile = cloudinaryFile;
        _mapper = mapper;
        _cache = cache;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(CreateUpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserByEmail(command.UserDto.Email);
            //Check if the user existed
            if (user is not null)
            {
                //Update user
                if (!string.IsNullOrWhiteSpace(command.FilePath))
                    command.UserDto.Image = _cloudinaryFile.UploadImage(command.FilePath);

                user.UpdateUserInformation(_mapper.Map<User>(command.UserDto));
                _logger.LogDebug("ready for updating!");
                if (await _unitOfWork.SaveChangesAsync() <= 0)
                    return Result.Fail($"Fail to update of user {user.Email}");

                return true;
            }

            //Create new user
            user = _mapper.Map<User>(command.UserDto);
            user.Password = "06C2754C5D9989201E0EEF0F3744ABE4";
            var entity = await _userRepository.Insert(user);
            if (await _unitOfWork.SaveChangesAsync() <= 0) return Result.Fail($"Fail to create of user {entity.Email}");
            var message = "New learner: " + entity.FirstName + " " + entity.LastName + " at " +
                          entity.CreationTime.ToLongDateString();
            await _publisher.Publish(new NewObjectCreatedEvent(entity.Id, message, NotificationEnum.Learner),
                cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            //throw new Exception("Error happens when user is adding or updating." + ex.Message);
            return Result.Fail("Error happens when user is adding or updating" + ex.Message);
        }
    }
}