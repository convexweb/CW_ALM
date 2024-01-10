using CW_ALM.Fluent.Common.Interfaces;
using CW_ALM.Fluent.Services.Interfaces;
using CW_ALM.Fluent.ViewModels;
using CW_ALM.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;

namespace CW_ALM.Fluent.Pages.Usuarios
{
    public partial class AlteraStatus
    {
        [Inject]
        protected IUsuarioService UsuarioService { get; set; } = default!;
        [Inject]
        protected IStringLocalizer<SharedResource_UI> Localizer { get; set; } = default!;
        [Inject]
        protected IJsonSerializerExtension JsonSerializerExtension { get; set; } = default!;
        [Inject]
        protected IToastService ToastService { get; set; } = default!;

        [Parameter]
        public UsuarioVM Content { get; set; } = default!;

        [CascadingParameter]
        public FluentDialog Dialog { get; set; } = default!;

        public bool Confirma { get; set; } = false;
        public string height { get; set; } = "height: 100px;";

        protected async Task AlteraStatusAsync()
        {
            Confirma = true;
            height = "height: 200px;";
            var response = new CommandResultVM();
            
            response = Content.Status ? await UsuarioService.InactivateAsync(Content) : await UsuarioService.ReactivateAsync(Content);
            if (response is not null && response.Success.Equals(true))
            {
                ToastService.ShowToast(ToastIntent.Success, response.Message);
                await Dialog.CloseAsync();
            }
            else
            {
                ToastService.ShowToast(ToastIntent.Error, response.Message);
            }
            Confirma = false;
            height = "height: 100px;";
        }

        protected async Task CloseAsync()
        {
            ToastService.ShowToast(ToastIntent.Info, Localizer["Resx_UI_OperacaoCancelada"]);
            await Dialog.CloseAsync(Content);
        }
    }
}
