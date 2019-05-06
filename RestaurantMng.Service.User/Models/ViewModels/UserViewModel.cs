using System;

namespace RestaurantMng.Service.User.Models.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public int GroupID { get; set; }
    }
}
