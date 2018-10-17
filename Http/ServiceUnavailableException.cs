using Microsoft.AspNetCore.Http;
using System;

namespace NetCore.Common.Http
{

    [NotLog]
    public class ServiceUnavailableException : HttpException
    {
        public ServiceUnavailableException() : base(StatusCodes.Status503ServiceUnavailable, string.Empty)
        {
        }

        public ServiceUnavailableException(string message) : base(StatusCodes.Status503ServiceUnavailable, message)
        {
        }
    }
}
