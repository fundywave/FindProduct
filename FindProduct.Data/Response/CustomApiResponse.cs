using System.Net;

namespace FindProduct.API
{
    public class CustomApiResponse<T>
    {
        public T Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
