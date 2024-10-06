using System.Net;
using API06.Service.Exceptions.BaseExceptionFolder;

namespace API06.Service.Exceptions;

public class ItemNotFoundException:BaseException
{
    public ItemNotFoundException(string msg, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base(msg, statusCode)
    {
    }
}