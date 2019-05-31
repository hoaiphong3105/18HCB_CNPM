using RestaurantMng.Service.User.Models.Dtos;
using RestaurantMng.Service.User.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMng.Service.User.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// get all
        /// </summary>
        /// <returns></returns>
        ResultModel<List<Data.Models.OrderItem>> GetDetailInfoTable(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultModel<List<Data.Models.Menu>> GetAllMenu();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultModel<List<NullModel>> Order(OrderReq model);

        /// <summary>
        /// Lấy danh sách món ăn đã order dùng cho đầu bếp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultModel<List<Data.Models.OrderItem>> GetOrderItems();

        /// <summary>
        /// cập nhật trạng thái
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultModel<List<NullModel>> UpdateStatus(int orderItemId,int status);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ResultModel<List<Data.Models.Order>> GetAll();
    }
}
