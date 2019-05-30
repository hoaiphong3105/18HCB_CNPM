using RestaurantMng.Data.Interfaces;
using RestaurantMng.Service.User.Interfaces;
using System;
using RestaurantMng.Service.User.Models.Dtos;
using System.Collections.Generic;
using System.Linq;
using RestaurantMng.Data.Models;
using RestaurantMng.Service.Interfaces;

namespace RestaurantMng.Service.Implements
{
    public class CashierService : ICashierService
    {
        private IRepository<RestaurantMng.Data.Models.Order, int> _cashierOrderRepository;
        private IRepository<RestaurantMng.Data.Models.OrderItem, int> _cashierOrderItemRepository;
        private IRepository<RestaurantMng.Data.Models.SpendingMoney, int> _spendingRepository;
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="unitOfWork"></param>
        public CashierService(IRepository<RestaurantMng.Data.Models.Order, int> cashierOrderRepository, IRepository<RestaurantMng.Data.Models.OrderItem, int> cashierOrderItemRepository, IRepository<RestaurantMng.Data.Models.SpendingMoney, int> spendingRepository,
            IUnitOfWork unitOfWork)
        {
            _cashierOrderRepository = cashierOrderRepository;
            _cashierOrderItemRepository = cashierOrderItemRepository;
            _spendingRepository = spendingRepository;
            _unitOfWork = unitOfWork;
        }

        public object GetAllOrder()
        {
            var result = new ResultModel<List<Data.Models.Order>>();
            object res = null;
            try
            {
                var dataOrder = _cashierOrderRepository.FindAll();
                var dataOrderItem = _cashierOrderItemRepository.FindAll();

                res =
                    (from Order in dataOrder
                     join OrderItem in dataOrderItem on Order.OrderId equals OrderItem.OrderId 
                     select new { OrderId = Order.OrderId, ItemId = OrderItem.OrderItemId, TableName = Order.TableList.TableName, Status = Order.Status }).Where(x => x.Status == 0);
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thất bại";
            }

            return res;
        }

        public object GetDetailOrder(int id)
        {
            var result = new ResultModel<List<Data.Models.Order>>();
            object res = null;
            try
            {
                var dataOrderItem = _cashierOrderItemRepository.FindAll();
                var dataOrder = _cashierOrderRepository.FindAll();
                res = dataOrder.Where(x => x.OrderId == id).Join(dataOrderItem,
                        order => order.OrderId,
                        orderItem => orderItem.OrderId,
                        (order, orderItem) => new { Surcharge = order.Surcharge, OrderId = order.OrderId, ItemName = orderItem.Menu.Name, Note = orderItem.Note, Quantity = orderItem.Quantity, Price = orderItem.Price, Status = orderItem.Status });
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thất bại";
            }

            return res;
        }

        public bool ThanhToan(int id, out string tableName)
        {
            tableName = null;
            var result = new ResultModel<List<Data.Models.Order>>();
            var res = false;
            try
            {
                var dataOrder = _cashierOrderRepository.FindById(id);
                dataOrder.Status = 0;
                dataOrder.TableList.Status = 0;
                tableName = dataOrder.TableList.TableName;
                foreach (var item in dataOrder.OrderItems)
                {
                    item.Status = 0;
                } 

                _cashierOrderRepository.Update(dataOrder);
                res = true;
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thất bại";
            }
            Save();
            return res;
        }

        public object ThongKe(DateTime? fromDate, DateTime? toDate, int type)
        {
            var result = new ResultModel<List<Data.Models.Order>>();
            object res = null;
            try
            {
                var spend = _spendingRepository.FindAll();

                if (fromDate != null)
                    spend = spend.Where(x => x.Date >= fromDate);
                if (toDate != null)
                    spend = spend.Where(x => x.Date <= toDate);

                

                if (type != 1)
                {
                    var typeName = string.Empty;
                    switch (type)
                    {
                        case 2:
                            typeName = "ChiTienLuong";
                            break;
                        case 3:
                            typeName = "ChiTienThucPham";
                            break;
                        case 4:
                            typeName = "Thu";
                            break;
                        default:
                            break;
                    }
                    spend = spend.Where(x => x.Type == typeName);
                }
                res = spend;
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thất bại";
            }
            return res;
        }

        /// <summary>
        /// Xóa user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public ResultModel<NullModel> RemoveUser(int id)
        //{
        //    var result = new ResultModel<NullModel>();
        //    try
        //    {
        //        var cashierEntity = _cashierRepository.FindById(id);
        //        if (cashierEntity == null)
        //        {
        //            result.Code = -1;
        //            result.Message = "Nhân viên không tồn tại";
        //        }
        //        else
        //        {
        //            cashierEntity.Status = false;
        //            _cashierRepository.Update(cashierEntity);
        //            _unitOfWork.Commit();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Code = -2;
        //        result.Message = "Xóa nhân viên thất bại!";
        //    }

        //    return result;
        //}

        /// <summary>
        /// Save
        /// </summary>
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
