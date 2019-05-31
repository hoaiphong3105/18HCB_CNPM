using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.WebApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Controllers
{
    public class RestaurantStatusController : Controller
    {
        private readonly ITableService _iTableService;
        private readonly IOrderService _iOrderService;

        public RestaurantStatusController(IOrderService iOrderService, ITableService iTableService)
        {
            _iOrderService = iOrderService;
            _iTableService = iTableService;
        }
        // GET: RestaurantStatus
        public ActionResult Index()
        {
            ViewBag.WaitingTable = _iOrderService.GetAll().Data
                .Where(x => x.Status == 1 && x.PaymentStatus == 1).Count();
            var tableList = _iTableService.GetAllTable().Data.Where(x => x.Status == 0).ToList();
            var tableInfoList = new List<TableInfoVM>();
            if(tableList != null && tableList.Count > 0)
            {
                foreach(var table in tableList)
                {
                    var orderItems = _iOrderService.GetOrderItems().Data
                        .Where(x => x.Order.TableId == table.TableId).ToList();
                    var orderInfoList = new List<OrderInfo>();
                    foreach(var orderItem in orderItems)
                    {
                        var menuItem = _iOrderService.GetAllMenu().Data
                            .Where(x => x.MenuItemId == orderItem.MeniItemId).FirstOrDefault();
                        var status = string.Empty;
                        if(orderItem.QuantityLate > 0)
                        {
                            status = "Bị trễ";
                        }
                        else if(orderItem.QuantityInProgress > 0)
                        {
                            status = "Đang chờ";
                        }
                        else
                        {
                            status = "Đã xong các món";
                        }
                        var orderInfo = new OrderInfo
                        {
                            Name = menuItem.Name,
                            Quantity = orderItem.Quantity,
                            Status = status,
                        };
                        orderInfoList.Add(orderInfo);
                    }
                    var tableInfo = new TableInfoVM
                    {
                        ID = table.TableId,
                        TableName = table.TableName,
                        OrderItems = orderInfoList,
                    };
                    tableInfoList.Add(tableInfo);
                }
            }
            return View(tableInfoList);
        }
    }
}