using RestaurantMng.Data.Interfaces;
using RestaurantMng.Data.Models;
using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.Service.User.Models.Dtos;
using RestaurantMng.Service.User.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMng.Service.User.Implements
{
    public class OrderService : IOrderService
    {
        private IRepository<RestaurantMng.Data.Models.OrderItem, int> _orderItemRepository;
        private IRepository<RestaurantMng.Data.Models.Order, int> _orderRepository;
        private IRepository<RestaurantMng.Data.Models.Menu, int> _menuRepository;
        private IUnitOfWork _unitOfWork;

        public OrderService(IRepository<RestaurantMng.Data.Models.OrderItem, int> orderItemRepository,
            IRepository<RestaurantMng.Data.Models.Order, int> orderRepository,
            IRepository<RestaurantMng.Data.Models.Menu, int> menuRepository,
            IUnitOfWork unitOfWork)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _menuRepository = menuRepository;
            _unitOfWork = unitOfWork;
        }

        public ResultModel<List<Menu>> GetAllMenu()
        {
            var result = new ResultModel<List<Menu>>();
            try
            {
                result.Data = _menuRepository.FindAll(x => x.Status == 0).ToList();
            }
            catch(Exception ex)
            {
                result.Code = -2;
                result.Message = "Thất bại";
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultModel<List<Data.Models.OrderItem>> GetDetailInfoTable(int id)
        {
            var result = new ResultModel<List<Data.Models.OrderItem>>();
            try
            {
                var order = _orderRepository.FindAll(x => x.TableId == id && x.PaymentStatus == 0).FirstOrDefault();

                // bàn trống or chưa thanh toán
                if (order == null)
                {
                    result.Data = new List<OrderItem>();
                    return result;
                }
                else
                {
                    result.Data = _orderItemRepository.FindAll(x => x.OrderId == order.OrderId).ToList();
                }
            }
            catch(Exception ex)
            {
                result.Code = -2;
                result.Message = "Thất bại";
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultModel<List<NullModel>> Order(OrderReq model)
        {
            var result = new ResultModel<List<NullModel>>();
            try
            {

            }
            catch(Exception ex)
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
