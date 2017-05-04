using System;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Web;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using JsonFx.Json;

namespace Fw.Json.HttpHandlers
{
    public class FwDataUrlToImgHandler : IHttpHandler
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
            StringBuilder html;
            string dataurl;
            dynamic response;
            string jsonResponse;
            JsonWriter jsonWriter;

            try
            {
                html = new StringBuilder();
                if (context.Request.QueryString["dataurl"]  == null) throw new Exception("dataurl is required.");
                dataurl = context.Request.QueryString["dataurl"];
                context.Response.Clear();
                context.Response.ContentType = "text/html";
                context.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate"); 
                context.Response.AddHeader("Pragma", "no-cache"); 
                context.Response.AddHeader("Expires", "0");
                html.Append("<img src=\"" + dataurl + "\" />");
                context.Response.Write(html);
                context.Response.AddHeader("Content-Length", html.Length.ToString());
                context.Response.StatusCode = 200;
                context.Response.Flush();
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