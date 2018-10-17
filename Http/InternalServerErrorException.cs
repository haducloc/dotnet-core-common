using Microsoft.AspNetCore.Http;
using System;

namespace NetCore.Common.Http
{
    public class InternalServerErrorException : HttpException
    {
        public InternalServerErrorException() : base(StatusCodes.Status500InternalServerError, string.Empty)
        {
        }

        public InternalServerErrorException(string message) : base(StatusCodes.Status500InternalServerError, message)
        {
        }
    }
}
