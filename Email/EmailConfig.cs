using Microsoft.Extensions.Configuration;
using NetCore.Common.Utils;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace NetCore.Common.Email
{
    public class EmailConfig
    {
        public SmtpConfig SmtpConfig { get; }

        public string FromEmail { get; }

        public string FromName { get; }

        public string NoReplyEmail { get; }

        public EmailConfig(IConfiguration configuration)
        {
            var emailConfig = AssertUtils.AssertNotNull(configuration.GetSection("EmailConfig"));
            SmtpConfig = new SmtpConfig(emailConfig);

            FromEmail = StringUtils.TrimToNull(emailConfig["FromEmail"]);
            FromName = StringUtils.TrimToNull(emailConfig["FromName"]);
            NoReplyEmail = StringUtils.TrimToNull(emailConfig["NoReplyEmail"]);

            Init(emailConfig);
        }

        protected virtual void Init(IConfigurationSection emailConfig)
        {
        }

        public SmtpClient CreateSmtpClient() => SmtpConfig.CreateSmtpClient();

        public MailMessage CreateNoReplyMessage()
        {
            var message = new MailMessage
            {
                From = new MailAddress(FromEmail, FromName),
                IsBodyHtml = true
            };
            message.ReplyToList.Add(new MailAddress(NoReplyEmail, FromName));
            return message;
        }
    }
}
