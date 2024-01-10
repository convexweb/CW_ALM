using CW_ALM.Domain.Commands.Authentication;
using CW_ALM.Domain.Entities;
using CW_ALM.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CW_ALM.Infra.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IUsuarioRepository _usuarioService;

        public AuthenticationRepository(IUsuarioRepository usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<Usuario> AuthenticateAsync(AuthenticateRequest model)
        {
            Usuario user = await _usuarioService.GetUsuarioByEmailSenhaAsync(email: model.Email, senha: model.Password);

            // return null if user not found
            if (user == null) return null;

            return user;
        }

        public string GenerateJwtToken(Usuario usuario, string secretPhrase)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretPhrase);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new ("UID", usuario.UID.ToString()),
                    new ("UsuarioAD", usuario.UsuarioAD),
                    new ("Nome", usuario.Nome),
                    new ("Email", usuario.Email),
                    new ("Senha", usuario.Senha),
                    new ("Status", usuario.Status.ToString()),
                    new ("SenhaAtualizada", usuario.SenhaAtualizada.ToString()),
                }.Concat(usuario.LstGruposUsuarios.Select(r => new Claim(ClaimTypes.Role, r.Grupo.Nome)).ToArray()), "CW_ALM_Roles"),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
