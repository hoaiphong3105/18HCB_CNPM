namespace RestaurantMng.WebApplication.ViewModels
{
    public class OrderOrderItemVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }

        public string TableName { get; set; }

        public int Status { get; set; }
    }
}
