using CW_ALM.Fluent.Common;
using CW_ALM.Fluent.Services.Interfaces;
using CW_ALM.Fluent.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CW_ALM.Fluent.Services
{
    public class HttpService : IHttpService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private ServerValidator _serverValidator;
        private ILocalStorageService _localStorageService;
        private IConfiguration _configuration;
        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ServerValidator serverValidator,
            ILocalStorageService localStorageService,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _serverValidator = serverValidator;
            _localStorageService = localStorageService;
            _configuration = configuration;
        }

        public async Task<CommandResultVM> GetAsync(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await sendRequestAsync(request);
        }

        public async Task<CommandResultVM> PostAsync(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequestAsync(request);
        }

        public async Task<CommandResultVM> PutAsync(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequestAsync(request);
        }

        // helper methods

        private async Task<CommandResultVM> sendRequestAsync(HttpRequestMessage request)
        {
            // add jwt auth header if user is logged in and request is to the api url
            var user = await _localStorageService.GetItem<UsuarioVM>("usuario");
            var isApiUrl = !request.RequestUri.IsAbsoluteUri;
            if (user != null && isApiUrl)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

            var language = await _localStorageService.GetItem<string>("culture");
            language = language is not null ? language : "en-US";
            request.Headers.Add("Accept-Language", language);

            using var response = await _httpClient.SendAsync(request);

            // auto logout on 401 response
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }

            return await response.Content.ReadFromJsonAsync<CommandResultVM>();
        }
    }
}
