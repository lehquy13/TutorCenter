namespace TutorCenter.Domain.Interfaces.Services;

public interface ICloudinaryFile
{
    string GetImage(string fileName);
    string UploadImage(string filePath);
    string UploadImage(string filePath, Stream stream);

}