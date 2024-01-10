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
    public partial class Edit
    {
        [Inject]
        protected IUsuarioService UsuarioService { get; set; } = default!;
        [Inject]
        protected IGrupoService GrupoService { get; set; } = default!;
        [Inject]
        protected IGrupoUsuarioService GrupoUsuarioService { get; set; } = default!;
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

        IEnumerable<Option<string>>? selectedGrupos { get; set; }
        static List<Option<string>> lstGrupos { get; set; } = [];

        protected override void OnInitialized()
        {
            Content.Senha = string.Empty;
            editContext = new EditContext(Content);
        }

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            var grupos = new List<GrupoVM>();
            var gruposUsuarios = new List<GrupoUsuarioVM>();
            var response = await GrupoService.GetAllAsync();
            if (response is not null && response.Success.Equals(true))
            {
                if (response.Data is not null)
                {
                    grupos = JsonSerializerExtension.Converte<List<GrupoVM>>((JsonElement)response.Data);
                }
            }
            response = await GrupoUsuarioService.GetByUsuarioUIDAsync(Content.UID);
            if (response is not null && response.Success.Equals(true))
            {
                if (response.Data is not null)
                {
                    gruposUsuarios = JsonSerializerExtension.Converte<List<GrupoUsuarioVM>>((JsonElement)response.Data);
                }
            }
            editContext = null;
            selectedGrupos = null;
            lstGrupos = null;
            await Task.Delay(50);
            lstGrupos = [];
            foreach (var grupo in grupos.OrderBy(a => a.Nome).ToList())
            {
                if (gruposUsuarios.Any(a => a.GrupoUID.Equals(grupo.UID)))
                {
                    lstGrupos.Add(new Option<string> { Value = grupo.UID.ToString(), Text = grupo.Nome, Selected = gruposUsuarios.Any(a => a.GrupoUID.Equals(grupo.UID)) });
                    //auxLstGrupos.Add(new Option<string> { Value = grupo.UID.ToString(), Text = grupo.Nome, Selected = gruposUsuarios.Any(a => a.GrupoUID.Equals(grupo.UID)) });
                }
                else
                {
                    lstGrupos.Add(new Option<string> { Value = grupo.UID.ToString(), Text = grupo.Nome });
                }
            }
            await Task.Delay(50);            
            editContext = new EditContext(Content);
            loading = false;
        }


        private async Task SaveAsync()
        {
            loading = true;
            try
            {
                Content.LstGrupos = [];
                var teste4 = lstGrupos;
                var teste5 = selectedGrupos;

                foreach (var selectedGrupo in selectedGrupos.GroupBy(a => new { a.Value }).Select(b => new { b.Key.Value }).ToList())
                {
                    Content.LstGrupos.Add(Guid.Parse(selectedGrupo.Value.ToString()));
                }
                var response = await UsuarioService.EditAsync(Content);
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
                        foreach (var selectedGrupo in selectedGrupos.GroupBy(a => new { a.Value }).Select(b => new { b.Key.Value }).ToList())
                        {
                            if (lstGrupo.Value.Trim().ToLower().Equals(selectedGrupo.Value.Trim().ToLower()))
                            {
                                lstGrupo.Selected = true;
                            }
                        }
                        auxLstGrupos.Add(lstGrupo);
                    }
                    await Task.Delay(50);
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
