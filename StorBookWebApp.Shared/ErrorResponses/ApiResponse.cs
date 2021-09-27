using Newtonsoft.Json;

namespace StorBookWebApp.Shared.ErrorResponses
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public int StatusCode {  get; set; }
        public static ApiResponse<T> Fail(string errorMessage,  int statusCode = default)
        {
            return new ApiResponse<T> { Succeeded = false, Message = errorMessage, StatusCode =  statusCode};
        }
        public static ApiResponse<T> Success(T data, int statusCode = 0)
        {
            return new ApiResponse<T> { Succeeded = true, Data = data, StatusCode = statusCode };
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
