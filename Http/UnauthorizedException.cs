using Microsoft.AspNetCore.Http;

namespace NetCore.Common.Http
{

    [NotLog]
    public class UnauthorizedException : HttpException
    {
        public UnauthorizedException() : base(StatusCodes.Status401Unauthorized, string.Empty)
        {
        }

        public UnauthorizedException(string message) : base(StatusCodes.Status401Unauthorized, message)
        {
        }
    }
}
