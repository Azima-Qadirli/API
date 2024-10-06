using System.Net;

namespace API06.Service.Exceptions.BaseExceptionFolder;

public class BaseException: Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public BaseException(string msg, HttpStatusCode statusCode=HttpStatusCode.InternalServerError)
    {
        StatusCode=statusCode;
    }
}