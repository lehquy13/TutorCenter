using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TutorCenter.Domain.Interfaces.Services;
using TutorCenter.Domain.Repository;
using TutorCenter.Domain.Users;
using TutorCenter.Domain.Users.Repos;

namespace TutorCenter.Application.Services.Users.Tutor.ChangeInfo;

public class TutorInfoChangingCommandHandler : IRequestHandler<TutorInfoChangingCommand, Result<bool>>
{
    private readonly IAppCache _cache;
    private readonly ICloudinaryFile _cloudinaryFile;
    private readonly ILogger<TutorInfoChangingCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly ITutorRepository _tutorRepository;
    private readonly IRepository<TutorVerificationInfo> _tutorVerificationInfoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TutorInfoChangingCommandHandler(ITutorRepository tutorRepository,
        IRepository<TutorMajor> tutorMajorRepository,
        ICloudinaryFile cloudinaryFile,
        IRepository<TutorVerificationInfo> tutorVerificationInfoRepository,
        ILogger<TutorInfoChangingCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork, IAppCache cache,
        IPublisher publisher)
    {
        _tutorRepository = tutorRepository;
        _cloudinaryFile = cloudinaryFile;
        _tutorMajorRepository = tutorMajorRepository;
        _tutorVerificationInfoRepository = tutorVerificationInfoRepository;
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _cache = cache;
        _publisher = publisher;
    }

    public async Task<Result<bool>> Handle(TutorInfoChangingCommand command,
        CancellationToken cancellationToken)
    {
        //Check if the user existed
        var tutor = await _tutorRepository.GetById(command.TutorDto.Id);
        if (tutor is null) return Result.Fail<bool>("Tutor not found");

        var newMajorUpdate = command.SubjectIds.DistinctBy(x => x).ToList();
        var currentMajor = tutor.Subjects;

        // check the subject changes
        foreach (var major in currentMajor)
            if (!newMajorUpdate.Contains(major.Id))
                currentMajor.Remove(major);
            else
                newMajorUpdate.Remove(major.Id);

        foreach (var newMu in newMajorUpdate)
            await _tutorMajorRepository.Insert(new TutorMajor
            {
                TutorId = tutor.Id,
                SubjectId = newMu
            });

        //Handle filepath !!! Upgrade
        if (command.FilePaths.Count > 0)
        {
            //TODO: Check if the verifications are removed or not
            tutor.TutorVerificationInfos = new List<TutorVerificationInfo>();
            foreach (var i in command.FilePaths)
                tutor.TutorVerificationInfos.Add(new TutorVerificationInfo
                {
                    Image = _cloudinaryFile.UploadImage(i),
                    TutorId = tutor.Id
                });
        }

        if (await _unitOfWork.SaveChangesAsync() <= 0) return Result.Fail("Update failed");

        return true;
    }
}