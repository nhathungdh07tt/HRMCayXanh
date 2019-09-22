using System.Collections.Generic;

namespace HRM.ViewModels.System
{
    public class UserViewModel
    {
        public long Id { set; get; }
        public string FullName { set; get; }
        public string BirthDay { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string UserName { set; get; }
        public string Address { get; set; }
        public string PhoneNumber { set; get; }
        public string Avatar { get; set; }        
        public string Gender { get; set; }

        public List<RoleViewModel> Roles { get; set; }

    }
}
