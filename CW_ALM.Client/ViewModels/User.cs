namespace CW_ALM.Client.ViewModels
{
    public class User
    {
        public Guid UID { get; set; }
        public string Name { get; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
