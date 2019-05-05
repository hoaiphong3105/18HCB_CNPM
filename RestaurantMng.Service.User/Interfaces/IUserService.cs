using RestaurantMng.Service.User.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMng.Service.User.Interfaces
{
    public interface IUserService
    {
        LoginDto CheckLogin(string username, string password);
    }
}
