namespace RestaurantMng.WebApplication.ViewModels
{
    public class OrderOrderItemVM
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }

        public string TableName { get; set; }

        public string UserName { get; set; }

        public int Status { get; set; }
    }
}
