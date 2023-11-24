
using MediatR;
using TutorCenter.Domain.Interfaces.Logger;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Subscribers;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.SubscribeRegistration.Commands;

public record EmailSubscriptionCommandHanler : IRequestHandler<EmailSubscriptionCommand, bool>
{
    private readonly IRepository<Subscriber> _subscriberRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAppLogger<EmailUnsubscriptionCommandHanlder> _logger;
    public EmailSubscriptionCommandHanler(IRepository<Subscriber> subscriberRepository, IUserRepository userRepository, IAppLogger<EmailUnsubscriptionCommandHanlder> logger)
    {
        _subscriberRepository = subscriberRepository;
        _userRepository = userRepository;
        _logger = logger;
    }
    
    public async Task<bool> Handle(EmailSubscriptionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var subscriber = await _userRepository.GetUserByEmail(command.Mail);
        if (subscriber == null)
        {
            _logger.LogError("User does not exist");
            return false;
        }
        
        var checkExisted = _subscriberRepository.GetAll()
            .FirstOrDefault(x => x.TutorId.Equals(subscriber.Id));
        if (checkExisted == null)
        {
            await _subscriberRepository.Insert(
                new()
                {
                    TutorId = subscriber.Id
                }
                );
            return true;
        }

        return false;
    }
}
