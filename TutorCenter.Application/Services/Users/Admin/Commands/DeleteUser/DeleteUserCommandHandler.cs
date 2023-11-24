using FluentResults;
using LazyCache;
using MediatR;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Services.Courses.Commands;
using TutorCenter.Domain.NotificationConsts;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Admin.Commands.DeleteUser;

public class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, Result<bool>>
{
    private readonly IAppCache _cache;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IAppCache cache,
        IPublisher publisher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _cache = cache;
        _publisher = publisher;
    }

    public async Task<Result<bool>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        var user = await _userRepository.GetById(command.UserId);
        if (user is null) return Result.Fail(new NonExistUserError());

        //user.DeleterUserId
        user.DeletionTime = DateTime.Now;
        user.IsDeleted = true;

        if (await _unitOfWork.SaveChangesAsync() <= 0) return Result.Fail("Fail to delete user");
        var message = "Delete tutor: " + user.GetFullNAme();
        await _publisher.Publish(new NewObjectCreatedEvent(user.Id, message, NotificationEnum.Tutor),
            cancellationToken);

        return true;
    }
}