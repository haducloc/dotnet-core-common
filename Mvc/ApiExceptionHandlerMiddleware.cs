using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using NetCore.Common.AspNet;
using NetCore.Common.Http;
using NetCore.Common.Services;
using System;
using System.Threading.Tasks;

namespace NetCore.Common.Mvc
{
    public class ApiExceptionHandlerMiddleware
    {
        readonly RequestDelegate next;
        readonly Config config;
        readonly ILogger<ApiExceptionHandlerMiddleware> logger;

        public ApiExceptionHandlerMiddleware(RequestDelegate next, Config config, ILogger<ApiExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.config = config;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (Attribute.GetCustomAttribute(ex.GetType(), typeof(NotLog)) == null)
                {
                    this.logger.LogError(ex, ex.Message);
                }
                if (!context.Response.HasStarted)
                {
                    int status = StatusCodes.Status500InternalServerError;
                    if (ex as HttpException != null)
                    {
                        status = ((HttpException)ex).Status;
                    }

                    var model = new Result<string>
                    {
                        IsError = true,
                        Message = GetMessage(ex, status),
                        Exception = config.IsDebugEnvironment ? ex.StackTrace : ex.GetType().FullName
                    };
                    await AspNetUtils.WriteJsonAsync(context.Response, model, status);
                }
            }
        }

        protected string GetMessage(Exception exception, int status)
        {
            if (!string.IsNullOrEmpty(exception.Message))
            {
                return exception.Message;
            }
            switch (status)
            {
                case StatusCodes.Status400BadRequest: return "Bad request";
                case StatusCodes.Status401Unauthorized: return "Unauthenticated";
                case StatusCodes.Status403Forbidden: return "Unauthorized";
                case StatusCodes.Status404NotFound: return "Not found";
                case StatusCodes.Status500InternalServerError: return "Internal server error";
                case StatusCodes.Status503ServiceUnavailable: return "Service unavailable";
                default:
                    return "An unexpected error has occurred, please verify your request and try again.";
            }
        }
    }
}