using LinkDev.Talabat.Core.Domain.Entities.Identity;

namespace DashBoard.Models.UserViewModels
{
    public class EditDeleteUserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<RoleCheckbox> Roles { get; set; }
    }
    public class RoleCheckbox
    {
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}
