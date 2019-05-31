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
        /// <returns></returns>
        public ResultModel<List<Data.Models.OrderItem>> GetOrderItems()
        {
            var result = new ResultModel<List<Data.Models.OrderItem>>();
            try
            {
                result.Data = _orderItemRepository.FindAll(x => x.Status == 1 || x.Status == 3).ToList();
            }
            catch (Exception ex)
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
        public ResultModel<List<Data.Models.OrderItem>> Order(OrderReq model)
        {
            var result = new ResultModel<List<Data.Models.OrderItem>>();
            try
            {
                var orderExist = _orderRepository.FindAll(x => x.TableId == model.tableId && x.Status == 1).FirstOrDefault();
                // nếu tồn tại => cập nhật
                if (orderExist != null)
                {
                    var orderItems = new List<Data.Models.OrderItem>();

                    // item thêm mới
                    var newItems = model.orders.Where(x => x.orderItemId == 0);
                    foreach (var item in newItems)
                    {
                        var orderItem = new Data.Models.OrderItem();
                        orderItem.MeniItemId = item.menuId;
                        orderItem.Note = item.note;
                        orderItem.Price = _menuRepository.FindById(item.menuId).Price;
                        orderItem.Quantity = item.quantity;
                        orderItem.Status = 1;
                        orderItems.Add(orderItem);
                    }
                    // item update
                    var itemUpdates = model.orders.Where(x => x.orderItemId != 0);
                    foreach (var item in itemUpdates)
                    {
                        var orderItem = _orderItemRepository.FindById(item.orderItemId);
                        orderItem.MeniItemId = item.menuId;
                        orderItem.Note = item.note;
                        orderItem.Price = _menuRepository.FindById(item.menuId).Price;
                        orderItem.Quantity = item.quantity;
                        orderItems.Add(orderItem);
                    }
                    orderExist.OrderItems = orderItems;

                    _orderRepository.Update(orderExist);
                    _unitOfWork.Commit();

                    // lấy ra dánh sách thêm mới
                    result.Data = orderExist.OrderItems.Where(x => !itemUpdates.Any(y => y.orderItemId == x.OrderItemId)).ToList();
                }
                else // thêm mới
                {
                    var order = new Data.Models.Order();
                    order.Status = 1;
                    order.OrderTime = DateTime.Now;
                    order.ServantId = model.servantId;
                    order.TableId = model.tableId;

                    var orderItems = new List<Data.Models.OrderItem>();
                    foreach (var item in model.orders)
                    {
                        if (item.quantity > 0)
                        {
                            var orderItem = new Data.Models.OrderItem();
                            orderItem.MeniItemId = item.menuId;
                            orderItem.Note = item.note;
                            orderItem.Price = _menuRepository.FindById(item.menuId).Price;
                            orderItem.Quantity = item.quantity;
                            orderItem.Status = 1;
                            orderItem.QuantityCompleted = 0;
                            orderItem.QuantityInProgress = 0;
                            orderItem.QuantityLate = 0;
                            orderItems.Add(orderItem);
                        }
                    }
                    order.OrderItems = orderItems;

                    if (orderItems.Count > 0)
                    {
                        _orderRepository.Add(order);
                        _unitOfWork.Commit();
                        result.Data = order.OrderItems.ToList();
                    }
                }
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

        /// <summary>
        /// Cập nhật trạng thái
        /// </summary>
        /// <returns></returns>
        public ResultModel<List<string>> UpdateStatus(int orderItemId, int status)
        {
            var result = new ResultModel<List<string>>();
            try
            {
                var orderItem = _orderItemRepository.FindById(orderItemId);
                orderItem.Status = status;

                _orderItemRepository.Update(orderItem);
                _unitOfWork.Commit();
                string menuName = _menuRepository.FindById(orderItem.MeniItemId).Name;
                string tableName = _orderRepository.FindById(orderItem.OrderId).TableList.TableName;
                string strStatus = status == 1 ? "đang xử lý" : status == 2 ? "hoàn thành" : "trễ";
                result.Data = new List<string>() { ($"{tableName}: {menuName} {strStatus}") };
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
        /// <returns></returns>
        public ResultModel<List<Data.Models.Order>> GetAll()
        {
            var result = new ResultModel<List<Data.Models.Order>>();
            try
            {
                var data = _orderRepository.FindAll()
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
    }
}
