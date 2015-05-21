using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ElectronicStore.Common
{
    class EmailHelper
    {
        public static void SendEmail(List<string> toList, string subject, string template, List<string> parameters, List<string> ccList)
        {
            if (toList.Count > 0)
            {
                string smtpServer = Utilities.GetSetting("mail-host", string.Empty);
                int smtpPort = int.Parse(Utilities.GetSetting("mail-port", string.Empty));
                string smtpUsername = Utilities.GetSetting("mail-user", string.Empty);
                string smtpPassword = Utilities.GetSetting("mail-password", string.Empty);
                string smtpSenderAddress = Utilities.GetSetting("email-from", string.Empty);
                int i = 1;
                foreach (var item in parameters)
                {
                    string source = "|_|" + i + "|_|";
                    string targer = item;
                    template = template.Replace(source, targer);
                    i++;
                }
                string toAddress = toList[0];
                var message = new MailMessage(smtpSenderAddress, toAddress, subject, template);
                foreach (var item in ccList)
                {
                    message.CC.Add(item);
                }
                foreach (var item in toList)
                {
                    if (item != toAddress)
                    {
                        message.To.Add(item);
                    }
                }
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SendMail(smtpServer, smtpPort, smtpUsername, smtpPassword, message);
            }
        }

        public static void SendMail(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, MailMessage message)
        {
            var mailClient = new SmtpClient(
                smtpServer, smtpPort)
            {
                Credentials =
                    new NetworkCredential(
                        smtpUsername,
                        smtpPassword),
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            try
            {
                mailClient.Send(message);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
