using RestaurantMng.Core.Common;
using RestaurantMng.Service.User.Models.Dtos;
using System.Web;
using System.Web.Mvc;

namespace RestaurantMng.WebApplication.Authorization
{
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        public string Role { set; get; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var infoSession = (LoginDto)HttpContext.Current.Session[ConstCommon.USER_SESSION];
            if (infoSession == null)
            {
                return false;
            }
            return (infoSession.Role == SystemRole.Admin || infoSession.Role == this.Role) ? true : false;
        }
    }
}