using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NetCore.Common.Http;
using NetCore.Common.Services;

namespace NetCore.Common.Mvc
{
    public class ErrorController : WebController
    {
        public ErrorController(Config config) : base(config)
        {
        }

        public IActionResult Index()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionFeature == null || exceptionFeature.Error == null)
            {
                return NotFound();
            }

            var model = new ErrorViewModel
            {
                Title = "Oops! Something went wrong."
            };

            if (exceptionFeature.Error is HttpException httpEx)
            {
                model.Status = httpEx.Status;
                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    model.ErrorMessage = httpEx.Message;
                }
                else
                {
                    model.ErrorMessage = GetErrorMessage(model.Status);
                }
            }
            else
            {
                model.Status = 500;
                model.ErrorMessage = GetErrorMessage(model.Status);
            }

            Response.StatusCode = model.Status;
            return View(model);
        }

        public static string GetErrorMessage(int errorStatus)
        {
            switch (errorStatus)
            {
                case 400:
                    return "Bad request. Please verify your request and try again.";
                case 403:
                    return "You don't have permission to access the page. Please contact to system administrator.";
                case 404:
                    return "The page you have requested cannot be found.";
                case 500:
                    return "Internal server error. Please verify your request and try again.";
                default:
                    return "An error occurred while processing your request. Please verify your request and try again.";
            }
        }
    }
}
