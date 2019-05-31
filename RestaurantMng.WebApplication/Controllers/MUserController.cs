using RestaurantMng.Core.Common;
using RestaurantMng.Data.Models;
using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.Service.User.Models.Dtos;
using RestaurantMng.WebApplication.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Controllers
{
    [Authorization(Role = SystemRole.Quanly)]
    [RoutePrefix("quan-ly")]
    public class MUserController : Controller
    {
        private readonly IUserService _iUserService;
        private readonly IGroupUserService _iGroupUserService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iUserService"></param>
        public MUserController(IUserService iUserService, IGroupUserService iGroupUserService)
        {
            _iUserService = iUserService;
            _iGroupUserService = iGroupUserService;
        }

        /// <summary>
        /// GET: User
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var usersResult = _iUserService.GetAllUser();
            if(usersResult.Code == 1)
            {
                var users = usersResult.Data.Where(x => x.Status == true).ToList();
                return View(users);
            }

            return View(new List<User>());
        }

        /// <summary>
        /// Partial View GroupUser
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PartialListGroupUser()
        {
            var result = _iGroupUserService.GetAll();
            if (result.Code == 1) 
            {
                var groupUsers = result.Data.Where(x => x.Status == true).ToList();
                return PartialView("PartialListGroupUser", groupUsers);
            }

            return PartialView("PartialListGroupUser", new List<GroupUser>());
        }

        /// <summary>
        /// Insert user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Insert(User user)
        {
            var result = new ResultModel<int>();

            if(user != null)
            {
                user.Status = true;
                user.Password = Encryption.HashMD5(user.Password.Trim(), user.UserName.Trim());
                result = _iUserService.CreateUser(user);
            }
            var data = new
            {
                User = new
                {
                    ID = user.ID,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    Phone = user.Phone,
                    Salary = user.Salary,
                },
                code = result.Code,
                message = result.Message,
            };
           return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetUser(int id)
        {
            var user = _iUserService.GetUser(id);
            var result = new
            {
                ID = user.ID,
                UserName = user.UserName,
                FullName = user.FullName,
                Password = user.Password,
                Address = user.Address,
                GroupID = user.GroupID,
                DateOfBirth = user.DateOfBirth,
                Phone = user.Phone,
                Salary = user.Salary,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(User user)
        {
            var result = new ResultModel<int>();
            var currUser = new User();
            if (user != null)
            {
                currUser = _iUserService.GetUser(user.ID);
                if (string.IsNullOrEmpty(user.Password))
                {
                    user.Password = currUser.Password;
                }
                currUser.UserName = user.UserName;
                currUser.FullName = user.FullName;
                currUser.Password = user.Password;
                currUser.Address = user.Address;
                currUser.GroupID = user.GroupID;
                currUser.DateOfBirth = user.DateOfBirth;
                currUser.Phone = user.Phone;
                currUser.Salary = user.Salary;

                result = _iUserService.UpdateUser(currUser);
            }

            var data = new
            {
                User = new
                {
                    ID = currUser.ID,
                    UserName = currUser.UserName,
                    FullName = currUser.FullName,
                    Address = currUser.Address,
                    DateOfBirth = currUser.DateOfBirth,
                    Phone = currUser.Phone,
                    Salary = currUser.Salary,
                },
                code = result.Code,
                message = result.Message,
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = _iUserService.RemoveUser(id);
            var data = new
            {
                code = result.Code,
                message = result.Message,
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}