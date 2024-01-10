using MediatR;
using System.Text.Json.Serialization;

namespace CW_ALM.Domain.Commands.Authentication
{
    public class AuthenticateRequest : IRequest<CommandResult>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        [JsonIgnore]
        public string? SecretPhrase { get; set; }
    }
}
