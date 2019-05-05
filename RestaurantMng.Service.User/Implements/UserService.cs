using RestaurantMng.Core.Interfaces;
using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantMng.Service.User.Models.Dtos;

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
        public LoginDto CheckLogin(string username, string password)
        {
            var result = new LoginDto();
            var data = _userRepository.FindSingle(x => x.UserName.Equals(username)
                        && x.Password.Equals(password) && x.IsActive == true);
            if (data != null)
            {
                result.UserID = data.ID;
                result.IsLoginSuccess = true;
                result.Role = data.GroupUser.GroupName;
                result.UserName = data.UserName;
            }
            return result;
        }
    }
}
