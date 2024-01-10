using CW_ALM.Fluent.Helpers;
using CW_ALM.Fluent.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CW_ALM.Fluent.Common
{
    public class ServerValidator : ComponentBase
    {
        [CascadingParameter]
        EditContext CurrentEditContext { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (this.CurrentEditContext == null)
            {
                throw new InvalidOperationException($"{nameof(ServerValidator)} requires a cascading " +
                    $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(ServerValidator)} " +
                    $"inside an EditForm.");
            }
        }

        public async void Validate(CommandResultVM commandResult, object model)
        {
            try
            {
                var messages = new ValidationMessageStore(this.CurrentEditContext);
                
                if (commandResult.Success.Equals(false))
                {
                    if (commandResult.Errors != null)
                    {
                        messages.Clear();

                        foreach (var error in commandResult.Errors)
                        {
                            var fieldIdentifier = new FieldIdentifier(model, error.Key);
                            messages.Add(fieldIdentifier, error.Value);
                        }
                    }
                } else
                {
                    var returnUrl = NavigationManager.QueryString("returnUrl") ?? "/";
                    NavigationManager.NavigateTo(returnUrl, forceLoad: true);
                }
            } catch(Exception ex)
            {
                _ = ex.Message;
                throw new Exception("Erro!!!!");
            }

            CurrentEditContext.NotifyValidationStateChanged();
        }
    }
}
