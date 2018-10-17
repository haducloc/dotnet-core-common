using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common.Crypto
{
    public class VerifyCodeUtils
    {
        public static string GenerateVerifyCode(int len)
        {
            Random rd = new Random();
            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                sb.Append(rd.Next(0, 10));
            }
            return sb.ToString();
        }
    }
}
