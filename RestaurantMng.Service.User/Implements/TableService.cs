using RestaurantMng.Data.Interfaces;
using RestaurantMng.Service.User.Interfaces;
using RestaurantMng.Service.User.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantMng.Service.User.Implements
{
    public class TableService : ITableService
    {
        private IRepository<RestaurantMng.Data.Models.TableList, int> _tableRepository;
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// 
        /// </summary>
        public TableService(IRepository<RestaurantMng.Data.Models.TableList, int> tableRepository,
            IUnitOfWork unitOfWork)
        {
            _tableRepository = tableRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public ResultModel<int> CreateTable(string tableName)
        {
            var result = new ResultModel<int>();
            try
            {
                var checkTable = CheckTableExist(tableName);
                if (checkTable.Code != 1)
                {
                    result.Code = checkTable.Code;
                    result.Message = checkTable.Message;
                }
                else
                {
                    var table = new Data.Models.TableList()
                    {
                        TableName = tableName,
                        Status = 0,
                    };

                    _tableRepository.Add(table);
                    _unitOfWork.Commit();
                    result.Data = table.TableId;
                }
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Error: Add table!";
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public ResultModel<NullModel> DeleteTable(int id)
        {
            var result = new ResultModel<NullModel>();
            try
            {
                var tableEntity = _tableRepository.FindById(id);
                if (tableEntity == null)
                {
                    result.Code = -1;
                    result.Message = "Table not exist";
                }
                else
                {
                    tableEntity.Status = -1;
                    _tableRepository.Update(tableEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Error: remove table!";
            }

            return result;
        }


        public ResultModel<List<Data.Models.TableList>> GetAllTable()
        {
            var result = new ResultModel<List<Data.Models.TableList>>();
            try
            {
                var data = _tableRepository.FindAll(x => x.Status == 0).ToList();
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Thất bại";
            }

            return result;
        }


        public ResultModel<NullModel> UpdateTable(string tableName)
        {
            var result = new ResultModel<NullModel>();
            try
            {
                var tableEntity = _tableRepository.FindSingle(x => x.TableName.Equals(tableName));
                if (tableEntity == null)
                {
                    result.Code = -1;
                    result.Message = "Table not exist";
                }
                else
                {
                    tableEntity.TableName = tableName;
                    _tableRepository.Update(tableEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Error: update table!";
            }

            return result;
        }

        /// <summary>
        /// Save
        /// </summary>
        public void Save()
        {
            _unitOfWork.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public ResultModel<NullModel> CheckTableExist(string tableName)
        {
            var result = new ResultModel<NullModel>();

            try
            {
                bool isExist = _tableRepository.FindSingle(x => x.TableName.Equals(tableName)) != null;
                if (isExist)
                {
                    result.Code = -1;
                    result.Message = "Table was existed";
                }
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Table was existedi";
            }
            return result;
        }

        public ResultModel<Data.Models.TableList> GetTable(int id)
        {
            var result = new ResultModel<Data.Models.TableList>();

            try
            {
                var table = _tableRepository.FindById(id);

                if (table == null)
                {
                    result.Code = -1;
                    result.Message = "Bàn không tồn tại";
                }
                else
                {
                    result.Data = table;
                }
            }
            catch (Exception ex)
            {
                result.Code = -2;
                result.Message = "Table was existedi";
            }
            return result;
        }
    }
}
