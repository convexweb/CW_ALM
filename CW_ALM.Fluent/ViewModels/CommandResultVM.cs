using System.Text.Json.Serialization;

namespace CW_ALM.Fluent.ViewModels
{
    public class CommandResultVM
    {
        [JsonPropertyName("success")]
        public bool? Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }

        [JsonPropertyName("errors")]
        public IDictionary<string, string> Errors { get; set; }
    }
}
