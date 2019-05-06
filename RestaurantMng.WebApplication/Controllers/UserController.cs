using RestaurantMng.Core.Common;
using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.Service.User.Models.Dtos;
using RestaurantMng.Service.User.Models.ViewModels;
using RestaurantMng.WebApplication.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Controllers
{
    [RoutePrefix("nhan-vien")]
    public class UserController : Controller
    {
        private readonly IUserService _iUserService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iUserService"></param>
        public UserController(IUserService iUserService)
        {
            _iUserService = iUserService;
        }

        [Route("")]
        public ActionResult Index()
        {
             return View();
        }

        /// <summary>
        /// GET: Login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dang-nhap")]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// POST: Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            string passwordHash = Encryption.HashMD5(password, username);

            var result = _iUserService.CheckLogin(username, passwordHash);
            if (result.Code == 1)
            {
                Session.Add(ConstCommon.USER_SESSION, (LoginDto)result.Data);
                return View("Index");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <returns></returns>
        //[Authorization(Role = SystemRole.Quanly)]
        [Route("them-moi")]
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[Authorization(Role = SystemRole.Quanly)]
        [HttpPost]
        public ActionResult CreateUser(UserViewModel model)
        {
            var user = new UserViewModel();
            user.FullName = "Triệu Mẫn2";
            user.UserName = "thungan1";
            user.GroupID = 4;
            var result = _iUserService.CreateUser(model);
            return View();
        }
    }
}