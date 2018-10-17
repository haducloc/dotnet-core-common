using Microsoft.Extensions.Configuration;
using NetCore.Common.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace NetCore.Common.Email
{
    public class SmtpConfig
    {
        public string Host { get; }

        public int Port { get; }

        public bool EnableSsl { get; }

        public string Login { get; }

        public string Password { get; }

        public SmtpConfig(IConfigurationSection emailConfig)
        {
            var config = AssertUtils.AssertNotNull(emailConfig.GetSection("SmtpConfig"));

            Host = AssertUtils.AssertNotNull(StringUtils.TrimToNull(config["SmtpHost"]));
            Port = Int32.Parse(AssertUtils.AssertNotNull(StringUtils.TrimToNull(config["SmtpPort"])));

            string enableSsl = StringUtils.TrimToNull(config["SmtpSsl"]);
            if (enableSsl != null)
            {
                EnableSsl = Boolean.Parse(enableSsl);
            }
            Login = AssertUtils.AssertNotNull(StringUtils.TrimToNull(config["SmtpLogin"]));
            Password = StringUtils.TrimToDefault(config["SmtpPassword"], string.Empty);
        }

        public SmtpClient CreateSmtpClient()
        {
            return new SmtpClient
            {
                Host = Host,
                Port = Port,
                EnableSsl = EnableSsl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential { UserName = Login, Password = Password },
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
        }
    }
}
