using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NetCore.Common.Http;
using System.Threading.Tasks;

namespace NetCore.Common.AspNet
{
    public class ErrorStatusToExceptionMiddleware
    {
        readonly RequestDelegate next;

        public ErrorStatusToExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var statusCodeFeature = new StatusCodePagesFeature();
            context.Features.Set<IStatusCodePagesFeature>(statusCodeFeature);

            await next(context);

            if (!statusCodeFeature.Enabled)
            {
                return;
            }
            if (context.Response.HasStarted)
            {
                return;
            }

            // 4XX, 5XX
            int status = context.Response.StatusCode;
            if (status < 400 || status >= 600)
            {
                return;
            }
            if (status == StatusCodes.Status400BadRequest)
            {
                throw new BadRequestException();
            }
            if (status == StatusCodes.Status401Unauthorized)
            {
                throw new UnauthorizedException();
            }
            if (status == StatusCodes.Status403Forbidden)
            {
                throw new ForbiddenException();
            }
            if (status == StatusCodes.Status404NotFound)
            {
                throw new NotFoundException();
            }
            if (status == StatusCodes.Status500InternalServerError)
            {
                throw new InternalServerErrorException();
            }
            if (status == StatusCodes.Status503ServiceUnavailable)
            {
                throw new ServiceUnavailableException();
            }
            throw new HttpException(status, string.Empty);
        }
    }
}
