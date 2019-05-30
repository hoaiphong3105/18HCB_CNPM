using System.Collections.Generic;

namespace RestaurantMng.WebApplication.Models.ViewModels
{
    public class OrderDetailVM
    {
        public int orderItemId { get; set; }
        public int menuId { get; set; }
        public string menuName { get; set; }
        public int quantity { get; set; }
        public string note { get; set; }
    }

    public class OrderVM
    {
        public List<OrderDetailVM> orders { get; set; }
        public string tableId { get; set; }
    }
}