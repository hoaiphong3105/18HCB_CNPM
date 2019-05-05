using System.Data.Entity;

namespace RestaurantMng.Data
{
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbName"></param>
        public AppDbContext() : base("RestaurantManagementEntities")
        {
        }
    }
}
