using System;

namespace NetCore.Common.Http
{
    public class HttpException : Exception
    {
        public int Status { get; private set; }

        public HttpException(int status, string message) : base(message)
        {
            this.Status = status;
        }

        public HttpException(int status, string message, Exception ex) : base(message, ex)
        {
            this.Status = status;
        }
    }
}
