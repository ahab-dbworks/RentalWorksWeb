using System;
using System.Dynamic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using Fw.Json.Utilities;
using JsonFx.Json;

namespace Fw.Json.HttpHandlers
{
    public class FwDownloadHandler : IHttpHandler
    {
        //---------------------------------------------------------------------------------------------
        public bool IsReusable
        {
            get { return true; }
        }
        //---------------------------------------------------------------------------------------------
        public void ProcessRequest(HttpContext context)
        {
            string filename, folderpath, filepath, extension, saveas, asattachment, contentdisposition, jsonResponse;
            DirectoryInfo directoryInfo;
            FileInfo[] pdfInfoList;
            dynamic response;
            JsonWriter jsonWriter;

            try 
            {
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.Cache.SetNoStore();
                context.Response.Cache.SetExpires(DateTime.MinValue);
                if (context.Request.QueryString["filename"] == null) throw new ArgumentNullException("filename");
                if (context.Request.QueryString["saveas"] == null)   throw new ArgumentNullException("saveas");
                if (context.Request.QueryString["asattachment"] == null) throw new ArgumentNullException("asattachment");
                filename   = Path.GetFileName(context.Request.QueryString["filename"]);
                saveas     = context.Request.QueryString["saveas"];
                saveas     = saveas.Replace("/", "-");
                saveas     = saveas.Replace("..", string.Empty);
                saveas     = Regex.Replace(saveas, @"[^\w\s\-\+\.]", string.Empty);
                saveas     = Path.GetFileName(saveas);
                asattachment = context.Request.QueryString["asattachment"];
                extension  = Path.GetExtension(filename).ToLower().Replace(".", string.Empty);
                folderpath = context.Server.MapPath("~/App_Data/Temp/Downloads");
                filepath   = Path.Combine(folderpath, filename);
                if (File.Exists(filepath))
                {
                    context.Response.StatusCode  = 200;
                    context.Response.ContentType = FwAppDocumentHandler.GetMimeType(extension);
                    context.Response.WriteFile(filepath);
                    contentdisposition = ((asattachment == "true") ? "attachment; " : "inline; ") + "filename=\"" + saveas + "\"";
                    context.Response.Headers["Content-Disposition"] = contentdisposition;
                    context.Response.Flush();
                }
                else
                {
                    context.Response.StatusCode  = 404;
                    context.Response.Flush();
                }
            
                //try
                //{
                //    // clean up the file.
                //    if (File.Exists(filepath))
                //    {
                //        for (int i = 0; i < 10; i++)
                //        {
                //            try 
                //            {
                //                File.Delete(filepath);
                //                break;
                //            }
                //            catch
                //            {
                //                Thread.Sleep(1000);
                //            }
                //            finally{}
                //        }
                    
                //    }
                //} catch { }

                try
                {
                    // cleanup any other files in the temp folder
                    directoryInfo = new DirectoryInfo(folderpath);
                    pdfInfoList   = directoryInfo.GetFiles("*");
                    foreach (FileInfo pdfInfo in pdfInfoList)
                    {
                        if (pdfInfo.CreationTime.AddDays(1) < DateTime.Now)
                        {
                            pdfInfo.Delete();
                        }
                    }
                } catch { }
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
        //---------------------------------------------------------------------------------------------
    }
}
