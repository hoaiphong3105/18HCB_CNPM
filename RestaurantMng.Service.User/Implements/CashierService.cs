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
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="unitOfWork"></param>
        public CashierService(IRepository<RestaurantMng.Data.Models.Order, int> cashierOrderRepository, IRepository<RestaurantMng.Data.Models.OrderItem, int> cashierOrderItemRepository,
            IUnitOfWork unitOfWork)
        {
            _cashierOrderRepository = cashierOrderRepository;
            _cashierOrderItemRepository = cashierOrderItemRepository;
            _unitOfWork = unitOfWork;
        }

        ///// <summary>
        ///// Thay đổi mật khẩu
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //public ResultModel<NullModel> ChangePassword(int id, string password)
        //{
        //    var result = new ResultModel<NullModel>();
        //    try
        //    {
        //        var cashierEntity = _cashierOrderRepository.FindById(id);
        //        if (cashierEntity == null)
        //        {
        //            result.Code = -1;
        //            result.Message = "Nhân viên không tồn tại!";
        //        }
        //        else
        //        {
        //            cashierEntity.Password = password;
        //            _cashierRepository.Update(cashierEntity);
        //            _unitOfWork.Commit();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Code = -2;
        //        result.Message = "Thay đổi mật khẩu thất bại";
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// Login
        ///// </summary>
        ///// <param name="username"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //public ResultModel<LoginDto> CheckLogin(string username, string password)
        //{
        //    var result = new ResultModel<LoginDto>();
        //    try
        //    {
        //        var data = _cashierRepository.FindSingle(x => x.UserName.Equals(username)
        //                    && x.Password.Equals(password) && x.Status == true);
        //        if (data != null)
        //        {
        //            var infoLogin = new LoginDto();
        //            infoLogin.UserID = data.ID;
        //            infoLogin.Role = data.GroupUser.GroupName;
        //            infoLogin.UserName = data.UserName;

        //            result.Data = infoLogin;
        //        }
        //        else
        //        {
        //            result.Code = -1;
        //            result.Message = "Đăng nhập thất bại";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Code = -2;
        //        result.Message = "Đăng nhập thất bại";
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// Kiểm tra account đã tồn tại
        ///// Dùng khi tạo mới nhân viên
        ///// </summary>
        ///// <param name="username"></param>
        ///// <returns></returns>
        //public ResultModel<NullModel> CheckUserExist(string username)
        //{
        //    var result = new ResultModel<NullModel>();

        //    try
        //    {
        //        bool isExist = _cashierRepository.FindSingle(x => x.UserName.Equals(username)) != null;
        //        if (isExist)
        //        {
        //            result.Code = -1;
        //            result.Message = "Tài khoản đã tồn tại";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Code = -2;
        //        result.Message = "Tài khoản đã tồn tại";
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// Thêm mới nhân viên 
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public ResultModel<int> CreateUser(Data.Models.User user)
        //{
        //    var result = new ResultModel<int>();
        //    try
        //    {
        //        var checkUser = CheckUserExist(user.UserName);
        //        if (checkUser.Code == 1)
        //        {
        //            result.Code = checkUser.Code;
        //            result.Message = checkUser.Message;
        //        }
        //        else
        //        {
        //            _cashierRepository.Add(user);
        //            _unitOfWork.Commit();

        //            result.Data = user.ID;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Code = -2;
        //        result.Message = "Thêm nhân viên thất bại!";
        //    }
        //    return result;
        //}

        public object GetAllOrder()
        {
            var result = new ResultModel<List<Data.Models.Order>>();
            var res = new object();
            try
            {
                var dataOrder = _cashierOrderRepository.FindAll();
                var dataOrderItem = _cashierOrderItemRepository.FindAll();

                res =
                    from Order in dataOrder
                     join OrderItem in dataOrderItem on Order.OrderId equals OrderItem.OrderId
                     select new { OrderId = Order.OrderId, ItemId = OrderItem.OrderItemId, TableName = Order.TableList.TableName, Status = Order.Status };
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
            var res = new object();
            try
            {
                var dataOrderItem = _cashierOrderItemRepository.FindAll();
                var dataOrder = _cashierOrderRepository.FindAll();
                res = dataOrder.Where(x => x.OrderId == id).Join(dataOrderItem,
                        order => order.OrderId,
                        orderItem => orderItem.OrderId,
                        (order, orderItem) => new { OrderId = order.OrderId, ItemName = orderItem.Menu.Name, Note = orderItem.Note, TotalPrice = order.TotalPrice, Price = orderItem.Price, Status = orderItem.Status });
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thất bại";
            }

            return res;
        }

        public bool ThanhToan(int id)
        {
            var result = new ResultModel<List<Data.Models.Order>>();
            var res = false;
            try
            {
                var dataOrder = _cashierOrderRepository.FindById(id);
                dataOrder.Status = 0;
                dataOrder.TableList.Status = 0;
                _cashierOrderRepository.Update(dataOrder);

            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thất bại";
            }
            Save();
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
