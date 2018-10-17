using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common.Base
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(Exception ex) : base(ex.Message, ex)
        {
        }
    }
}
