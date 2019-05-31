using RestaurantMng.Data.Interfaces;
using RestaurantMng.Data;
using RestaurantMng.Service.User.Implements;
using RestaurantMng.Service.User.Interfaces;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;
using RestaurantMng.Service.Implements;
using RestaurantMng.Service.Interfaces;

namespace RestaurantMng.WebApplication
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<AppDbContext,AppDbContext>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUnitOfWork, EFUnitOfWork>(new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(IRepository<,>),typeof(EFRepository<,>));
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ITableService, TableService>();
            container.RegisterType<ICashierService, CashierService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IGroupUserService, GroupUserService>();
            container.RegisterType<IShiftWorkService, ShiftWorkService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}