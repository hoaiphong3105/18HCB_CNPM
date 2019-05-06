namespace RestaurantMng.Service.User.Models.Dtos
{
    public class ResultModel
    {
        public ResultModel()
        {
            Code = 1;
            Message = "Thành công";
            Data = null;
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
