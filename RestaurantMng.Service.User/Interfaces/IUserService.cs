using RestaurantMng.Service.User.Models.Dtos;
using RestaurantMng.Service.User.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMng.Service.User.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        ResultModel CheckLogin(string username, string password);

        /// <summary>
        /// Kiểm tra user đã tồn tại trong hệ thống
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        ResultModel CheckUserExist(string username);

        /// <summary>
        /// Thêm mới user
        /// </summary>
        /// <param name="userVM"></param>
        /// <returns></returns>
        ResultModel CreateUser(UserViewModel userVM);

        /// <summary>
        /// SaveChanges
        /// </summary>
        void Save();
    }
}