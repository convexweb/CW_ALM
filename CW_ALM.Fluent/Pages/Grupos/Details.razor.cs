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
    public partial class Details
    {
        [Inject]
        protected IGrupoUsuarioService GrupoUsuarioService { get; set; } = default!;
        [Inject]
        protected IStringLocalizer<SharedResource_UI> Localizer { get; set; } = default!;
        [Inject]
        protected IJsonSerializerExtension JsonSerializerExtension { get; set; } = default!;
        
        [Parameter]
        public GrupoVM Content { get; set; } = default!;

        [CascadingParameter]
        public FluentDialog Dialog { get; set; } = default!;

        private IQueryable<GrupoUsuarioVM>? lstGruposUsuarios { get; set;} = Enumerable.Empty<GrupoUsuarioVM>().AsQueryable();
        public bool loading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            await Task.Delay(100);
            var response = await GrupoUsuarioService.GetByGrupoUIDAsync(Content.UID);
            if (response is not null && response.Success.Equals(true))
            {
                if (response.Data is not null)
                {
                    lstGruposUsuarios = JsonSerializerExtension.Converte<List<GrupoUsuarioVM>>((JsonElement)response.Data).AsQueryable();
                }
            }
            await Task.Delay(100);
            loading = false;
            StateHasChanged();
        }

        private async Task CloseAsync()
        {
            await Dialog.CancelAsync();
        }
    }
}
