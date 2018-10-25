using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace NetCore.Common.Utils
{
    public class ModelUtils
    {
        public static void SetProperties(object to, object from, Func<string, bool> forProperties)
        {
            AssertUtils.AssertNotNull(to);
            AssertUtils.AssertNotNull(from);
            AssertUtils.AssertNotNull(forProperties);

            PropertyInfo[] fromProperties = from.GetType().GetProperties();
            foreach (PropertyInfo fromProp in fromProperties)
            {
                if (!forProperties(fromProp.Name))
                {
                    continue;
                }
                AssertUtils.AssertTrue(fromProp.CanRead);

                PropertyInfo toProp = to.GetType().GetProperty(fromProp.Name);
                AssertUtils.AssertNotNull(toProp);
                AssertUtils.AssertTrue(toProp.CanWrite);

                toProp.SetValue(to, fromProp.GetValue(from));
            }
        }

        public static void SetProperties(object to, object from, params string[] forProperties)
        {
            AssertUtils.AssertHasElements(forProperties);

            SetProperties(to, from, prop => {

                return forProperties.Any(p => prop.Equals(p));
            });
        }
    }
}
