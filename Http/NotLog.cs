using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common.Http
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class NotLog : Attribute
    {
    }
}
