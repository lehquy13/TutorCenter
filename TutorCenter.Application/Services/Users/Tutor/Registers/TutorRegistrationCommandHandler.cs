﻿
using EduSmart.Domain.Repository;
using FluentResults;
using LazyCache;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using TutorCenter.Application.Common.Errors.User;
using TutorCenter.Application.Services.Abstractions.CommandHandlers;
using TutorCenter.Domain;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Courses.Repos;
using TutorCenter.Domain.Interfaces.Services;
using TutorCenter.Domain.Users;
using TutorCenter.Domain.Users.Repos;

namespace CED.Application.Services.Users.Tutor.Registers;

public class TutorRegisterCommandHandler : IRequestHandler<TutorRegistrationCommand,Result<bool>>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TutorRegisterCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IAppCache _cache;
    private readonly IPublisher _publisher;
    private readonly IRepository<TutorVerificationInfo> _tutorVerificationInfoRepository;
    private readonly IRepository<TutorMajor> _tutorMajorInfoRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICloudinaryFile _cloudinaryFile;

    public TutorRegisterCommandHandler(IUserRepository userRepository,
        IRepository<TutorVerificationInfo> tutorVerificationInfoRepository,
        IRepository<TutorMajor> tutorMajorInfoRepository,
        ICloudinaryFile cloudinaryFile,
        ITutorRepository tutorRepository,
        IUnitOfWork unitOfWork,
        ILogger<TutorRegisterCommandHandler> logger,
        IMapper mapper, IAppCache cache, IPublisher publisher, ISubjectRepository subjectRepository)
    {
        _userRepository = userRepository;
        _tutorRepository = tutorRepository;
        this._unitOfWork = unitOfWork;
        this._logger = logger;
        this._mapper = mapper;
        this._cache = cache;
        this._publisher = publisher;
        _tutorMajorInfoRepository = tutorMajorInfoRepository;
        _cloudinaryFile = cloudinaryFile;
        _tutorVerificationInfoRepository = tutorVerificationInfoRepository;
        _subjectRepository = subjectRepository;
    }

    public async Task<Result<bool>> Handle(TutorRegistrationCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            //Check if the user existed
            //TODO: Check if the logic goes well

            var user = await _userRepository.GetById(command.TutorForRegistrationDto.Id);

            if (user is null)
            {
                return Result.Fail(new NonExistUserError());
            }
            
            var tutor = _mapper.Map<TutorCenter.Domain.Users.Tutor>(command.TutorForRegistrationDto);
            tutor.Role = UserRole.Tutor;
            tutor.Description = command.TutorForRegistrationDto.Description;
            tutor.University = command.TutorForRegistrationDto.University;
            tutor.AcademicLevel = command.TutorForRegistrationDto.AcademicLevel.ToEnum<AcademicLevel>();
            tutor.IsVerified = false;

             _userRepository.Delete(user);
            var tutorToRegister = await _tutorRepository.Insert(tutor); 

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return Result.Fail("Fail to register new tutor");
            }
            
            foreach (var i in command.TutorForRegistrationDto.TutorVerificationInfoDtos)
            {
                i.Image = _cloudinaryFile.UploadImage(i.Image);
                var tutorVerificationInfo = _mapper.Map<TutorVerificationInfo>(i);
                await _tutorVerificationInfoRepository.Insert(tutorVerificationInfo);
            }

            //Handle major


            if (command.TutorForRegistrationDto.Majors is { Count: > 0 })
            {
                foreach (var i in command.TutorForRegistrationDto.Majors)
                {
                    var s = await _subjectRepository.GetSubjectByName(i);

                    if (s == null)
                    {
                        continue;
                    }

                    await _tutorMajorInfoRepository.Insert(new TutorMajor()
                    {
                        SubjectId = s.Id,
                        TutorId = tutorToRegister.Id
                    });
                }
            }


            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return Result.Fail("Fail to save majors and verification info");
            }

            _logger.LogInformation("Done");
            return true;
        }
        catch (Exception e)
        {
            return Result.Fail("Error while registering tutor. Details error: " + e.Message);
        }
    }
}