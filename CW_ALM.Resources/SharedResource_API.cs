using Microsoft.Extensions.Localization;

namespace CW_ALM.Resources
{
    public interface ISharedResource_API
    {
    }
    public class SharedResource_API : ISharedResource_API
    {
        private readonly IStringLocalizer _localizer;

        public SharedResource_API(IStringLocalizer<SharedResource_API> localizer)
        {
            _localizer = localizer;
        }

        public string this[string index]
        {
            get
            {
                return _localizer[index];
            }
        }
    }
}
