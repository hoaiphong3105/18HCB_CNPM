using System;

namespace RestaurantMng.WebApplication.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
    }
}
