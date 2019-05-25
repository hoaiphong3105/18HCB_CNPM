using RestaurantMng.Service.User.Models.Dtos;
using System.Collections.Generic;

namespace RestaurantMng.Service.User.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// get all
        /// </summary>
        /// <returns></returns>
        ResultModel<List<Data.Models.User>> GetAllUser();
        
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        ResultModel<LoginDto> CheckLogin(string username, string password);

        /// <summary>
        /// Kiểm tra user đã tồn tại trong hệ thống
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        ResultModel<NullModel> CheckUserExist(string username);

        /// <summary>
        /// Thêm mới user
        /// </summary>
        /// <param name="userVM"></param>
        /// <returns></returns>
        ResultModel<int> CreateUser(Data.Models.User userEntity);

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        ResultModel<NullModel> ChangePassword(int id, string password);

        /// <summary>
        /// Xóa user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        ResultModel<NullModel> RemoveUser(int id);

        /// <summary>
        /// SaveChanges
        /// </summary>
        void Save();
    }
}