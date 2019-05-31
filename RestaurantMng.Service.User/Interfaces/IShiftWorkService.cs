using RestaurantMng.Service.User.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMng.Service.User.Interfaces
{
    public interface IShiftWorkService
    {
        ResultModel<List<Data.Models.ShiftWork>> GetAll();
        ResultModel<List<Data.Models.ShiftWorkDetail>> GetDetail();
        ResultModel<int> AddShiftWorkDetail(Data.Models.ShiftWorkDetail swDetail);
        ResultModel<int> DeleteDetail(Data.Models.ShiftWorkDetail swDetail);
    }
}
