using CW_ALM.Domain.Commands.Authentication;
using CW_ALM.Domain.Core;
using CW_ALM.Domain.Entities;

namespace CW_ALM.Domain.Repositories
{
    public interface IAuthenticationRepository : IScopedService
    {
        Task<Usuario> AuthenticateAsync(AuthenticateRequest model);
        string GenerateJwtToken(Usuario user, string secretPhrase);
    }
}
