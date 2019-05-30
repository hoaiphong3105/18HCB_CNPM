using System.Collections.Generic;

namespace RestaurantMng.Service.User.Models.Request
{
    public class OrderReq
    {
        public List<OrderReq_Item> orders { get; set; }
        public string tableId { get; set; }
    }
    public class OrderReq_Item
    {
        public int orderItemId { get; set; }
        public int menuId { get; set; }
        public string menuName { get; set; }
        public int quantity { get; set; }
        public string note { get; set; }
    }
}
