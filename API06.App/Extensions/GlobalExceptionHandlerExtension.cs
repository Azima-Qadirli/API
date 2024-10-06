using System.Net;
using API06.Service.Exceptions.BaseExceptionFolder;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace API06.App.Extensions;

public static class GlobalExceptionHandlerExtension
{
    public static void ConfigureException(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
               var statusCode = (int)HttpStatusCode.InternalServerError;
               var message  = "Internal Server Error";
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    if (contextFeature.Error is BaseException ex)
                    {
                        message = ex.Message;
                        statusCode = (int)ex.StatusCode;
                    }
                    context.Response.StatusCode = statusCode;
                    var result = JsonConvert.SerializeObject(new{statusCode = statusCode, message = message});
                    await context.Response.WriteAsync(result);
                }
            });
        });
        
          
    }
}