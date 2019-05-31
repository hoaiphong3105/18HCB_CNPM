using RestaurantMng.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantMng.WebApplication.Models.ViewModels
{
    public class TableInfoVM
    {
        public int ID;
        public string TableName;
        public List<OrderInfo> OrderItems;
    }
}