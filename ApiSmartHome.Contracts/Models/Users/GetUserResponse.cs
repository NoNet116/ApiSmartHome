using ApiSmartHome.Data.Models;

namespace ApiSmartHome.Contracts.Models.Users
{
    public class GetUserResponse
    {
        public int UserAmount { get; set; }
        public UserView[] Users { get; set; }
    } 
    public class UserView(User user)
    {
        public Guid Id { get; set; } = user.Id;
        public string Email { get; set; } = user.Email;
        public string Password { get; set; } = user.Password;
        public string FirstName { get; set; } = user.FirstName;
        public string LastName { get; set; } = user.LastName;
        public string Role { get; set; } = user.Role.Name;
    }
    

}
