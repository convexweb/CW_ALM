using System.Text.Json.Serialization;

namespace CW_ALM.Fluent.ViewModels
{
    public class GrupoVM
    {
        [JsonPropertyName("uid")]
        public Guid UID { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        public string GenericError { get; set; }
        public bool Selected { get; set; } = false;

        public GrupoVM()
        {
        }

        public GrupoVM(Guid uID, string nome, bool status, bool? selected)
        {
            UID = uID;
            Nome = nome;
            Status = status;
            Selected = selected ?? false;
        }
    }
}
