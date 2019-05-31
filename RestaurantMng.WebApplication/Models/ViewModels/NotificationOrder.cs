namespace RestaurantMng.WebApplication.Models.ViewModels
{
    public class NotificationOrder
    {
        public int OrderItemId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string Note { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
    }
}