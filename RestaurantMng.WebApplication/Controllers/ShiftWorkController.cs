using RestaurantMng.Data.Models;
using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.Service.User.Models.Dtos;
using RestaurantMng.WebApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Controllers
{
    public class ShiftWorkController : Controller
    {
        private readonly IShiftWorkService _iShiftWorkService;
        private readonly IUserService _iUserService;
        public ShiftWorkController(IShiftWorkService iShiftWorkService, IUserService iUserService)
        {
            _iShiftWorkService = iShiftWorkService;
            _iUserService = iUserService;
        }
        // GET: ShiftWork
        public ActionResult Index()
        {
            var shiftWorks = _iShiftWorkService.GetAll().Data;
            var shiftWorkDetails = _iShiftWorkService.GetDetail().Data;
            var listShiftWork = new List<ShiftWorkInfoVM>();
            foreach(var shiftWork in shiftWorks)
            {
                var shiftWorkDetail = shiftWorkDetails.Where(x => x.ShiftWorkID == shiftWork.Id);
                var shiftWorkInfo = new ShiftWorkInfoVM
                {
                    ID = shiftWork.Id,
                    Name = shiftWork.Name,
                    Start = (int)shiftWork.Start,
                    End = (int)shiftWork.End,
                };
                shiftWorkInfo.Employees = new List<Data.Models.User>();
                foreach (var swd in shiftWorkDetail)
                {
                    var employee = _iUserService.GetUser((int)swd.EmployeeID);
                    shiftWorkInfo.Employees.Add(employee);
                }
                listShiftWork.Add(shiftWorkInfo);
            }
            return View(listShiftWork);
        }

        public PartialViewResult PartialListUser()
        {
            var result = _iUserService.GetAllUser();
            if (result.Code == 1)
            {
                var users = result.Data.Where(x => x.Status == true).ToList();
                return PartialView("PartialListUser", users);
            }

            return PartialView("PartialListUser", new List<User>());
        }

        public PartialViewResult PartialListShiftWork()
        {
            var result = _iShiftWorkService.GetAll();
            if (result.Code == 1)
            {
                return PartialView("PartialListShiftWork", result.Data);
            }

            return PartialView("PartialListShiftWork", new List<ShiftWork>());
        }

        [HttpPost]
        public ActionResult Insert(int employeeID, int shiftWorkID)
        {
            var result = new ResultModel<int>();
            var swDetail = new ShiftWorkDetail
            {
                EmployeeID = employeeID,
                ShiftWorkID = shiftWorkID,
            };

            result = _iShiftWorkService.AddShiftWorkDetail(swDetail);
            var user = _iUserService.GetUser(employeeID);
            var data = new
            {
                User = new
                {
                    UserName = user.UserName,
                    FullName = user.FullName,
                },
                code = result.Code,
                message = result.Message,
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteDetail(int employeeID, int shiftWorkID)
        {
            var result = new ResultModel<int>();
            var swDetail = _iShiftWorkService.GetDetail().Data
                .Where(x => x.EmployeeID == employeeID && x.ShiftWorkID == shiftWorkID)
                .FirstOrDefault();

            result = _iShiftWorkService.DeleteDetail(swDetail);

            return Json(result.Code, JsonRequestBehavior.AllowGet);
        }
    }
}