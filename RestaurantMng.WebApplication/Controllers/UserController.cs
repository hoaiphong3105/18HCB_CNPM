using RestaurantMng.Core.Common;
using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.WebApplication.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Controllers
{
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

        // GET: User
        public ActionResult Index()
        {
             return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            string passwordHash = Encryption.HashMD5(password, username);

            var infoLogin = _iUserService.CheckLogin(username, passwordHash);
            if (infoLogin.IsLoginSuccess)
            {
                Session.Add(ConstCommon.USER_SESSION, infoLogin);
                return View("Index");
            }
            else
            {
                return View();
            }
        }
    }
}