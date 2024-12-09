using StackExchange.Redis;

namespace DashBoard.Models.UserViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
