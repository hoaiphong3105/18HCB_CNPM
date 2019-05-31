using RestaurantMng.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantMng.WebApplication.Models.ViewModels
{
    public class ShiftWorkInfoVM
    {
        public int ID;
        public string Name;
        public int Start;
        public int End;
        public List<User> Employees;
    }
}