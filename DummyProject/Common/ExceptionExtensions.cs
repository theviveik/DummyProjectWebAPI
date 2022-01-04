using BusinessAccessLayer;
using BusinessAccessLayer.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace DummyProject
{
    public static class ExceptionExtensions
    {
        public static void ErrorExceptionExtensions(this IApplicationBuilder app, ICustomLog logger)
        {
           app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error($"Something Went Wrong in the {contextFeature.Error}");
                        await context.Response.WriteAsync(new Error()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error. Please Try Again Later"
                        }.ToString());
                    }
                });
            });

        }
    }
}
