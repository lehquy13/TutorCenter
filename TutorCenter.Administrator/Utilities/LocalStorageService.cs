
using Hanssens.Net;

namespace TutorCenter.Web.Utilities;

public class LocalStorageServiceService : ILocalStorageService
{
    private readonly LocalStorage _localStorage;

    public LocalStorageServiceService()
    {
        LocalStorageConfiguration config = new()
        {
            AutoLoad = true,
            AutoSave = true,
            Filename = "CED.Web"
        };
        _localStorage = new LocalStorage(config);
    }
    public LocalStorageServiceService(LocalStorage localStorage)
    {
    _localStorage = localStorage;
    }

    public async Task ClearStorage(List<string> keys)
    {
        await Task.CompletedTask;
        foreach (var key in keys)
        {
            _localStorage.Remove(key);
        }
    }

    public async Task<T?> GetStorageItem<T>(string key)
    {
        await Task.CompletedTask;
        return (_localStorage.Get<T>(key));
    }
    public bool Exist(string key)
    {
        return _localStorage.Exists(key);
    }

    public async Task SetStorageItem<T>(string key, T value)
    {
        await Task.CompletedTask;
        if (value != null)
        {
            _localStorage.Store(key, value);
            _localStorage.Persist();
        }
    }
}