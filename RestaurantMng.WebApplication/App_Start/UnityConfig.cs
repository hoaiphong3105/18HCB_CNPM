using RestaurantMng.Core.Interfaces;
using RestaurantMng.Data;
using RestaurantMng.Service.User.Implements;
using RestaurantMng.Service.User.Interfaces;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace RestaurantMng.WebApplication
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, EFUnitOfWork>();
            container.RegisterType(typeof(IRepository<,>),typeof(EFRepository<,>));
            container.RegisterType<IUserService, UserService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}