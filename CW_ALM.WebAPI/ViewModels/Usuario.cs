using System.Text.Json.Serialization;

namespace CW_ALM.WebAPI.ViewModels
{
    public class Usuario
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
        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}
