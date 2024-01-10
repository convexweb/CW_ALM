using Microsoft.Extensions.Localization;

namespace CW_ALM.Resources
{
    public interface ISharedResource_UI
    {
    }
    public class SharedResource_UI : ISharedResource_UI
    {
        private readonly IStringLocalizer _localizer;

        public SharedResource_UI(IStringLocalizer<SharedResource_UI> localizer)
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
