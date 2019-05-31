using RestaurantMng.Data.Interfaces;
using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.Service.User.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMng.Service.User.Implements
{
    public class ShiftWorkService: IShiftWorkService
    {
        private IRepository<RestaurantMng.Data.Models.ShiftWork, int> _shiftWorkRepository;
        private IRepository<RestaurantMng.Data.Models.ShiftWorkDetail, int> _shiftWorkDetailRepository;
        private IUnitOfWork _unitOfWork;

        public ShiftWorkService(IRepository<RestaurantMng.Data.Models.ShiftWork, int> shiftWorkRepository,
            IRepository<RestaurantMng.Data.Models.ShiftWorkDetail, int> shiftWorkDetailRepository,
            IUnitOfWork unitOfWork)
        {
            _shiftWorkRepository = shiftWorkRepository;
            _shiftWorkDetailRepository = shiftWorkDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public ResultModel<List<Data.Models.ShiftWork>> GetAll()
        {
            var result = new ResultModel<List<Data.Models.ShiftWork>>();
            try
            {
                var data = _shiftWorkRepository.FindAll().ToList();
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thất bại";
            }

            return result;
        }

        public ResultModel<List<Data.Models.ShiftWorkDetail>> GetDetail()
        {
            var result = new ResultModel<List<Data.Models.ShiftWorkDetail>>();
            try
            {
                var data = _shiftWorkDetailRepository.FindAll().ToList();
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thất bại";
            }

            return result;
        }

        public ResultModel<int> AddShiftWorkDetail(Data.Models.ShiftWorkDetail swDetail)
        {
            var result = new ResultModel<int>();
            try
            {
                var checkExist = _shiftWorkDetailRepository.FindAll()
                    .Any(x => x.EmployeeID == swDetail.EmployeeID 
                        && x.ShiftWorkID == swDetail.ShiftWorkID);
                if (checkExist)
                {
                    result.Code = -1;
                    result.Message = "Đã tồn tai nhan vien trong ca lam viec nay";
                }
                else
                {
                    _shiftWorkDetailRepository.Add(swDetail);
                    _unitOfWork.Commit();

                    result.Data = swDetail.Id;
                }
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thêm ca thất bại!";
            }
            return result;
        }

        public ResultModel<int> DeleteDetail(Data.Models.ShiftWorkDetail swDetail)
        {
            var result = new ResultModel<int>();
            try
            {
                _shiftWorkDetailRepository.Remove(swDetail);
                _unitOfWork.Commit();

                result.Data = swDetail.Id;
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Xoá ca thất bại!";
            }
            return result;
        }
    }
}
