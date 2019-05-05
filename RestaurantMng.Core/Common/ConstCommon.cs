using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMng.Core.Common
{
    public class ConstCommon
    {
        public const string USER_SESSION = "USER_SESSION";
    }

    /// <summary>
    /// Phân quyền trong hệ thống
    /// </summary>
    public class SystemRole
    {
        public const string Admin = "Admin";

        public const string Quanly = "Quan ly";

        public const string Daubep = "Dau bep";

        public const string Thungan = "Thu ngan";

        public const string Phucvu = "Phuc vu";
    }
}
