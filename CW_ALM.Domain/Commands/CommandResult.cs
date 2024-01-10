using CW_ALM.Domain.Commands.Interfaces;

namespace CW_ALM.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult()
        {            
        }

        public CommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = new Dictionary<string, string>();
        }
        public CommandResult AddError(string fieldName, string message)
        {
            Errors.Add(fieldName, message);
            return this;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public IDictionary<string, string> Errors { get; }
    }
}
