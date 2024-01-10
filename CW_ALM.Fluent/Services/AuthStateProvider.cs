using CW_ALM.Fluent.Common.Interfaces;
using CW_ALM.Fluent.Services.Interfaces;
using CW_ALM.Fluent.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace CW_ALM.Fluent.Services
{
    public class AuthStateProvider : AuthenticationStateProvider, IDisposable, IAuthStateProvider
    {
        private IHttpService _httpService;
        private IJsonSerializerExtension _jsonSerializerExtension;
        private ILocalStorageService _localStorageService;
        private NavigationManager _navigationManager;

        public UsuarioVM CurrentUser { get; set; } = new();

        public AuthStateProvider(
            IHttpService httpService,
            IJsonSerializerExtension jsonSerializerExtension,
            ILocalStorageService localStorageService,
            NavigationManager navigationManager
        )
        {
            _httpService = httpService;
            _jsonSerializerExtension = jsonSerializerExtension;
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
            AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        }

        public async Task Initialize()
        {
            CurrentUser = await _localStorageService.GetItem<UsuarioVM>("usuario");
        }

        public async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
        {
            var authenticationState = await task;

            if (authenticationState is not null)
            {
                CurrentUser = UsuarioVM.FromClaimsPrincipal(authenticationState.User);
            }
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();
            await FetchUserFromBrowserAsync();

            if (CurrentUser is not null && !CurrentUser.UID.Equals(Guid.Empty))
            {

                string token = CurrentUser.Token;
                var claimPrincipal = CreateClaimsPrincipalFromToken(token);
                var userFromToken = UsuarioVM.FromClaimsPrincipal(claimPrincipal);

                if (CurrentUser is not null && userFromToken is not null)
                {
                    if (
                        CurrentUser.UID.Equals(userFromToken.UID) &&
                        CurrentUser.UsuarioAD.Equals(userFromToken.UsuarioAD) &&
                        CurrentUser.Nome.Equals(userFromToken.Nome) &&
                        CurrentUser.Email.Equals(userFromToken.Email) &&
                        CurrentUser.Senha.Equals(userFromToken.Senha) &&
                        CurrentUser.Token.Equals(userFromToken.Token) &&
                        CurrentUser.Grupos.SequenceEqual(userFromToken.Grupos)
                    )
                    {
                        principal = CurrentUser.ToClaimsPrincipal();
                    }
                }
            }

            return new(principal);
        }

        public async Task<CommandResultVM> LoginAsync(string email, string password)
        {
            var principal = new ClaimsPrincipal();
            var response = await _httpService.PostAsync("/authentication/authenticate", new { email, password });
            await SendAuthenticateRequestAsync(response);
            if(CurrentUser is not null)
            {
                principal = CurrentUser.ToClaimsPrincipal();
            }
            
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
            return response;
        }

        public async Task SendAuthenticateRequestAsync(CommandResultVM response)
        {
            if (response.Success.Equals(true))
            {
                var User = _jsonSerializerExtension.Converte<UsuarioVM>((JsonElement)response.Data);
                CurrentUser = User;

                string token = User.Token;
                var claimPrincipal = CreateClaimsPrincipalFromToken(token);
                var user = UsuarioVM.FromClaimsPrincipal(claimPrincipal);
                await PersistUserToBrowserAsync();
            }
        }

        public void Logout()
        {
            ClearBrowserUserDataAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
        }

        public void Dispose() 
        {
            AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;
        }

        public async Task PersistUserToBrowserAsync()
        {
            await _localStorageService.SetItem("usuario", CurrentUser);
        }

        public async Task ClearBrowserUserDataAsync()
        {
            CurrentUser = null;
            await _localStorageService.RemoveItem("usuario");
            _navigationManager.NavigateTo(uri: "login", forceLoad: true);
        }

        public async Task FetchUserFromBrowserAsync()
        {
            CurrentUser = await _localStorageService.GetItem<UsuarioVM>("usuario");
        }
        private ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity();

            if (tokenHandler.CanReadToken(token))
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
                identity = new ClaimsIdentity(jwtSecurityToken.Claims, "CW_ALM_Roles");
                identity.AddClaim(new Claim("Token", token));
            }

            return new ClaimsPrincipal(identity);
        }
    }

    
}
