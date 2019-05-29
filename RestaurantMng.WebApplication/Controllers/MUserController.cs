using RestaurantMng.Data.Models;
using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.Service.User.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Controllers
{
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
        public int Insert(User user)
        {
            var result = new ResultModel<int>();

            if(user != null)
            {
                user.Status = true;
                result = _iUserService.CreateUser(user);
            }

            return result.Code;
        }
    }
}