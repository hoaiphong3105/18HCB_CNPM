﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestaurantMng.Data.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RestaurantManagementEntities : DbContext
    {
        public RestaurantManagementEntities()
            : base("name=RestaurantManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<GroupUser> GroupUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<FoodResource> FoodResources { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuToday> MenuTodays { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<SpendingMoney> SpendingMoneys { get; set; }
        public virtual DbSet<TableList> TableLists { get; set; }
        public virtual DbSet<ShiftWork> ShiftWorks { get; set; }
        public virtual DbSet<ShiftWorkDetail> ShiftWorkDetails { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
