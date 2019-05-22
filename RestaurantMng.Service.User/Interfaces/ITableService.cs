using RestaurantMng.Service.User.Models.Dtos;
using RestaurantMng.Service.User.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMng.Service.User.Interfaces
{
    public interface ITableService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        ResultModel<int> CreateTable(string tableName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        ResultModel<NullModel> UpdateTable(string tableName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        ResultModel<NullModel> DeleteTable(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ResultModel<List<TableVM>> GetAllTable();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        ResultModel<NullModel> CheckTableExist(string tableName);
    }
}
