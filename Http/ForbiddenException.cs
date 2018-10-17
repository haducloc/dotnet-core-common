using Microsoft.AspNetCore.Http;

namespace NetCore.Common.Http
{
    public class ForbiddenException : HttpException
    {
        public ForbiddenException() : base(StatusCodes.Status403Forbidden, string.Empty)
        {
        }

        public ForbiddenException(string message) : base(StatusCodes.Status403Forbidden, message)
        {
        }
    }
}
