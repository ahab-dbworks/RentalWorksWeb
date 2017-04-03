using System;
using System.Dynamic;
using System.IO;
using System.Web;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using JsonFx.Json;

namespace Fw.Json.HttpHandlers
{
    public class FwWebContentReportHandler : IHttpHandler
    {
        public static FwSqlConnection Connection {get;set;}
        //------------------------------------------------------------------------------------
        public bool IsReusable
        {
            get { return false; }
        }
        //------------------------------------------------------------------------------------
        public void ProcessRequest(HttpContext context)
        {
            EO.Pdf.HtmlToPdfResult pdfResult;
            bool renderAsAttachment;
            string html, attachment, filename, category, uniqueid1;
            FwSqlCommand qry;
            dynamic response;
            string jsonResponse;
            JsonWriter jsonWriter;

            try
            {
                if (context.Request.QueryString["category"]  == null) throw new Exception("category is required.");
                if (context.Request.QueryString["uniqueid1"] == null) throw new Exception("uniqueid1 is required.");
                renderAsAttachment = false;
                attachment = renderAsAttachment ? "attachment;" : string.Empty;
                filename   = "report";
                qry        = new FwSqlCommand(FwWebContentReportHandler.Connection);
                category   = context.Request.QueryString["category"];
                uniqueid1  = context.Request.QueryString["uniqueid1"];
                qry.Add("select top 1 note");
                qry.Add("from webcontentview with(nolock)");
                qry.Add("where category = @category");
                qry.Add("  and uniqueid1 = @uniqueid1");
                qry.AddParameter("@category", category);
                qry.AddParameter("@uniqueid1", uniqueid1);
                qry.Execute();
                html = qry.GetField("note").ToString();
                context.Response.Clear();
                context.Response.ContentType = "application/pdf";
                context.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate"); 
                context.Response.AddHeader("Pragma", "no-cache"); 
                context.Response.AddHeader("Expires", "0");
                context.Response.AddHeader("Content-Disposition", attachment + "filename=\"" + filename + ".pdf\";"); 
                using (MemoryStream pdfOutputStream = new MemoryStream())
                {
                    pdfResult = EO.Pdf.HtmlToPdf.ConvertHtml(html.ToString(), pdfOutputStream);
                    context.Response.AddHeader("Content-Length", pdfOutputStream.Length.ToString());
                    pdfOutputStream.WriteTo(context.Response.OutputStream);
                }
                context.Response.StatusCode = 200;
            }
            catch(Exception ex)
            {
                response                     = new ExpandoObject();
                response.exception           = ex.Message;
                response.stacktrace          = ex.StackTrace;
                response.serverVersion       = FwVersion.Current.FullVersion;
                jsonWriter                   = new JsonWriter();
                jsonResponse                 = jsonWriter.Write(response);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonResponse);
                context.Response.StatusCode = 200;
                context.Response.Flush();
                context.Response.SuppressContent = true;
            }
            finally
            {
                context.ApplicationInstance.CompleteRequest();
                context.Response.End();
            }
        }
        //------------------------------------------------------------------------------------
    }
}