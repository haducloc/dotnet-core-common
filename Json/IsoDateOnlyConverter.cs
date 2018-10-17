using NetCore.Common.Utils;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common.Json
{
    public class IsoDateOnlyConverter : IsoDateTimeConverter
    {
        public IsoDateOnlyConverter()
        {
            DateTimeFormat = DateUtils.Iso8601Date;
        }
    }
}
