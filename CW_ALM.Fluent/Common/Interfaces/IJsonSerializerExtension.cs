using System.Text.Json;

namespace CW_ALM.Fluent.Common.Interfaces
{
    public interface IJsonSerializerExtension
    {
        T Converte<T>(JsonElement element);
    }
}
