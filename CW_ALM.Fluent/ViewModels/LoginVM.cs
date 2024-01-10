using System.ComponentModel.DataAnnotations;

namespace CW_ALM.Fluent.ViewModels
{
    public class LoginVM
    {
        public LoginVM()
        {
        }

        public LoginVM(string email, string password, string genericError)
        {
            Email = email;
            Password = password;
            GenericError = genericError;
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public string GenericError { get; set; }
    }
}
