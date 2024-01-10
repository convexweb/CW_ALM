using System.Security.Claims;
using System.Text.Json.Serialization;

namespace CW_ALM.Fluent.ViewModels
{
    public class UsuarioVM
    {
        [JsonPropertyName("uid")]
        public Guid UID { get; set; }
        [JsonPropertyName("usuarioAD")]
        public string UsuarioAD { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("senha")]
        public string Senha { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("grupos")]
        public List<string> Grupos { get; set; } = [];
        public List<Guid> LstGrupos { get; set; } = [];


        public string GenericError { get; set; }

        public UsuarioVM()
        {
        }

        public UsuarioVM(Guid uID, string usuarioAD, string nome, string email, string token, bool status, List<string> grupos)
        {
            UID = uID;
            UsuarioAD = usuarioAD;
            Nome = nome;
            Email = email;
            Token = token;
            Status = status;
            Grupos = grupos;
        }

        public ClaimsPrincipal ToClaimsPrincipal()
        {
            return new(new ClaimsIdentity(new Claim[]
            {
                new ("UID", UID.ToString()),
                new ("UsuarioAD", UsuarioAD),
                new ("Nome", Nome),
                new ("Email", Email),
                new ("Senha", Senha),
                new ("Token", Token),
                new ("Status", Status.ToString()),
            }.Concat(Grupos.Select(r => new Claim(ClaimTypes.Role, r)).ToArray()), "CW_ALM_Roles"));
        }

        public static UsuarioVM FromClaimsPrincipal(ClaimsPrincipal principal)
        {
            string strUID = principal.FindFirst(a => a.Type.Trim().ToLower().Equals("UID".Trim().ToLower()))?.Value ?? Guid.Empty.ToString();
            string strStatus = principal.FindFirst(a => a.Type.Trim().ToLower().Equals("Status".Trim().ToLower()))?.Value ?? false.ToString();
            var teste = bool.Parse(strStatus);
            return new()
            {
                UID = Guid.Parse(strUID),
                UsuarioAD = principal.FindFirst(a => a.Type.Trim().ToLower().Equals("UsuarioAD".Trim().ToLower()))?.Value ?? string.Empty.ToString(),
                Nome = principal.FindFirst(a => a.Type.Trim().ToLower().Equals("Nome".Trim().ToLower()))?.Value ?? string.Empty.ToString(),
                Email = principal.FindFirst(a => a.Type.Trim().ToLower().Equals("Email".Trim().ToLower()))?.Value ?? string.Empty.ToString(),
                Senha = principal.FindFirst(a => a.Type.Trim().ToLower().Equals("Senha".Trim().ToLower()))?.Value ?? string.Empty.ToString(),
                Token = principal.FindFirst(a => a.Type.Trim().ToLower().Equals("Token".Trim().ToLower()))?.Value ?? string.Empty.ToString(),
                Status = bool.Parse(strStatus),
                Grupos = principal.FindAll(a => a.Type.Trim().ToLower().Equals("Role".Trim().ToLower())).Select(c => c.Value).ToList()
            };
        }
    }
}
