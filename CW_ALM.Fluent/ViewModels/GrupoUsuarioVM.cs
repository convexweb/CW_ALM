using System.Text.Json.Serialization;

namespace CW_ALM.Fluent.ViewModels
{
    public class GrupoUsuarioVM
    {
        [JsonPropertyName("grupoUID")]
        public Guid GrupoUID { get; set; }
        [JsonPropertyName("usuarioUID")]
        public Guid UsuarioUID { get; set; }
        [JsonPropertyName("usuario")]
        public UsuarioVM Usuario { get; set; }
    }
}
