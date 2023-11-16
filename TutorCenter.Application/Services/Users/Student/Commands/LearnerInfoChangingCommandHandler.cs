using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorCenter.Domain.Interfaces.Services;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Student.Commands;

public class LearnerInfoChangingCommandHandler : IRequestHandler<LearnerInfoChangingCommand, Result<bool>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<LearnerInfoChangingCommandHandler> _logger;
    private readonly ICloudinaryFile _cloudinaryFile;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;

    public LearnerInfoChangingCommandHandler(
        IUserRepository userRepository,
        ILogger<LearnerInfoChangingCommandHandler> logger,
        ICloudinaryFile cloudinaryFile,
        IMapper mapper,
        IPublisher publisher, 
        IUnitOfWork unitOfWork, 
        IAppCache cache) 
    {
        _userRepository = userRepository;
        _logger = logger;
        _cloudinaryFile = cloudinaryFile;
        _mapper = mapper;
        _publisher = publisher;
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<Result<bool>> Handle(LearnerInfoChangingCommand command,  CancellationToken cancellationToken) 
    {
        var user = await _userRepository.GetUserByEmail(command.LearnerDto.Email);
        //Check if the subject existed
        if (user is not null)
        {
            if (!string.IsNullOrWhiteSpace(command.FilePath))
            {
                command.LearnerDto.Image = _cloudinaryFile.UploadImage(command.FilePath);
            }

            var userFromDb = _mapper.Map<User>(command.LearnerDto);
            user.UpdateUserInformation(userFromDb);
            
            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return Result.Fail($"Fail to update of user {user.Email}");
            }    
            return true;
        }
        _logger.LogDebug("ready for creating!");

        user = _mapper.Map<User>(command.LearnerDto);

        var entity = await _userRepository.Insert(user);
        if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
        {
            return Result.Fail($"Fail to create of user {entity.Email}");
        } 
        return true;
    }
}

