namespace CW_ALM.Fluent.Services.Interfaces
{
    public interface ILocalStorageService : IScopedService
    {
        Task<T> GetItem<T>(string key);
        Task SetItem<T>(string key, T value);
        Task RemoveItem(string key);
    }
}
