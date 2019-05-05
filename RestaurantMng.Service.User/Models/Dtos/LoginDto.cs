using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMng.Service.User.Models.Dtos
{
    public class LoginDto
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool IsLoginSuccess { get; set; }
    }
}
