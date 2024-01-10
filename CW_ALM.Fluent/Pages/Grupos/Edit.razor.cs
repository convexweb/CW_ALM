using CW_ALM.Fluent.Common;
using CW_ALM.Fluent.Common.Interfaces;
using CW_ALM.Fluent.Services.Interfaces;
using CW_ALM.Fluent.ViewModels;
using CW_ALM.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;

namespace CW_ALM.Fluent.Pages.Grupos
{
    public partial class Edit
    {
        [Inject]
        protected IGrupoService GrupoService { get; set; } = default!;
        [Inject]
        protected IStringLocalizer<SharedResource_UI> Localizer { get; set; } = default!;
        [Inject]
        protected IJsonSerializerExtension JsonSerializerExtension { get; set; } = default!;
        [Inject]
        protected ServerValidator ServerValidator { get; set; } = default!;
        [Inject]
        protected IToastService ToastService { get; set; } = default!;

        [Parameter]
        public GrupoVM Content { get; set; } = default!;

        [CascadingParameter]
        public FluentDialog Dialog { get; set; } = default!;

        private EditContext editContext;
        private bool loading;
        private string error;

        IEnumerable<Option<string>>? selectedGrupos { get; set; }
        static List<Option<string>> lstGrupos { get; set; } = [];

        protected override void OnInitialized()
        {
            editContext = new EditContext(Content);
        }

        private async Task SaveAsync()
        {
            loading = true;
            try
            {
                var response = await GrupoService.EditAsync(Content);
                if (response is not null && response.Success.Equals(true))
                {
                    ToastService.ShowToast(ToastIntent.Success, response.Message);
                    await Dialog.CloseAsync();
                }
                else
                {
                    ServerValidator.Validate(response, Content);
                }
            }
            catch
            {
            }
            loading = false;
        }

        private async Task CancelAsync()
        {
            ToastService.ShowToast(ToastIntent.Info, Localizer["Resx_UI_OperacaoCancelada"]);
            await Dialog.CancelAsync();
        }
    }
}
