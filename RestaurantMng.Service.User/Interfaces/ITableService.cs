using RestaurantMng.Service.User.Models.Dtos;
using System.Collections.Generic;

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
        ResultModel<List<Data.Models.TableList>> GetAllTable();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        ResultModel<NullModel> CheckTableExist(string tableName);
    }
}
