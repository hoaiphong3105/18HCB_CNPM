using RestaurantMng.Data.Interfaces;
using RestaurantMng.Service.User.Interfaces;
using System;
using RestaurantMng.Service.User.Models.Dtos;
using System.Collections.Generic;
using System.Linq;
using RestaurantMng.Data.Models;
using RestaurantMng.Service.Interfaces;

namespace RestaurantMng.Service.User.Implements
{
    public class GroupUserService : IGroupUserService
    {
        private IRepository<RestaurantMng.Data.Models.GroupUser, int> _groupUserRepository;
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="groupUserRepository"></param>
        /// <param name="unitOfWork"></param>
        public GroupUserService(IRepository<RestaurantMng.Data.Models.GroupUser, int> groupUserRepository,
            IUnitOfWork unitOfWork)
        {
            _groupUserRepository = groupUserRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResultModel<List<Data.Models.GroupUser>> GetAll()
        {
            var result = new ResultModel<List<Data.Models.GroupUser>>();
            try
            {
                var data = _groupUserRepository.FindAll()
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
        /// Save
        /// </summary>
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
