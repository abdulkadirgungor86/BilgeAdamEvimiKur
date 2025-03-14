using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.COMMON.Tools.Services
{
    public static class MailService
    {
        public async static Task SendAsync(string receiver, string subject = "BilgeAdam kursu email testi", string body = "BilgeAdam kursu test mesajıdır." )
        {
            List<string> keys = new List<string> { "senderEmail", "senderEmailPassword", "senderEmailDeliveryMethod", "senderEmailHost", "senderEmailPort", "senderEmailEnableSsl", "senderEmailUseDefaultCredentials" };
            Dictionary<string, string> configSettings = await JsonService.ReadFromFileAsync(keys);

            string sender = configSettings["senderEmail"];
            string password = configSettings["senderEmailPassword"];
            Enum.TryParse(configSettings["senderEmailDeliveryMethod"],false, out SmtpDeliveryMethod deliveryMethod);

            MailAddress senderEmail = new(sender);
            MailAddress receiverEmail = new(receiver);

            SmtpClient smtp = new()
            {

                Host = configSettings["senderEmailHost"], 
                Port = Convert.ToInt32(configSettings["senderEmailPort"]),
                EnableSsl = Convert.ToBoolean(configSettings["senderEmailEnableSsl"]),
                DeliveryMethod = deliveryMethod,
                UseDefaultCredentials = Convert.ToBoolean(configSettings["senderEmailUseDefaultCredentials"]),
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };

            using (MailMessage message = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
