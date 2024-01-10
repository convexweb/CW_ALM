using CW_ALM.Fluent.Common.Interfaces;
using CW_ALM.Fluent.Services.Interfaces;
using CW_ALM.Fluent.ViewModels;
using CW_ALM.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Text.Json;

namespace CW_ALM.Fluent.Pages.Usuarios
{
    public partial class Index
    {
        [Inject]
        protected IUsuarioService UsuarioService { get; set; } = default!;
        [Inject]
        protected IStringLocalizer<SharedResource_UI> Localizer { get; set; } = default!;
        [Inject]
        protected IJsonSerializerExtension JsonSerializerExtension { get; set; } = default!;
        [Inject] 
        protected IDialogService DialogService { get; set; } = default!;


        private bool loading;
        private IQueryable<UsuarioVM>? usuarios;


        protected override async Task OnInitializedAsync()
        {
            loading = true;
            await CarregaListagemAsync();
            loading = false;
            StateHasChanged();
        }

        public async Task CarregaListagemAsync()
        {
            usuarios = Enumerable.Empty<UsuarioVM>().AsQueryable();
            var response = await UsuarioService.GetAllAsync();
            if (response is not null && response.Success.Equals(true))
            {
                if (response.Data is not null)
                {
                    usuarios = JsonSerializerExtension.Converte<List<UsuarioVM>>((JsonElement)response.Data).AsQueryable();
                }
            }
        }
        public async Task OpenAddDialogAsync()
        {
            var data = new UsuarioVM();

            var dialog = await DialogService.ShowDialogAsync<Add>(data, new DialogParameters()
            {
                Title = Localizer["Resx_UI_AdicionarUsuario"],
                PreventDismissOnOverlayClick = true,
                PreventScroll = true,
            });

            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                loading = true;
                await CarregaListagemAsync();
                loading = false;
            }
        }

        public async Task OpenEditDialogAsync(UsuarioVM data)
        {
            var dialog = await DialogService.ShowDialogAsync<Edit>(data, new DialogParameters()
            {
                Title = Localizer["Resx_UI_EditarUsuario"],
                PreventDismissOnOverlayClick = true,
                PreventScroll = true,
            });

            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                loading = true;
                await CarregaListagemAsync();
                loading = false;
            }
        }

        public async Task OpenDetailsDialogAsync(UsuarioVM data)
        {
            var dialog = await DialogService.ShowDialogAsync<Details>(data, new DialogParameters()
            {
                Title = Localizer["Resx_UI_VisualizarUsuario"],
                PreventDismissOnOverlayClick = true,
                PreventScroll = true,
                Width = "800px"
            });
        }

        private async Task OpenAtualizaStatusDialogAsync(UsuarioVM data)
        {
            var dialog = await DialogService.ShowDialogAsync<AlteraStatus>(data, new DialogParameters()
            {
                PreventDismissOnOverlayClick = true,
                PreventScroll = true,
                Width = "500px",
            });
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                loading = true;
                await CarregaListagemAsync();
                loading = false;
            }
        }
    }
}
