using Microsoft.AspNetCore.Http;

namespace NetCore.Common.Http
{

    [NotLog]
    public class BadRequestException : HttpException
    {
        public BadRequestException() : base(StatusCodes.Status400BadRequest, string.Empty)
        {
        }

        public BadRequestException(string message) : base(StatusCodes.Status400BadRequest, message)
        {
        }
    }
}
