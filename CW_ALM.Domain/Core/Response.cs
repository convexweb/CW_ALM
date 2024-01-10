using System.Collections.ObjectModel;

namespace CW_ALM.Domain.Core
{
    public class Response
    {
        private readonly IDictionary<string, string> _messages = new Dictionary<string, string>();

        public IDictionary<string, string> Errors { get; }
        public object Result { get; }

        public Response() => Errors = new ReadOnlyDictionary<string, string>(_messages);

        public Response(object result) : this() => Result = result;

        public Response AddError(string fieldName, string message)
        {
            _messages.Add(fieldName, message);
            return this;
        }
    }
}
