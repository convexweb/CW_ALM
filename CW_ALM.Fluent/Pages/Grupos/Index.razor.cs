using CW_ALM.Fluent.Common.Interfaces;
using CW_ALM.Fluent.Services.Interfaces;
using CW_ALM.Fluent.ViewModels;
using CW_ALM.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Text.Json;

namespace CW_ALM.Fluent.Pages.Grupos
{
    public partial class Index
    {
        [Inject]
        protected IGrupoService GrupoService { get; set; } = default!;
        [Inject]
        protected IStringLocalizer<SharedResource_UI> Localizer { get; set; } = default!;
        [Inject]
        protected IJsonSerializerExtension JsonSerializerExtension { get; set; } = default!;
        [Inject] 
        protected IDialogService DialogService { get; set; } = default!;


        private bool loading;
        private IQueryable<GrupoVM>? grupos;


        protected override async Task OnInitializedAsync()
        {
            loading = true;
            await CarregaListagemAsync();
            loading = false;
            StateHasChanged();
        }

        public async Task CarregaListagemAsync()
        {
            grupos = Enumerable.Empty<GrupoVM>().AsQueryable();
            var response = await GrupoService.GetAllAsync();
            if (response is not null && response.Success.Equals(true))
            {
                if (response.Data is not null)
                {
                    grupos = JsonSerializerExtension.Converte<List<GrupoVM>>((JsonElement)response.Data).AsQueryable();
                }
            }
        }
        public async Task OpenAddDialogAsync()
        {
            var data = new GrupoVM();

            var dialog = await DialogService.ShowDialogAsync<Add>(data, new DialogParameters()
            {
                Title = Localizer["Resx_UI_AdicionarGrupo"],
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

        public async Task OpenEditDialogAsync(GrupoVM data)
        {
            var dialog = await DialogService.ShowDialogAsync<Edit>(data, new DialogParameters()
            {
                Title = Localizer["Resx_UI_EditarGrupo"],
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

        public async Task OpenDetailsDialogAsync(GrupoVM data)
        {
            var dialog = await DialogService.ShowDialogAsync<Details>(data, new DialogParameters()
            {
                Title = Localizer["Resx_UI_VisualizarGrupo"],
                PreventDismissOnOverlayClick = true,
                PreventScroll = true,
                Width = "800px"
            });
        }

        private async Task OpenAtualizaStatusDialogAsync(GrupoVM data)
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
