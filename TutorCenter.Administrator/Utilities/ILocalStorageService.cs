namespace TutorCenter.Administrator.Utilities;

public interface ILocalStorageService
{
    bool Exist(string key);
    Task ClearStorage(List<string> keys);
    Task<T?> GetStorageItem<T>(string key);
    Task SetStorageItem<T>(string key, T value);
}