using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common.Mvc
{
    public class Result<T>
    {
        public bool IsError { get; set; }

        public string Message { get; set; }

        public string Code { get; set; }

        public string Exception { get; set; }

        public string Link { get; set; }

        public T Data { get; set; }
    }
}
