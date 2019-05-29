using RestaurantMng.Service.User.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMng.Service.User.Interfaces
{
    public interface IGroupUserService
    {
        /// <summary>
        /// get all
        /// </summary>
        /// <returns></returns>
        ResultModel<List<Data.Models.GroupUser>> GetAll();

        /// <summary>
        /// Save change
        /// </summary>
        void Save();
    }
}
