using System;

namespace NetCore.Common.Base
{
    public class InitializeException : Exception
    {
        public InitializeException()
        {
        }

        public InitializeException(Exception ex) : base(ex.Message, ex)
        {
        }
    }
}
