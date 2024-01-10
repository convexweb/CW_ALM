namespace CW_ALM.Client.Services.Interfaces
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string uri);
        Task<T> PostAsync<T>(string uri, object value);
    }
}
