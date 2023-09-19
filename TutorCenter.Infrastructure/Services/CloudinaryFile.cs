using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TutorCenter.Domain.Interfaces.Services;

namespace TutorCenter.Infrastructure.Services;

public class CloudinaryFile : ICloudinaryFile
{
    private readonly ILogger<CloudinaryFile> _logger;
    private Cloudinary Cloudinary { get; set; }

    public CloudinaryFile(IOptions<CloudinarySetting> cloudinarySetting, ILogger<CloudinaryFile> logger)
    {
        _logger = logger;

        Cloudinary = new Cloudinary(
            new Account(
                cloudinarySetting.Value.CloudName,
                cloudinarySetting.Value.ApiKey,
                cloudinarySetting.Value.ApiSecret
            ));
        Cloudinary.Api.Secure = true;
    }

    public string UploadImage(string filePath)
    {
        try
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filePath),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true
            };
            
            var uploadResult = Cloudinary.Upload(uploadParams);
            _logger.LogInformation(uploadResult.JsonObj.ToString());

            return uploadResult.Url.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return @"https://res.cloudinary.com/dhehywasc/image/upload/v1686121404/default_avatar2_ws3vc5.png";
        }
    }
    public string UploadImage(string filePath, Stream stream )
    {
        try
        {
            var paramss = new ImageUploadParams()
            {
                File = new FileDescription(filePath, stream),
                Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
            };
            var result = Cloudinary.Upload(paramss);
           return result.Url.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return default;
        }
    }

    public string GetImage(string fileName)
    {
        try
        {
            var getResourceParams = new GetResourceParams("fileName")
            {
                QualityAnalysis = true
            };
            var getResourceResult = Cloudinary.GetResource(getResourceParams);
            var resultJson = getResourceResult.JsonObj;

            // Log quality analysis score to the console
            _logger.LogInformation(resultJson["quality_analysis"]?.ToString());

            return resultJson["url"]?.ToString()??"https://res.cloudinary.com/dhehywasc/image/upload/v1686121404/default_avatar2_ws3vc5.png";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return @"https://res.cloudinary.com/dhehywasc/image/upload/v1686121404/default_avatar2_ws3vc5.png";
        }
    }
}