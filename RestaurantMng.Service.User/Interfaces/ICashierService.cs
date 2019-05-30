using RestaurantMng.Service.User.Models.Dtos;
using System;
using System.Collections.Generic;

namespace RestaurantMng.Service.Interfaces
{
    public interface ICashierService
    {
        /// <summary>
        /// get all
        /// </summary>
        /// <returns></returns>
        object GetAllOrder();
        object GetDetailOrder(int id);
        bool ThanhToan(int id, out string tableName);
        object ThongKe(DateTime? fromDate, DateTime? toDate, int type);
        void Save();
    }
}