﻿using RestaurantMng.Core.Common;
using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.Service.User.Models.Dtos;
using RestaurantMng.WebApplication.ViewModels;
using System.Configuration;
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
            // test
            model = new UserViewModel();
            model.FullName = "Triệu Mẫn2";
            model.UserName = "thungan1";
            model.GroupID = 4;
            // end test

            var user = new Data.Models.User();
            user.FullName = user.FullName;
            user.UserName = user.UserName;
            user.Address = user.Address;
            user.DateOfBirth = user.DateOfBirth;
            user.Phone = user.Phone;
            user.GroupID = user.GroupID;
            user.Password = Encryption.HashMD5(ConfigurationManager.AppSettings["DefaultPassword"], user.UserName);
            user.Status = true;
            var result = _iUserService.CreateUser(user);
            return View();
        }
    }
}