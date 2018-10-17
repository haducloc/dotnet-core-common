using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NetCore.Common.Services
{
    public class ViewRenderService
    {
        // https://github.com/aspnet/Entropy/blob/dev/samples/Mvc.RenderViewToString/RazorViewToStringRenderer.cs

        public IRazorViewEngine ViewEngine { get; }

        public IHttpContextAccessor HttpContextAccessor { get; }

        public ViewRenderService(IRazorViewEngine viewEngine, IHttpContextAccessor httpContextAccessor)
        {
            this.ViewEngine = viewEngine;
            this.HttpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Render<TModel>(string viewPath, TModel model, Action<dynamic> viewBagConsumer = null)
        {
            var viewEngineResult = ViewEngine.GetView(null, viewPath, false);
            if (!viewEngineResult.Success)
            {
                throw new InvalidOperationException($"Couldn't find view {viewPath}");
            }
            var view = viewEngineResult.View;

            using (var output = new StringWriter())
            {
                var httpContext = new DefaultHttpContext
                {
                    RequestServices = HttpContextAccessor.HttpContext.RequestServices,
                    User = HttpContextAccessor.HttpContext.User
                };

                var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    new ViewDataDictionary<TModel>(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary())
                    {
                        Model = model
                    },
                    new TempDataDictionary(
                       httpContext,
                       (ITempDataProvider)httpContext.RequestServices.GetService(typeof(ITempDataProvider))),
                    output,
                    new HtmlHelperOptions());

                if (viewBagConsumer != null) viewBagConsumer(viewContext.ViewBag);

                await view.RenderAsync(viewContext);
                return output.ToString().Trim();
            }
        }
    }
}
