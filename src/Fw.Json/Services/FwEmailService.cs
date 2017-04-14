using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Fw.Json.SqlServer;

namespace Fw.Json.Services
{
    public static class FwEmailService
    {
        //---------------------------------------------------------------------------------------------
        public static string SendEmail(FwSqlConnection conn, string from, string to, string cc, string subject, string body, bool isBodyHtml, List<Attachment> attachments)
        {
            SmtpClient smtpClient;
            MailMessage message;
            string status;
            FwSqlData.GetEmailReportControlResponse emailreportcontrol;
            
            try {
                status = string.Empty;
                emailreportcontrol = FwSqlData.GetEmailReportControl(conn);
                message   = new MailMessage(from, to, subject, body);
                if (!string.IsNullOrWhiteSpace(cc))
                {
                    message.CC.Add(cc);
                }
                if (attachments != null)
                {
                    foreach (Attachment attachment in attachments)
                    {
                        message.Attachments.Add(attachment);
                    }
                }
                message.IsBodyHtml = isBodyHtml;
                smtpClient = new SmtpClient(emailreportcontrol.host, emailreportcontrol.port);
                smtpClient.Credentials = new NetworkCredential(emailreportcontrol.accountname, emailreportcontrol.accountpassword, string.Empty);
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }

            return status;
        }
        //---------------------------------------------------------------------------------------------
        public static string SendEmail(FwSqlConnection conn, string from, string to, string cc, string subject, string body, bool isBodyHtml, string attachmentname, string attachmentpath)
        {
            Attachment attachment;
            List<Attachment> attachments;
            
            attachments = new List<Attachment>();
            attachment = new Attachment(attachmentpath);
            attachment.Name = attachmentname;
            attachments.Add(attachment);
            return SendEmail(conn, from, to, cc, subject, body, isBodyHtml, attachments);
        }
        //---------------------------------------------------------------------------------------------
    }
}
