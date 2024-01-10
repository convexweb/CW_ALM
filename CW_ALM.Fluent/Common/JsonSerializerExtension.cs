using CW_ALM.Fluent.Common.Interfaces;
using System.Text.Json;

namespace CW_ALM.Fluent.Common
{
    public class JsonSerializerExtension : IJsonSerializerExtension
    {
        public T Converte<T>(JsonElement element)
        {
            var json = element.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
