﻿namespace RestaurantMng.WebApplication.ViewModels
{
    public class OrderItemDetailVM
    {
        public int Quantity { get; set; }
        public string Note { get; set; }

        public decimal Price { get; set; }
        public decimal Surcharge { get; set; }

        public string ItemName { get; set; }

        public int Status { get; set; }
    }
}
