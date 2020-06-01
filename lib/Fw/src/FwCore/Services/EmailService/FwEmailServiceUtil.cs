using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static FwStandard.Reporting.FwReport;

namespace FwCore.Services.EmailService
{
    public class FwEmailServiceUtil
    {
        public static async Task ProcessEmailAsync(FwApplicationConfig appConfig, Func<BeforeSendEmailRequest, Task<BeforeSendEmailResponse>> funcBeforeSendEmail)
        {
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                conn.LogSql = appConfig.HostedServices.LogSql("EmailService");

                // Get 'OPEN' emailtasks
                FwJsonDataTable dtOpenEmailtasks;
                using (FwSqlCommand qryOpenEmailTasks = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qryOpenEmailTasks.Add("select et.emailtaskid, et.createdate");
                    qryOpenEmailTasks.Add("from   emailtask et with(nolock) join appemail ae with(nolock) on(et.appemailid = ae.appemailid)");
                    qryOpenEmailTasks.Add("where status = 'OPEN'");
                    //qryOpenEmailTasks.Add("  and et.emailtaskid = isnull(@emailtaskid, et.emailtaskid)");
                    qryOpenEmailTasks.Add("  and ae.limitsendingemail <> 'T'");
                    qryOpenEmailTasks.Add("union");
                    qryOpenEmailTasks.Add("select et.emailtaskid, et.createdate");
                    qryOpenEmailTasks.Add("from   emailtask et with(nolock) join appemail ae with(nolock) on(et.appemailid = ae.appemailid)");
                    qryOpenEmailTasks.Add("where status = 'OPEN'");
                    //qryOpenEmailTasks.Add("  and et.emailtaskid = isnull(@emailtaskid, et.emailtaskid)");
                    qryOpenEmailTasks.Add("  and ae.limitsendingemail = 'T'");
                    qryOpenEmailTasks.Add("  and(dbo.cansendemail(ae.limitsunday,");
                    qryOpenEmailTasks.Add("                       ae.limitmonday,");
                    qryOpenEmailTasks.Add("                       ae.limittuesday,");
                    qryOpenEmailTasks.Add("                       ae.limitwednesday,");
                    qryOpenEmailTasks.Add("                       ae.limitthursday,");
                    qryOpenEmailTasks.Add("                       ae.limitfriday,");
                    qryOpenEmailTasks.Add("                       ae.limitsaturday,");
                    qryOpenEmailTasks.Add("                       ae.limitstarttime,");
                    qryOpenEmailTasks.Add("                       ae.limitendtime) = 'T')");
                    qryOpenEmailTasks.Add("order by createdate");
                    dtOpenEmailtasks = await qryOpenEmailTasks.QueryToFwJsonTableAsync();
                }

                // call prepareemail on each 'OPEN' emailtask
                for (int rowno = 0; rowno < dtOpenEmailtasks.Rows.Count; rowno++)
                {
                    try
                    {
                        string emailtaskid = dtOpenEmailtasks.GetValue(rowno, "emailtaskid").ToString().TrimEnd();
                        using (FwSqlCommand cmdPrepareEmail = new FwSqlCommand(conn, "prepareemail", appConfig.DatabaseSettings.QueryTimeout))
                        {
                            cmdPrepareEmail.AddParameter("@emailtaskid", emailtaskid);
                            await cmdPrepareEmail.ExecuteNonQueryAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message + ex.StackTrace);
                    }
                }

                // Get 'OUTBOX' emailtasks
                FwJsonDataTable dtOutboxEmailTasks;
                using (FwSqlCommand qryOutboxEmailTasks = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qryOutboxEmailTasks.Add("select et.emailtaskid, et.status, et.subject, et.emailtext, ae.category");
                    qryOutboxEmailTasks.Add("from emailtask et with (nolock) join appemail ae with(nolock) on(et.appemailid = ae.appemailid)");
                    //qryOutboxEmailTasks.Add("  and et.emailtaskid = isnull(@emailtaskid, et.emailtaskid)");
                    qryOutboxEmailTasks.Add("where status = 'OUTBOX'");
                    qryOutboxEmailTasks.Add("order by createdate");
                    dtOutboxEmailTasks = await qryOutboxEmailTasks.QueryToFwJsonTableAsync();
                }


                for (int rownoEmailTask = 0; rownoEmailTask < dtOutboxEmailTasks.Rows.Count; rownoEmailTask++)
                {
                    try
                    {
                        string emailtaskid = dtOutboxEmailTasks.GetValue(rownoEmailTask, "emailtaskid").ToString().TrimEnd();
                        string appemailCategory = dtOutboxEmailTasks.GetValue(rownoEmailTask, "category").ToString().TrimEnd();
                        string status = dtOutboxEmailTasks.GetValue(rownoEmailTask, "status").ToString().TrimEnd();
                        string emailSubject = dtOutboxEmailTasks.GetValue(rownoEmailTask, "subject").ToString().TrimEnd();
                        string emailBody = dtOutboxEmailTasks.GetValue(rownoEmailTask, "emailtext").ToString().TrimEnd();
                        await FwEmailServiceUtil.SendEmail(appConfig, conn, emailtaskid, appemailCategory, status, emailSubject, emailBody, funcBeforeSendEmail);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message + ex.StackTrace);
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task SendEmail(FwApplicationConfig appConfig, FwSqlConnection conn, string emailtaskid, string appemailCategory, string status, string emailSubject, string emailBody, Func<BeforeSendEmailRequest, Task<BeforeSendEmailResponse>> funcBeforeSendEmail)
        {
            try
            {
                string emailTo = string.Empty;
                string emailCc = string.Empty;
                bool letSendEmail = true;

                // get emailrecipients
                using (FwSqlCommand procGetEmailTo = new FwSqlCommand(conn, "dbo.getemailto", appConfig.DatabaseSettings.QueryTimeout))
                {
                    procGetEmailTo.AddParameter("@emailtaskid", emailtaskid);
                    procGetEmailTo.AddParameter("@emailtype", "EMAIL");
                    procGetEmailTo.AddParameter("@receipients", SqlDbType.VarChar, ParameterDirection.Output);
                    await procGetEmailTo.ExecuteNonQueryAsync();
                    emailTo = procGetEmailTo.GetParameter("@receipients").ToString().TrimEnd().Replace(";", ","); ;
                    if (string.IsNullOrEmpty(emailTo))
                    {
                        using (FwSqlCommand procLogEmailHistory = new FwSqlCommand(conn, "dbo.logemailhistory", appConfig.DatabaseSettings.QueryTimeout))
                        {
                            procGetEmailTo.AddParameter("@emailtaskid", emailtaskid);
                            procGetEmailTo.AddParameter("@message", "No email account to send to.");
                            await procGetEmailTo.ExecuteNonQueryAsync();
                        }
                    }
                }

                // get CC
                using (FwSqlCommand procGetEmailTo = new FwSqlCommand(conn, "dbo.getemailto", appConfig.DatabaseSettings.QueryTimeout))
                {
                    procGetEmailTo.AddParameter("@emailtaskid", emailtaskid);
                    procGetEmailTo.AddParameter("@emailtype", "CC");
                    procGetEmailTo.AddParameter("@receipients", SqlDbType.VarChar, ParameterDirection.Output);
                    await procGetEmailTo.ExecuteNonQueryAsync();
                    emailCc = procGetEmailTo.GetParameter("@receipients").ToString().TrimEnd().Replace(";", ","); ;
                }

                // log an error if there are no email recipients
                if (string.IsNullOrEmpty(emailTo))
                {
                    using (FwSqlCommand procLogEmailHistory = new FwSqlCommand(conn, "dbo.logemailhistory", appConfig.DatabaseSettings.QueryTimeout))
                    {
                        procLogEmailHistory.AddParameter("@emailtaskid", emailtaskid);
                        procLogEmailHistory.AddParameter("@message", "No email account to send to.");
                        await procLogEmailHistory.ExecuteNonQueryAsync();
                        letSendEmail = false;
                    }
                }

                // log an error if there is no email text
                if (string.IsNullOrEmpty(emailBody))
                {
                    using (FwSqlCommand procLogEmailHistory = new FwSqlCommand(conn, "dbo.logemailhistory", appConfig.DatabaseSettings.QueryTimeout))
                    {
                        procLogEmailHistory.AddParameter("@emailtaskid", emailtaskid);
                        procLogEmailHistory.AddParameter("@message", "No email account to send to.");
                        await procLogEmailHistory.ExecuteNonQueryAsync();
                        letSendEmail = false;
                    }
                }

                if (letSendEmail)
                {
                    bool emailFileAttach = false;
                    string emailbodyformat = string.Empty;
                    string emailprofilename = string.Empty;

                    using (FwSqlCommand qryGetEmailBodyFormat = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                    {
                        qryGetEmailBodyFormat.Add("select emailbodyformat = dbo.getemailbodyformat(@emailtaskid)");
                        qryGetEmailBodyFormat.AddParameter("@emailtaskid", emailtaskid);
                        await qryGetEmailBodyFormat.ExecuteNonQueryAsync();
                        emailbodyformat = qryGetEmailBodyFormat.GetField("emailbodyformat").ToString().TrimEnd();
                    }

                    // get the emailfileattachment
                    using (FwSqlCommand procCreateEmailDocument = new FwSqlCommand(conn, "dbo.createemaildocument", appConfig.DatabaseSettings.QueryTimeout))
                    {
                        procCreateEmailDocument.AddParameter("@emailtaskid", emailtaskid);
                        procCreateEmailDocument.AddParameter("@emailfileattachement", SqlDbType.Char, ParameterDirection.Output);
                        await procCreateEmailDocument.ExecuteNonQueryAsync();
                        emailFileAttach = procCreateEmailDocument.GetParameter("@emailfileattachement").ToBoolean();
                    }

                    // load the smtp server settings
                    string emailFromAddress = string.Empty;
                    string emailFromDisplay = string.Empty;
                    string emailServerAccountName = string.Empty;
                    string emailServerAccountPassword = string.Empty;
                    string emailServerAuthType = string.Empty;
                    string emailServerHost = string.Empty;
                    int emailServerPort = 25;
                    using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.Add("select top 1 *");
                        qry.Add("from emailreportcontrol with (nolock)");
                        await qry.ExecuteAsync();
                        emailFromAddress = qry.GetField("emailfromaddress").ToString().TrimEnd();
                        emailFromDisplay = qry.GetField("emailfromdisplay").ToString().TrimEnd();
                        emailServerAccountName = qry.GetField("accountname").ToString().TrimEnd();
                        emailServerAccountPassword = qry.GetField("accountpassword").ToString().TrimEnd();
                        emailServerAuthType = qry.GetField("authtype").ToString().TrimEnd();
                        emailServerHost = qry.GetField("host").ToString().TrimEnd();
                        emailServerPort = qry.GetField("port").ToInt32();
                    }

                    // start building the MailMessage
                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress(emailFromAddress, emailFromDisplay);
                        message.To.Add(emailTo);
                        if (emailCc.Length > 0)
                        {
                            message.CC.Add(emailCc);
                        }
                        message.Subject = emailSubject;

                        BeforeSendEmailRequest beforeSendEmailRequest = new BeforeSendEmailRequest();
                        beforeSendEmailRequest.AppEmailCategory = appemailCategory;
                        beforeSendEmailRequest.EmailTaskBody = emailBody;
                        beforeSendEmailRequest.EmailTaskId = emailtaskid;
                        beforeSendEmailRequest.LetSendEmail = letSendEmail;
                        beforeSendEmailRequest.MailMessage = message;
                        BeforeSendEmailResponse beforeSendEmailResponse = await funcBeforeSendEmail(beforeSendEmailRequest);

                        if (beforeSendEmailResponse.LetSendEmail) {
                            try
                            {
                                SmtpClient smtpClient = new SmtpClient(emailServerHost, emailServerPort);
                                smtpClient.Credentials = new NetworkCredential(emailServerAccountName, emailServerAccountPassword);
                                smtpClient.Send(message);

                                status = "SENT";

                                // log the email To for successful email
                                using (FwSqlCommand procLogEmailHistory = new FwSqlCommand(conn, "dbo.logemailhistory", appConfig.DatabaseSettings.QueryTimeout))
                                {
                                    procLogEmailHistory.AddParameter("@emailtaskid", emailtaskid);
                                    procLogEmailHistory.AddParameter("@message", "sent email to " + emailTo);
                                    await procLogEmailHistory.ExecuteNonQueryAsync();
                                }

                                // log the email CC for successful email
                                if (!string.IsNullOrEmpty(emailCc))
                                {
                                    using (FwSqlCommand procLogEmailHistory = new FwSqlCommand(conn, "dbo.logemailhistory", appConfig.DatabaseSettings.QueryTimeout))
                                    {
                                        procLogEmailHistory.AddParameter("@emailtaskid", emailtaskid);
                                        procLogEmailHistory.AddParameter("@message", "sent email cc to " + emailCc);
                                        await procLogEmailHistory.ExecuteNonQueryAsync();
                                        letSendEmail = false;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                status = "CANCEL";
                                using (FwSqlCommand procLogEmailHistory = new FwSqlCommand(conn, "dbo.logemailhistory", appConfig.DatabaseSettings.QueryTimeout))
                                {
                                    procLogEmailHistory.AddParameter("@emailtaskid", emailtaskid);
                                    procLogEmailHistory.AddParameter("@message", "Error in Email Service.");
                                    await procLogEmailHistory.ExecuteNonQueryAsync();
                                    letSendEmail = false;
                                }
                                using (FwSqlCommand procLogEmailHistory = new FwSqlCommand(conn, "dbo.logemailhistory", appConfig.DatabaseSettings.QueryTimeout))
                                {
                                    procLogEmailHistory.AddParameter("@emailtaskid", emailtaskid);
                                    procLogEmailHistory.AddParameter("@message", "Error: " + ex.Message);
                                    await procLogEmailHistory.ExecuteNonQueryAsync();
                                    letSendEmail = false;
                                }
                                using (FwSqlCommand procLogEmailHistory = new FwSqlCommand(conn, "dbo.logemailhistory", appConfig.DatabaseSettings.QueryTimeout))
                                {
                                    procLogEmailHistory.AddParameter("@emailtaskid", emailtaskid);
                                    procLogEmailHistory.AddParameter("@message", $"To: {emailTo}");
                                    await procLogEmailHistory.ExecuteNonQueryAsync();
                                    letSendEmail = false;
                                }
                                using (FwSqlCommand procLogEmailHistory = new FwSqlCommand(conn, "dbo.logemailhistory", appConfig.DatabaseSettings.QueryTimeout))
                                {
                                    procLogEmailHistory.AddParameter("@emailtaskid", emailtaskid);
                                    procLogEmailHistory.AddParameter("@message", $"CC: {emailCc}");
                                    await procLogEmailHistory.ExecuteNonQueryAsync();
                                    letSendEmail = false;
                                }
                                using (FwSqlCommand procLogEmailHistory = new FwSqlCommand(conn, "dbo.logemailhistory", appConfig.DatabaseSettings.QueryTimeout))
                                {
                                    procLogEmailHistory.AddParameter("@emailtaskid", emailtaskid);
                                    procLogEmailHistory.AddParameter("@message", $"Subject: {emailSubject}");
                                    await procLogEmailHistory.ExecuteNonQueryAsync();
                                    letSendEmail = false;
                                }
                                using (FwSqlCommand procLogEmailHistory = new FwSqlCommand(conn, "dbo.logemailhistory", appConfig.DatabaseSettings.QueryTimeout))
                                {
                                    procLogEmailHistory.AddParameter("@emailtaskid", emailtaskid);
                                    procLogEmailHistory.AddParameter("@message", $"Body: {emailBody}");
                                    await procLogEmailHistory.ExecuteNonQueryAsync();
                                    letSendEmail = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                status = "CANCEL";
                Console.Error.WriteLine(ex.Message + ex.StackTrace);
            }
            finally
            {
                // update email task status
                using (FwSqlCommand cmdUpdateEmailTask = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    cmdUpdateEmailTask.Add("update emailtask");
                    cmdUpdateEmailTask.Add("set status = @status");
                    cmdUpdateEmailTask.Add("where emailtaskid = @emailtaskid");
                    cmdUpdateEmailTask.AddParameter("@emailtaskid", emailtaskid);
                    cmdUpdateEmailTask.AddParameter("@status", status);
                    await cmdUpdateEmailTask.ExecuteNonQueryAsync();
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ReportEmail> DeserializeReportEmail(string appEmailCategory, string serializedReportEmail)
        {
            var serializer = new XmlSerializer(typeof(ReportEmail));
            ReportEmail reportEmail = null;
            using (var reader = new StringReader(serializedReportEmail))
            {
                try
                {
                    reportEmail = (ReportEmail)serializer.Deserialize(reader);
                    await Task.CompletedTask;
                    return reportEmail;
                }
                catch (InvalidOperationException ex)
                {
                    throw new Exception($"{appEmailCategory} email template must be a valid <ReportEmail>", ex);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
