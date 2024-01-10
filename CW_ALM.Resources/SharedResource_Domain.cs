using Microsoft.Extensions.Localization;

namespace CW_ALM.Resources
{
    public interface ISharedResource_Domain
    {
    }
    public class SharedResource_Domain : ISharedResource_Domain
    {
        private readonly IStringLocalizer _localizer;

        public SharedResource_Domain(IStringLocalizer<SharedResource_Domain> localizer)
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
