using CW_ALM.Domain.Entities;

namespace CW_ALM.Domain.Commands.Authentication
{
    public class AuthenticateResponse
    {
        public Guid UID { get; set; }
        public string UsuarioAD { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Status { get; set; }
        public List<string> Grupos { get; set; } = [];
        public string Token { get; set; }


        public AuthenticateResponse(Usuario usuario, string token)
        {
            UID = usuario.UID;
            UsuarioAD = usuario.UsuarioAD;
            Nome = usuario.Nome;
            Email = usuario.Email;
            Senha = usuario.Senha;
            Status = usuario.Status;
            Grupos = usuario.LstGruposUsuarios.Select(r => r.Grupo.Nome).ToList();
            Token = token;
        }
    }
}
