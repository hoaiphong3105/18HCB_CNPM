using RestaurantMng.Data.Interfaces;
using RestaurantMng.Service.User.Interfaces;
using System;
using RestaurantMng.Service.User.Models.Dtos;
using System.Collections.Generic;
using System.Linq;

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
        /// Thay đổi mật khẩu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ResultModel<NullModel> ChangePassword(int id, string password)
        {
            var result = new ResultModel<NullModel>();
            try
            {
                var userEntity = _userRepository.FindById(id);
                if (userEntity == null)
                {
                    result.Code = -1;
                    result.Message = "Nhân viên không tồn tại!";
                }
                else
                {
                    userEntity.Password = password;
                    _userRepository.Update(userEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thay đổi mật khẩu thất bại";
            }
            return result;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ResultModel<LoginDto> CheckLogin(string username, string password)
        {
            var result = new ResultModel<LoginDto>();
            try
            {
                var data = _userRepository.FindSingle(x => x.UserName.Equals(username)
                            && x.Password.Equals(password) && x.Status == true);
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
        public ResultModel<NullModel> CheckUserExist(string username)
        {
            var result = new ResultModel<NullModel>();

            try
            {
                bool isExist = _userRepository.FindSingle(x => x.UserName.Equals(username)) != null;
                if (isExist)
                {
                    result.Code = -1;
                    result.Message = "Tài khoản đã tồn tại";
                }
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Tài khoản đã tồn tại";
            }
            return result;
        }

        /// <summary>
        /// Thêm mới nhân viên 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultModel<int> CreateUser(Data.Models.User user)
        {
            var result = new ResultModel<int>();
            try
            {
                var checkUser = CheckUserExist(user.UserName);
                if (checkUser.Code == 1)
                {
                    result.Code = checkUser.Code;
                    result.Message = checkUser.Message;
                }
                else
                {
                    _userRepository.Add(user);
                    _unitOfWork.Commit();

                    result.Data = user.ID;
                }
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thêm nhân viên thất bại!";
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResultModel<List<Data.Models.User>> GetAllUser()
        {
            var result = new ResultModel<List<Data.Models.User>>();
            try
            {
                var data = _userRepository.FindAll(x => x.Status == false)
                    .ToList();
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thất bại";
            }

            return result;
        }

        /// <summary>
        /// Xóa user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultModel<NullModel> RemoveUser(int id)
        {
            var result = new ResultModel<NullModel>();
            try
            {
                var userEntity = _userRepository.FindById(id);
                if (userEntity == null)
                {
                    result.Code = -1;
                    result.Message = "Nhân viên không tồn tại";
                }
                else
                {
                    userEntity.Status = false;
                    _userRepository.Update(userEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Xóa nhân viên thất bại!";
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
