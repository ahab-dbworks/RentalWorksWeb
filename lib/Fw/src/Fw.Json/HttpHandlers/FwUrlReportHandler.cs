using System;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Web;
using Fw.Json.Utilities;
using JsonFx.Json;

namespace Fw.Json.HttpHandlers
{
    public class FwUrlReportHandler : IHttpHandler
    {
        //public static FwSqlConnection Connection {get;set;}
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
            string format, headerhtml, bodyhtml, footerhtml, attachment, filename, headerurl, bodyurl, footerurl, html;
            WebRequest webRequest;
            WebResponse webResponse;
            dynamic response;
            string jsonResponse;
            JsonWriter jsonWriter;

            try
            {
                if (context.Request.QueryString["format"]  == null)    throw new Exception("format is required.");
                if (context.Request.QueryString["headerurl"]  == null) throw new Exception("headerurl is required.");
                if (context.Request.QueryString["bodyurl"]    == null) throw new Exception("bodyurl is required.");
                if (context.Request.QueryString["footerurl"]  == null) throw new Exception("footerurl is required.");
                format             = context.Request.QueryString["format"];
                headerurl          = context.Request.QueryString["headerurl"];
                bodyurl            = context.Request.QueryString["bodyurl"];
                footerurl          = context.Request.QueryString["footerurl"];
                renderAsAttachment = false;
                attachment         = renderAsAttachment ? "attachment;" : string.Empty;
                filename           = "report";
            
                webRequest            = WebRequest.Create(headerurl);
                webResponse           = webRequest.GetResponse();
                using (Stream dataStream = webResponse.GetResponseStream ())
                using (StreamReader reader = new StreamReader (dataStream))
                {
                    headerhtml = reader.ReadToEnd();
                }

                webRequest            = WebRequest.Create(bodyurl);
                webResponse           = webRequest.GetResponse();
                using (Stream dataStream = webResponse.GetResponseStream ())
                using (StreamReader reader = new StreamReader (dataStream))
                {
                    bodyhtml = reader.ReadToEnd();
                }

                webRequest            = WebRequest.Create(footerurl);
                webResponse           = webRequest.GetResponse();
                using (Stream dataStream = webResponse.GetResponseStream ())
                using (StreamReader reader = new StreamReader (dataStream))
                {
                    footerhtml = reader.ReadToEnd();
                }
            
                context.Response.Clear();
                context.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate"); 
                context.Response.AddHeader("Pragma", "no-cache"); 
                context.Response.AddHeader("Expires", "0");
            
                if (format == "pdf") 
                {
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", attachment + "filename=\"" + filename + ".pdf\";"); 
                    using (MemoryStream pdfOutputStream = new MemoryStream())
                    {
                        EO.Pdf.HtmlToPdf.Options.PageSize   = new SizeF(11f, 8.5f);
                        EO.Pdf.HtmlToPdf.Options.OutputArea = new RectangleF(0.25f, 1.25f, 10.50f, 7.0f);
                        EO.Pdf.HtmlToPdf.Options.HeaderHtmlFormat = headerhtml;
                        EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = footerhtml;

                        pdfResult = EO.Pdf.HtmlToPdf.ConvertHtml(bodyhtml, pdfOutputStream);
                        context.Response.AddHeader("Content-Length", pdfOutputStream.Length.ToString());
                        pdfOutputStream.WriteTo(context.Response.OutputStream);

                        context.Response.StatusCode = 200;
                        context.Response.Flush();
                        context.Response.SuppressContent = true;
                    }
                }
                else if (format == "html")
                {
                    context.Response.ContentType = "text/html";
                    html = headerhtml + bodyhtml;
                    context.Response.Write(html);
                    context.Response.AddHeader("Content-Length", html.Length.ToString());
                    context.Response.StatusCode = 200;
                    context.Response.Flush();
                }
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