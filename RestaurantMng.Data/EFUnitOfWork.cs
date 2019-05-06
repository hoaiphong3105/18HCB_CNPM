using RestaurantMng.Data.Interfaces;

namespace RestaurantMng.Data
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public EFUnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
