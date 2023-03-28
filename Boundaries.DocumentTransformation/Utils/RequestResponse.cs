namespace Boundaries.DocumentTransformation.Utils
{
    public class RequestResponse<T>
    {
        public string Message { get; set; }
        public T Content { get; set; }
        public System.Net.HttpStatusCode StatusCode { get; set; }

        public static RequestResponse<T> BuildFailResponse(string message, System.Net.HttpStatusCode code)
        {
            return new RequestResponse<T>
            {
                Message = message,
                StatusCode = code,
                Content = default
            };
        }

        public static RequestResponse<T> BuildResponse(T content)
        {
            return new RequestResponse<T>
            {
                Message = "",
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = content
            };
        }
    }
}
