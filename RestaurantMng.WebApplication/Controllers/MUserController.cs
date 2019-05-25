using RestaurantMng.Data.Models;
using RestaurantMng.Service.User.Interfaces;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iUserService"></param>
        public MUserController(IUserService iUserService)
        {
            _iUserService = iUserService;
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
    }
}