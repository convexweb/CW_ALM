using CW_ALM.Fluent.Common;
using CW_ALM.Fluent.Common.Interfaces;
using CW_ALM.Fluent.Services.Interfaces;
using CW_ALM.Fluent.ViewModels;
using CW_ALM.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Text.Json;

namespace CW_ALM.Fluent.Pages.Usuarios
{
    public partial class Add
    {
        [Inject]
        protected IUsuarioService UsuarioService { get; set; } = default!;
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
        public UsuarioVM Content { get; set; } = default!;

        [CascadingParameter]
        public FluentDialog Dialog { get; set; } = default!;

        private EditContext editContext;
        private bool loading;
        private string error;

        IEnumerable<Option<string>>? selectedGrupos { get; set; } = Enumerable.Empty<Option<string>>();
        static List<Option<string>> lstGrupos { get; set; } = [];
        protected override void OnInitialized()
        {
            // redirect to home if already logged in
            Content = new();
            editContext = new EditContext(Content);
        }

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            var grupos = new List<GrupoVM>();
            var response = await GrupoService.GetAllAsync();
            if (response is not null && response.Success.Equals(true))
            {
                if (response.Data is not null)
                {
                    grupos = JsonSerializerExtension.Converte<List<GrupoVM>>((JsonElement)response.Data);
                }
            }
            lstGrupos = [];
            foreach(var grupo in grupos.OrderBy(a => a.Nome).ToList())
            {
                lstGrupos.Add(new Option<string> { Value = grupo.UID.ToString(), Text = grupo.Nome });
            }
            loading = false;
        }

        private async Task SaveAsync()
        {
            loading = true;
            StateHasChanged();
            editContext = new EditContext(Content);
            try
            {
                Content.LstGrupos = [];
                foreach (var selectedGrupo in selectedGrupos)
                {
                    Content.LstGrupos.Add(Guid.Parse(selectedGrupo.Value.ToString()));
                }
                var response = await UsuarioService.CreateAsync(Content);
                if (response is not null && response.Success.Equals(true))
                {
                    ToastService.ShowToast(ToastIntent.Success, response.Message);
                    await Dialog.CloseAsync();
                }
                else
                {
                    var auxLstGrupos = new List<Option<string>>();
                    foreach (var lstGrupo in lstGrupos)
                    {
                        lstGrupo.Selected = false;
                        foreach (var selectedGrupo in selectedGrupos)
                        {
                            if(lstGrupo.Value.Trim().ToLower().Equals(selectedGrupo.Value.Trim().ToLower()))
                            {
                                lstGrupo.Selected = true;
                            }
                        }
                        auxLstGrupos.Add(lstGrupo);
                    }
                    lstGrupos = auxLstGrupos;
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
