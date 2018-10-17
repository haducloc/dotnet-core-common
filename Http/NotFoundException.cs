using Microsoft.AspNetCore.Http;

namespace NetCore.Common.Http
{

    [NotLog]
    public class NotFoundException : HttpException
    {
        public NotFoundException() : base(StatusCodes.Status404NotFound, string.Empty)
        {
        }

        public NotFoundException(string message) : base(StatusCodes.Status404NotFound, message)
        {
        }
    }
}
