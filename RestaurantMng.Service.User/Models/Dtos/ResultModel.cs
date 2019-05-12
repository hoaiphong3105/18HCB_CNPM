namespace RestaurantMng.Service.User.Models.Dtos
{
    public class ResultModel<T>
    {
        public ResultModel()
        {
            Code = 1;
            Message = "Thành công";
            Data = default(T);
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }

    public class NullModel
    {

    }
}
