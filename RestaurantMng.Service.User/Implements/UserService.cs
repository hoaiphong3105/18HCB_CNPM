using RestaurantMng.Data.Interfaces;
using RestaurantMng.Service.User.Interfaces;
using System;
using RestaurantMng.Service.User.Models.Dtos;
using RestaurantMng.Service.User.Models.ViewModels;
using RestaurantMng.Core.Common;
using System.Configuration;

namespace RestaurantMng.Service.User.Implements
{
    public class UserService : IUserService
    {
        private IRepository<RestaurantMng.Data.Models.User, int> _userRepository;
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="unitOfWork"></param>
        public UserService(IRepository<RestaurantMng.Data.Models.User, int> userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ResultModel CheckLogin(string username, string password)
        {
            var result = new ResultModel();
            try
            {
                var data = _userRepository.FindSingle(x => x.UserName.Equals(username)
                            && x.Password.Equals(password) && x.IsActive == true);
                if (data != null)
                {
                    var infoLogin = new LoginDto();
                    infoLogin.UserID = data.ID;
                    infoLogin.Role = data.GroupUser.GroupName;
                    infoLogin.UserName = data.UserName;

                    result.Data = infoLogin;
                }
                else
                {
                    result.Code = -1;
                    result.Message = "Đăng nhập thất bại";
                }
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Đăng nhập thất bại";
            }
            return result;
        }

        /// <summary>
        /// Kiểm tra account đã tồn tại
        /// Dùng khi tạo mới nhân viên
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ResultModel CheckUserExist(string username)
        {
            var result = new ResultModel();

            try
            {
                bool isExist = _userRepository.FindSingle(x => x.UserName.Equals(username)) != null;
                if (isExist)
                {
                    result.Code = -1;
                    result.Message = "Tài khoản đã tồn tại";
                }
            }
            catch(Exception ex)
            {
                result.Code = -2;
                result.Message = "Tài khoản đã tồn tại";
            }
            return result;
        }

        /// <summary>
        /// Thêm mới nhân viên 
        /// </summary>
        /// <param name="userVM"></param>
        /// <returns></returns>
        public ResultModel CreateUser(UserViewModel userVM)
        {
            var result = new ResultModel();
            try
            {
                var user = new Data.Models.User();
                user.FullName = userVM.FullName;
                user.UserName = userVM.UserName;
                user.Address = userVM.Address;
                user.DateOfBirth = userVM.DateOfBirth;
                user.Phone = userVM.Phone;
                user.GroupID = userVM.GroupID;
                user.Password = Encryption.HashMD5(ConfigurationManager.AppSettings["DefaultPassword"], userVM.UserName);
                user.IsActive = true;

                _userRepository.Add(user);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thêm nhân viên thất bại!";
            }
            return result;
        }

        /// <summary>
        /// Save
        /// </summary>
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
