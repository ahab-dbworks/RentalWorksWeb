using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Web;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using JsonFx.Json;

namespace Fw.Json.HttpHandlers
{
    public class FwAppDocumentHandler : IHttpHandler
    {
        //------------------------------------------------------------------------------------
        public bool IsReusable
        {
            get { return false; }
        }
        //------------------------------------------------------------------------------------
        public void ProcessRequest(HttpContext context)
        {  
            string operation;
            dynamic response;
            string jsonResponse;
            JsonWriter jsonWriter;
            
            try 
            {
                switch (context.Request.HttpMethod)
                {
                    case "GET":
                        operation = (context.Request.QueryString["operation"] != null) ? context.Request.QueryString["operation"] : string.Empty;
                        switch(operation)
                        {
                            case "getappimage":
                                GetAppImage(context);
                                break;
                            case "getiframehtml":
                                GetIframeHtml(context);
                                break;
                            default:
                                throw new Exception("Unsupported operation.");
                        }
                        break;
                    case "POST":
                        PostFile(context);
                        break;
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
        void GetIframeHtml(HttpContext context)
        {
            StringBuilder html;
            string htmlResult, authtoken, appdocumentid, documenttypeid, uniqueid1, uniqueid2, appimageid;

            if(context.Request.Cookies["authtoken"]          == null) throw new ArgumentNullException("authtoken",     "FwAppDocumentHandler.GetIframeHtml: authtoken is required.");
            if(context.Request.QueryString["appdocumentid"]  == null) throw new ArgumentNullException("appdocumentid", "FwAppDocumentHandler.GetIframeHtml: appdocumentid is required.");
            if(context.Request.QueryString["documenttypeid"] == null) throw new ArgumentNullException("documenttypeid","FwAppDocumentHandler.GetIframeHtml: documenttypeid is required.");
            if(context.Request.QueryString["uniqueid1"]      == null) throw new ArgumentNullException("uniqueid1",     "FwAppDocumentHandler.GetIframeHtml: uniqueid1 is required.");
            if(context.Request.QueryString["uniqueid2"]      == null) throw new ArgumentNullException("uniqueid2",     "FwAppDocumentHandler.GetIframeHtml: uniqueid2 is required.");
            if(context.Request.QueryString["appimageid"]     == null) throw new ArgumentNullException("appdocumentid", "FwAppDocumentHandler.GetIframeHtml: appdocumentid is required.");
            authtoken       = context.Request.Cookies["authtoken"].Value;
            appdocumentid   = context.Request.QueryString["appdocumentid"];
            documenttypeid  = context.Request.QueryString["documenttypeid"];
            uniqueid1       = context.Request.QueryString["uniqueid1"];
            uniqueid2       = context.Request.QueryString["uniqueid2"];
            appimageid      = context.Request.QueryString["appimageid"];
            html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html style=\"overflow:hidden;\">");
            html.AppendLine("<head>");
            html.AppendLine("<title></title>");
            html.AppendLine("</head>");
            html.AppendLine("<body style=\"margin:0;padding:0;\">");
            html.AppendLine("  <form name=\"form1\" action=\"fwappdocument.ashx?operation=insertappimage\" method=\"post\" enctype=\"multipart/form-data\" target=\"_self\">");
            html.AppendLine("    <div>");
            html.AppendLine("      <span style=\"width:16px;height:16px;border:1px solid #777777;border-radius:3px;background-color:#eeeeee;background-image:url(theme/fwimages/icons/128/ellipsisbutton.002.png);background-size:16px 16px;background-repeat:no-repeat;\"><!--");
            html.AppendLine("     --><input id=\"file1\" name=\"file1\" type=\"file\" style=\"width:16px;height:16px;opacity:0;\" onchange=\"if (this.value.length > 0) {document.forms[0].submit();}\" /><!--");
            html.AppendLine("   --></span>");
            html.AppendLine("      <div style=\"display:none\">");
            html.AppendLine("        <input name=\"appdocumentid\" type=\"hidden\" value=\"" + GetSafeHtmlAttribute(appdocumentid) + "\" />");
            html.AppendLine("        <input name=\"documenttypeid\" type=\"hidden\" value=\"" + GetSafeHtmlAttribute(documenttypeid) + "\" />");
            html.AppendLine("        <input name=\"uniqueid1\" type=\"hidden\" value=\"" + GetSafeHtmlAttribute(uniqueid1) + "\" />");
            html.AppendLine("        <input name=\"uniqueid2\" type=\"hidden\" value=\"" + GetSafeHtmlAttribute(uniqueid2) + "\" />");
            html.AppendLine("        <input name=\"appimageid\" type=\"hidden\" value=\"" + GetSafeHtmlAttribute(appimageid) + "\" />");
            html.AppendLine("      </div>");
            html.AppendLine("    </div>");
            html.AppendLine("  </form>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");
            htmlResult = html.ToString();
            context.Response.Clear();
            context.Response.ContentType = "text/html";
            context.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate"); 
            context.Response.AddHeader("Pragma", "no-cache"); 
            context.Response.AddHeader("Expires", "0"); 
            context.Response.AddHeader("Content-Length", htmlResult.Length.ToString());
            context.Response.Write(html.ToString());
            context.Response.StatusCode = 200;
            context.Response.Flush();
        }
        //------------------------------------------------------------------------------------
        string GetSafeHtmlAttribute(string attributeValue)
        {
            return attributeValue.Replace("\"", string.Empty);
        }
        //------------------------------------------------------------------------------------
        void GetAppImage(HttpContext context)
        {
            string appimageid, filename;
            FwSqlCommand qry;
            byte[] documentBuffer = null;
            string mimetype, extension;

            if (string.IsNullOrEmpty(context.Request.QueryString["appimageid"])) throw new ArgumentNullException("appimageid", "FwAppDocumentHandler.ProcessRequest: Missing query string parameter: appimageid");
            if (string.IsNullOrEmpty(context.Request.QueryString["filename"]))   throw new ArgumentNullException("filename", "FwAppDocumentHandler.ProcessRequest: Missing query string parameter: filename");
            appimageid = FwCryptography.AjaxDecrypt(context.Request.QueryString["appimageid"]);
            filename   = FwConvert.StripNonAlphaNumericCharacters(context.Request.QueryString["filename"]);
            qry        = new FwSqlCommand(FwSqlConnection.AppConnection);
            qry.Add("select top 1 image, extension");
            qry.Add("from appimage");
            qry.Add("where appimageid = @appimageid");
            qry.Parameters.AddWithValue("@appimageid", appimageid);
            qry.Execute();
            documentBuffer = qry.GetField("image").ToByteArray();
            extension      = FwConvert.StripNonAlphaNumericCharacters(qry.GetField("extension").ToString().Trim().ToLower());
            mimetype       = FwAppDocumentHandler.GetMimeType(extension);
            if (documentBuffer != null)
            {
                context.Response.Clear();
                context.Response.ContentType = mimetype;
                context.Response.AddHeader("Pragma", "public"); 
                context.Response.AddHeader("Cache-Control", "max-age=0"); 
                context.Response.AddHeader("Content-Length", documentBuffer.Length.ToString());
                context.Response.AddHeader("Content-Disposition", "filename=\"" + filename + "." + extension + "\";"); 
                context.Response.BinaryWrite(documentBuffer);
                context.Response.StatusCode = 200;
                context.Response.Flush();
                context.Response.SuppressContent = true;
            }
        }
        //------------------------------------------------------------------------------------        
        void PostFile(HttpContext context)
        {
            HttpPostedFile file;
            byte[] image, thumbnail;
            string authtoken, appdocumentid, documenttypeid, uniqueid1, uniqueid2, fileextension;
            int width, height;
            FwDateTime attachDateTime;

            if (context.Request.Files[0]               == null) throw new ArgumentNullException("file1",          "FwAppDocumentHandler.PostFile: No files were posted.");
            if (context.Request.Cookies["authtoken"]   == null) throw new ArgumentNullException("authtoken",      "FwAppDocumentHandler.PostFile: authtoken is required.");
            if (context.Request.Form["appdocumentid"]  == null) throw new ArgumentNullException("appdocumentid",  "FwAppDocumentHandler.PostFile: appdocumentid is required.");
            if (context.Request.Form["uniqueid1"]      == null) throw new ArgumentNullException("uniqueid1",      "FwAppDocumentHandler.PostFile: uniqueid1 is required.");
            if (context.Request.Form["uniqueid2"]      == null) throw new ArgumentNullException("uniqueid2",      "FwAppDocumentHandler.PostFile: uniqueid2 is required.");
            if (context.Request.Form["documenttypeid"] == null) throw new ArgumentNullException("documenttypeid", "FwAppDocumentHandler.PostFile: documenttypeid is required.");
            file           = context.Request.Files[0];
            authtoken      = (context.Request.Cookies["authtoken"]   != null) ? context.Request.Cookies["authtoken"].Value : string.Empty;
            appdocumentid  = (context.Request.Form["appdocumentid"]  != null) ? FwCryptography.AjaxDecrypt(context.Request.Form["appdocumentid"]) : string.Empty;
            documenttypeid = (context.Request.Form["documenttypeid"] != null) ? FwCryptography.AjaxDecrypt(context.Request.Form["documenttypeid"]) : string.Empty;
            uniqueid1      = (context.Request.Form["uniqueid1"]      != null) ? FwCryptography.AjaxDecrypt(context.Request.Form["uniqueid1"]) : string.Empty;
            uniqueid2      = (context.Request.Form["uniqueid2"]      != null) ? FwCryptography.AjaxDecrypt(context.Request.Form["uniqueid2"]) : string.Empty;
            fileextension  = Path.GetExtension(file.FileName).ToLower().Replace(".", string.Empty);
            width          = 0;
            height         = 0;
            thumbnail      = new byte[0];
            image          = new byte[file.ContentLength];
            using (BinaryReader reader = new BinaryReader(file.InputStream))
            {
                reader.Read(image, 0, file.ContentLength);
            }
            switch(fileextension) 
            {
                case "jpg":
                case "jpeg":
                case "gif":
                case "tif":
                case "tiff":
                case "bmp":
                case "png":
                    MemoryStream ms = null;
                    try
                    {
                        ms = new MemoryStream(image);
                        using (Bitmap bitmap = new Bitmap(ms))
                        {
                            ms = null;
                            height = bitmap.Height;
                            width = bitmap.Width;
                        }
                    }
                    finally
                    {
                        if (ms != null)
                        {
                            ms.Dispose();
                        }
                    }
                    
                    thumbnail = FwGraphics.GetJpgThumbnail(image);
                    break;
            }
            attachDateTime = DateTime.UtcNow;
            UpdateAppDocumentImage(appdocumentid, documenttypeid, uniqueid1, uniqueid2, width, height, image, thumbnail, fileextension.ToUpper(), attachDateTime);
            context.Response.Flush();
        }
        //------------------------------------------------------------------------------------        
        //void InsertAppDocument(string uniqueid1, string uniqueid2, string documenttypeid, string projectid, 
        //    string description, string extension, string rectype, int width, int height, byte[] image, 
        //    FwDateTime attachdate, FwDateTime expiration, FwDateTime received, string contactid, string inputbyusersid)
        //{
        //    FwDateTime datestamp;
        //    string appdocumentid, appimageid;
        //    byte[] thumbnail;
            
        //    thumbnail = FwGraphics.GetJpgThumbnail(new FwSqlConnection(FwAppDocumentHandler.Database), image);
        //    datestamp = DateTime.UtcNow;
        //    using(FwSqlConnection conn = new FwSqlConnection(FwAppDocumentHandler.Database))
        //    {
        //        conn.Open();
        //        using(SqlTransaction transaction = conn.GetConnection().BeginTransaction())
        //        {
        //            // insert appdocument
        //            appdocumentid = FwSqlData.GetNextId(conn, transaction);
        //            using (FwSqlCommand cmd = new FwSqlCommand(conn))
        //            {
        //                cmd.Transaction = transaction;
        //                cmd.Add("insert into appdocument(appdocumentid, datestamp, description, attachdate, uniqueid1, uniqueid2, uniqueid1int, attachtime, contactid, documenttypeid, expiration, inactive, inputbyusersid, projectid, received, uploadpending, attachtoemail)");
        //                cmd.Add("values(@appdocumentid, @datestamp, @description, @attachdate, @uniqueid1, @uniqueid2, @uniqueid1int, @attachtime, @contactid, @documenttypeid, @expiration, @inactive, @inputbyusersid, @projectid, @received, @uploadpending, @attachtoemail)");
        //                cmd.AddParameter("@appdocumentid",  appdocumentid);
        //                cmd.AddParameter("@datestamp",      datestamp.GetSqlValue());
        //                cmd.AddParameter("@description",    description);
        //                cmd.AddParameter("@attachdate",     attachdate.GetSqlDate());
        //                cmd.AddParameter("@uniqueid1",      uniqueid1);
        //                cmd.AddParameter("@uniqueid2",      uniqueid2);
        //                cmd.AddParameter("@uniqueid1int",   0);
        //                cmd.AddParameter("@attachtime",     attachdate.GetSqlTime());
        //                cmd.AddParameter("@contactid",      contactid);
        //                cmd.AddParameter("@documenttypeid", documenttypeid);
        //                cmd.AddParameter("@expiration",     expiration.GetSqlDate());
        //                cmd.AddParameter("@inactive",       'F');
        //                cmd.AddParameter("@inputbyusersid", inputbyusersid);
        //                cmd.AddParameter("@projectid",      projectid);
        //                cmd.AddParameter("@received",       received.GetSqlDate());
        //                cmd.AddParameter("@uploadpending",  'F');
        //                cmd.AddParameter("@attachtoemail",  'F');
        //                cmd.ExecuteNonQuery(false);
        //            }
                    
        //            // insert appimage
        //            appimageid = FwSqlData.GetNextId(conn, transaction);
        //            using (FwSqlCommand cmd = new FwSqlCommand(conn))
        //            {
        //                cmd.Transaction = transaction;
        //                cmd.Add("insert into appimage(appimageid, uniqueid1, uniqueid2, uniqueid3, description, extension, rectype, height, width, thumbnail, image, datestamp)");
        //                cmd.Add("values(@appimageid, @uniqueid1, @uniqueid2, @uniqueid3, @description, @extension, @rectype, @height, @width, @thumbnail, @image, @datestamp)");
        //                cmd.AddParameter("@appimageid",  appimageid);
        //                cmd.AddParameter("@uniqueid1",   appdocumentid);
        //                cmd.AddParameter("@uniqueid2",   string.Empty);
        //                cmd.AddParameter("@uniqueid3",   string.Empty);
        //                cmd.AddParameter("@description", description);
        //                cmd.AddParameter("@extension",   extension);
        //                cmd.AddParameter("@rectype",     rectype);
        //                cmd.AddParameter("@height",      height);
        //                cmd.AddParameter("@width",       width);
        //                cmd.AddParameter("@thumbnail",   thumbnail);
        //                cmd.AddParameter("@image",       image);
        //                cmd.AddParameter("@datestamp",   datestamp);
        //                cmd.ExecuteNonQuery(false);
        //            }
        //            transaction.Commit();
        //        }
        //    }
        //}
        //------------------------------------------------------------------------------------        
        void UpdateAppDocumentImage(string appdocumentid, string documenttypeid, string uniqueid1, string uniqueid2, int width, int height, byte[] image, byte[] thumbnail, string extension, FwDateTime attachDateTime)
        {
            FwDateTime datestamp;
            string appimageid;
            
            datestamp = DateTime.UtcNow;
            using(FwSqlConnection conn = new FwSqlConnection(FwSqlConnection.AppDatabase))
            {
                conn.Open();
                using(SqlTransaction transaction = conn.GetConnection().BeginTransaction())
                {
                    
                    appimageid = FwSqlData.GetNextId(conn, transaction);
                    if (string.IsNullOrEmpty(appdocumentid))
                    {
                        appdocumentid = FwSqlData.GetNextId(conn, transaction);
                        // update the attachdate/time on the appdocument
                        using (FwSqlCommand cmd = new FwSqlCommand(conn))
                        {
                            cmd.Transaction = transaction;
                            cmd.Add("insert appdocument(appdocumentid, documenttypeid, uniqueid1, uniqueid2, attachdate, attachtime)");
                            cmd.Add("values(@appdocumentid, @documenttypeid, @uniqueid1, @uniqueid2, @attachdate, @attachtime)");
                            cmd.AddParameter("@appdocumentid",  appdocumentid);
                            cmd.AddParameter("@documenttypeid",  documenttypeid);
                            cmd.AddParameter("@uniqueid1",  uniqueid1);
                            cmd.AddParameter("@uniqueid2",  uniqueid2);
                            cmd.AddParameter("@attachdate", attachDateTime.GetSqlDate());
                            cmd.AddParameter("@attachtime", attachDateTime.GetSqlTime());
                            cmd.ExecuteNonQuery(false);
                        }
                    }
                    else
                    {
                        // update the attachdate/time on the appdocument
                        using (FwSqlCommand cmd = new FwSqlCommand(conn))
                        {
                            cmd.Transaction = transaction;
                            cmd.Add("update appdocument");
                            cmd.Add("set attachdate = @attachdate,");
                            cmd.Add("    attachtime = @attachtime");
                            cmd.Add("where appdocumentid = @appdocumentid");
                            cmd.AddParameter("@appdocumentid",  appdocumentid);
                            cmd.AddParameter("@attachdate", attachDateTime.GetSqlDate());
                            cmd.AddParameter("@attachtime", attachDateTime.GetSqlTime());
                            cmd.ExecuteNonQuery(false);
                        
                        }
                    }
                    
                    // delete any existing appimages
                    using (FwSqlCommand cmd = new FwSqlCommand(conn))
                    {
                        cmd.Transaction = transaction;
                        cmd.Add("delete appimage");
                        cmd.Add("where uniqueid1 = @uniqueid1");
                        cmd.AddParameter("@uniqueid1",  appdocumentid);
                        cmd.ExecuteNonQuery(false);
                    }
                    
                    // insert appimage
                    using (FwSqlCommand cmd = new FwSqlCommand(conn))
                    {
                        cmd.Transaction = transaction;
                        cmd.Add("insert into appimage(appimageid, uniqueid1, uniqueid2, uniqueid3, description, extension, rectype, height, width, thumbnail, image, datestamp)");
                        cmd.Add("values(@appimageid, @uniqueid1, @uniqueid2, @uniqueid3, @description, @extension, @rectype, @height, @width, @thumbnail, @image, @datestamp)");
                        cmd.AddParameter("@appimageid",  appimageid);
                        cmd.AddParameter("@uniqueid1",   appdocumentid);
                        cmd.AddParameter("@uniqueid2",   string.Empty);
                        cmd.AddParameter("@uniqueid3",   string.Empty);
                        cmd.AddParameter("@description", string.Empty);
                        cmd.AddParameter("@extension",   extension);
                        cmd.AddParameter("@rectype",     "F");
                        cmd.AddParameter("@height",      height);
                        cmd.AddParameter("@width",       width);
                        cmd.AddParameter("@thumbnail",   thumbnail);
                        cmd.AddParameter("@image",       image);
                        cmd.AddParameter("@datestamp",   datestamp.GetSqlValue());
                        cmd.ExecuteNonQuery(false);
                    }
                    transaction.Commit();
                }
            }
        }
        //------------------------------------------------------------------------------------        
        public static string GetMimeType(string extension)
        {
            string fileextension = extension.Trim().ToLower();
            switch(fileextension)
            {
                case "3dm": return "x-world/x-3dmf";
                case "3dmf": return "x-world/x-3dmf";
                case "a": return "application/octet-stream";
                case "aab": return "application/x-authorware-bin";
                case "aam": return "application/x-authorware-map";
                case "aas": return "application/x-authorware-seg";
                case "abc": return "text/vnd.abc";
                case "acgi": return "text/html";
                case "afl": return "video/animaflex";
                case "ai": return "application/postscript";
                case "aif": return "audio/x-aiff";
                case "aifc": return "audio/x-aiff";
                case "aiff": return "audio/x-aiff";
                case "aim": return "application/x-aim";
                case "aip": return "text/x-audiosoft-intra";
                case "ani": return "application/x-navi-animation";
                case "aos": return "application/x-nokia-9000-communicator-add-on-software";
                case "aps": return "application/mime";
                case "arc": return "application/octet-stream";
                case "arj": return "application/arj";
                case "art": return "image/x-jg";
                case "asf": return "video/x-ms-asf";
                case "asm": return "text/x-asm";
                case "asp": return "text/asp";
                case "asx": return "video/x-ms-asf";
                case "au": return "audio/x-au";
                case "avi": return "video/avi";
                case "avs": return "video/avs-video";
                case "bcpio": return "application/x-bcpio";
                case "bin": return "application/x-binary";
                case "bm": return "image/bmp";
                case "bmp": return "image/bmp";
                case "boo": return "application/book";
                case "book": return "application/book";
                case "boz": return "application/x-bzip2";
                case "bsh": return "application/x-bsh";
                case "bz": return "application/x-bzip";
                case "bz2": return "application/x-bzip2";
                case "c": return "text/x-c";
                case "c++": return "text/plain";
                case "cat": return "application/vnd.ms-pki.seccat";
                case "cc": return "text/x-c";
                case "ccad": return "application/clariscad";
                case "cco": return "application/x-cocoa";
                case "cdf": return "application/x-cdf";
                case "cer": return "application/pkix-cert";
                case "cha": return "application/x-chat";
                case "chat": return "application/x-chat";
                case "class": return "application/java";
                case "com": return "text/plain";
                case "conf": return "text/plain";
                case "cpio": return "application/x-cpio";
                case "cpp": return "text/x-c";
                case "cpt": return "application/x-compactpro";
                case "crl": return "application/pkcs-crl";
                case "crt": return "application/pkix-cert";
                case "csh": return "text/x-script.csh";
                case "css": return "text/css";
                case "cxx": return "text/plain";
                case "dcr": return "application/x-director";
                case "deepv": return "application/x-deepv";
                case "def": return "text/plain";
                case "der": return "application/x-x509-ca-cert";
                case "dif": return "video/x-dv";
                case "dir": return "application/x-director";
                case "dl": return "video/x-dl";
                case "doc": return "application/msword";
                case "dot": return "application/msword";
                case "dp": return "application/commonground";
                case "drw": return "application/drafting";
                case "dump": return "application/octet-stream";
                case "dv": return "video/x-dv";
                case "dvi": return "application/x-dvi";
                case "dwf": return "model/vnd.dwf";
                case "dwg": return "image/x-dwg";
                case "dxf": return "application/dxf";
                case "dxr": return "application/x-director";
                case "el": return "text/x-script.elisp";
                case "elc": return "application/x-elc";
                case "env": return "application/x-envoy";
                case "eps": return "application/postscript";
                case "es": return "application/x-esrehber";
                case "etx": return "text/x-setext";
                case "evy": return "application/x-envoy";
                case "exe": return "application/octet-stream";
                case "f": return "text/x-fortran";
                case "f77": return "text/x-fortran";
                case "f90": return "text/x-fortran";
                case "fdf": return "application/vnd.fdf";
                case "fif": return "image/fif";
                case "fli": return "video/x-fli";
                case "flo": return "image/florian";
                case "flx": return "text/vnd.fmi.flexstor";
                case "fmf": return "video/x-atomic3d-feature";
                case "for": return "text/x-fortran";
                case "fpx": return "image/vnd.fpx";
                case "frl": return "application/freeloader";
                case "funk": return "audio/make";
                case "g": return "text/plain";
                case "g3": return "image/g3fax";
                case "gif": return "image/gif";
                case "gl": return "video/x-gl";
                case "gsd": return "audio/x-gsm";
                case "gsm": return "audio/x-gsm";
                case "gsp": return "application/x-gsp";
                case "gss": return "application/x-gss";
                case "gtar": return "application/x-gtar";
                case "gz": return "application/x-gzip";
                case "gzip": return "application/x-gzip";
                case "h": return "text/x-h";
                case "hdf": return "application/x-hdf";
                case "help": return "application/x-helpfile";
                case "hgl": return "application/vnd.hp-hpgl";
                case "hh": return "text/x-h";
                case "hlb": return "text/x-script";
                case "hlp": return "application/hlp";
                case "hpg": return "application/vnd.hp-hpgl";
                case "hpgl": return "application/vnd.hp-hpgl";
                case "hqx": return "application/binhex";
                case "hta": return "application/hta";
                case "htc": return "text/x-component";
                case "htm": return "text/html";
                case "html": return "text/html";
                case "htmls": return "text/html";
                case "htt": return "text/webviewhtml";
                case "htx": return "text/html";
                case "ice": return "x-conference/x-cooltalk";
                case "ico": return "image/x-icon";
                case "idc": return "text/plain";
                case "ief": return "image/ief";
                case "iefs": return "image/ief";
                case "iges": return "model/iges";
                case "igs": return "model/iges";
                case "ima": return "application/x-ima";
                case "imap": return "application/x-httpd-imap";
                case "inf": return "application/inf";
                case "ins": return "application/x-internett-signup";
                case "ip": return "application/x-ip2";
                case "isu": return "video/x-isvideo";
                case "it": return "audio/it";
                case "iv": return "application/x-inventor";
                case "ivr": return "i-world/i-vrml";
                case "ivy": return "application/x-livescreen";
                case "jam": return "audio/x-jam";
                case "jav": return "text/x-java-source";
                case "java": return "text/x-java-source";
                case "jcm": return "application/x-java-commerce";
                case "jfif": return "image/jpeg";
                case "jfif-tbnl": return "image/jpeg";
                case "jpe": return "image/jpeg";
                case "jpeg": return "image/jpeg";
                case "jpg": return "image/jpeg";
                case "jps": return "image/x-jps";
                case "js": return "text/javascript";
                case "jut": return "image/jutvision";
                case "kar": return "music/x-karaoke";
                case "ksh": return "text/x-script.ksh";
                case "la": return "audio/nspaudio";
                case "lam": return "audio/x-liveaudio";
                case "latex": return "application/x-latex";
                case "lha": return "application/lha";
                case "lhx": return "application/octet-stream";
                case "list": return "text/plain";
                case "lma": return "audio/nspaudio";
                case "log": return "text/plain";
                case "lsp": return "text/x-script.lisp";
                case "lst": return "text/plain";
                case "lsx": return "text/x-la-asf";
                case "ltx": return "application/x-latex";
                case "lzh": return "application/x-lzh";
                case "lzx": return "application/lzx";
                case "m": return "text/x-m";
                case "m1v": return "video/mpeg";
                case "m2a": return "audio/mpeg";
                case "m2v": return "video/mpeg";
                case "m3u": return "audio/x-mpequrl";
                case "man": return "application/x-troff-man";
                case "map": return "application/x-navimap";
                case "mar": return "text/plain";
                case "mbd": return "application/mbedlet";
                case "mc$": return "application/x-magic-cap-package-1.0";
                case "mcd": return "application/mcad";
                case "mcf": return "text/mcf";
                case "mcp": return "application/netmc";
                case "me": return "application/x-troff-me";
                case "mht": return "message/rfc822";
                case "mhtml": return "message/rfc822";
                case "mid": return "audio/midi";
                case "midi": return "audio/midi";
                case "mif": return "application/x-mif";
                case "mime": return "www/mime";
                case "mjf": return "audio/x-vnd.audioexplosion.mjuicemediafile";
                case "mjpg": return "video/x-motion-jpeg";
                case "mm": return "application/base64";
                case "mme": return "application/base64";
                case "mod": return "audio/mod";
                case "moov": return "video/quicktime";
                case "mov": return "video/quicktime";
                case "movie": return "video/x-sgi-movie";
                case "mp2": return "audio/mpeg";
                case "mp3": return "audio/mpeg3";
                case "mpa": return "audio/mpeg";
                case "mpc": return "application/x-project";
                case "mpe": return "video/mpeg";
                case "mpeg": return "video/mpeg";
                case "mpg": return "video/mpeg";
                case "mpga": return "audio/mpeg";
                case "mpp": return "application/vnd.ms-project";
                case "mpt": return "application/x-project";
                case "mpv": return "application/x-project";
                case "mpx": return "application/x-project";
                case "mrc": return "application/marc";
                case "ms": return "application/x-troff-ms";
                case "mv": return "video/x-sgi-movie";
                case "my": return "audio/make";
                case "mzz": return "application/x-vnd.audioexplosion.mzz";
                case "nap": return "image/naplps";
                case "naplps": return "image/naplps";
                case "nc": return "application/x-netcdf";
                case "ncm": return "application/vnd.nokia.configuration-message";
                case "nif": return "image/x-niff";
                case "niff": return "image/x-niff";
                case "nix": return "application/x-mix-transfer";
                case "nsc": return "application/x-conference";
                case "nvd": return "application/x-navidoc";
                case "o": return "application/octet-stream";
                case "oda": return "application/oda";
                case "omc": return "application/x-omc";
                case "omcd": return "application/x-omcdatamaker";
                case "omcr": return "application/x-omcregerator";
                case "p": return "text/x-pascal";
                case "p10": return "application/pkcs10";
                case "p12": return "application/pkcs-12";
                case "p7a": return "application/x-pkcs7-signature";
                case "p7c": return "application/pkcs7-mime";
                case "p7m": return "application/pkcs7-mime";
                case "p7r": return "application/x-pkcs7-certreqresp";
                case "p7s": return "application/pkcs7-signature";
                case "part": return "application/pro_eng";
                case "pas": return "text/pascal";
                case "pbm": return "image/x-portable-bitmap";
                case "pcl": return "application/vnd.hp-pcl";
                case "pct": return "image/x-pict";
                case "pcx": return "image/x-pcx";
                case "pdb": return "chemical/x-pdb";
                case "pdf": return "application/pdf";
                case "pfunk": return "audio/make";
                case "pgm": return "image/x-portable-graymap";
                case "pic": return "image/pict";
                case "pict": return "image/pict";
                case "pkg": return "application/x-newton-compatible-pkg";
                case "pko": return "application/vnd.ms-pki.pko";
                case "pl": return "text/x-script.perl";
                case "plx": return "application/x-pixclscript";
                case "pm": return "text/x-script.perl-module";
                case "pm4": return "application/x-pagemaker";
                case "pm5": return "application/x-pagemaker";
                case "png": return "image/png";
                case "pnm": return "image/x-portable-anymap";
                case "pot": return "application/vnd.ms-powerpoint";
                case "pov": return "model/x-pov";
                case "ppa": return "application/vnd.ms-powerpoint";
                case "ppm": return "image/x-portable-pixmap";
                case "pps": return "application/vnd.ms-powerpoint";
                case "ppt": return "application/vnd.ms-powerpoint";
                case "ppz": return "application/mspowerpoint";
                case "pre": return "application/x-freelance";
                case "prt": return "application/pro_eng";
                case "ps": return "application/postscript";
                case "psd": return "application/octet-stream";
                case "pvu": return "paleovu/x-pv";
                case "pwz": return "application/vnd.ms-powerpoint";
                case "py": return "text/x-script.phyton";
                case "pyc": return "applicaiton/x-bytecode.python";
                case "qcp": return "audio/vnd.qcelp";
                case "qd3": return "x-world/x-3dmf";
                case "qd3d": return "x-world/x-3dmf";
                case "qif": return "image/x-quicktime";
                case "qt": return "video/quicktime";
                case "qtc": return "video/x-qtc";
                case "qti": return "image/x-quicktime";
                case "qtif": return "image/x-quicktime";
                case "ra": return "audio/x-pn-realaudio";
                case "ram": return "audio/x-pn-realaudio";
                case "ras": return "image/cmu-raster";
                case "rast": return "image/cmu-raster";
                case "rexx": return "text/x-script.rexx";
                case "rf": return "image/vnd.rn-realflash";
                case "rgb": return "image/x-rgb";
                case "rm": return "audio/x-pn-realaudio";
                case "rmi": return "audio/mid";
                case "rmm": return "audio/x-pn-realaudio";
                case "rmp": return "audio/x-pn-realaudio";
                case "rng": return "application/ringing-tones";
                case "rnx": return "application/vnd.rn-realplayer";
                case "roff": return "application/x-troff";
                case "rp": return "image/vnd.rn-realpix";
                case "rpm": return "audio/x-pn-realaudio-plugin";
                case "rt": return "text/richtext";
                case "rtf": return "application/rtf";
                case "rtx": return "text/richtext";
                case "rv": return "video/vnd.rn-realvideo";
                case "s": return "text/x-asm";
                case "s3m": return "audio/s3m";
                case "saveme": return "application/octet-stream";
                case "sbk": return "application/x-tbook";
                case "scm": return "video/x-scm";
                case "sdml": return "text/plain";
                case "sdp": return "application/sdp";
                case "sdr": return "application/sounder";
                case "sea": return "application/sea";
                case "set": return "application/set";
                case "sgm": return "text/sgml";
                case "sgml": return "text/sgml";
                case "sh": return "text/x-script.sh";
                case "shar": return "application/x-bsh";
                case "shtml": return "text/html";
                case "sid": return "audio/x-psid";
                case "sit": return "application/x-sit";
                case "skd": return "application/x-koan";
                case "skm": return "application/x-koan";
                case "skp": return "application/x-koan";
                case "skt": return "application/x-koan";
                case "sl": return "application/x-seelogo";
                case "smi": return "application/smil";
                case "smil": return "application/smil";
                case "snd": return "audio/basic";
                case "sol": return "application/solids";
                case "spc": return "text/x-speech";
                case "spl": return "application/futuresplash";
                case "spr": return "application/x-sprite";
                case "sprite": return "application/x-sprite";
                case "src": return "application/x-wais-source";
                case "ssi": return "text/x-server-parsed-html";
                case "ssm": return "application/streamingmedia";
                case "sst": return "application/vnd.ms-pki.certstore";
                case "step": return "application/step";
                case "stl": return "application/vnd.ms-pki.stl";
                case "stp": return "application/step";
                case "sv4cpio": return "application/x-sv4cpio";
                case "sv4crc": return "application/x-sv4crc";
                case "svf": return "image/vnd.dwg";
                case "svr": return "x-world/x-svr";
                case "swf": return "application/x-shockwave-flash";
                case "t": return "application/x-troff";
                case "talk": return "text/x-speech";
                case "tar": return "application/x-tar";
                case "tbk": return "application/toolbook";
                case "tcl": return "text/x-script.tcl";
                case "tcsh": return "text/x-script.tcsh";
                case "tex": return "application/x-tex";
                case "texi": return "application/x-texinfo";
                case "texinfo": return "application/x-texinfo";
                case "text": return "text/plain";
                case "tgz": return "application/gnutar";
                case "tif": return "image/tiff";
                case "tiff": return "image/tiff";
                case "tr": return "application/x-troff";
                case "tsi": return "audio/tsp-audio";
                case "tsp": return "audio/tsplayer";
                case "tsv": return "text/tab-separated-values";
                case "turbot": return "image/florian";
                case "txt": return "text/plain";
                case "uil": return "text/x-uil";
                case "uni": return "text/uri-list";
                case "unis": return "text/uri-list";
                case "unv": return "application/i-deas";
                case "uri": return "text/uri-list";
                case "uris": return "text/uri-list";
                case "ustar": return "multipart/x-ustar";
                case "uu": return "text/x-uuencode";
                case "uue": return "text/x-uuencode";
                case "vcd": return "application/x-cdlink";
                case "vcs": return "text/x-vcalendar";
                case "vda": return "application/vda";
                case "vdo": return "video/vdo";
                case "vew": return "application/groupwise";
                case "viv": return "video/vivo";
                case "vivo": return "video/vivo";
                case "vmd": return "application/vocaltec-media-desc";
                case "vmf": return "application/vocaltec-media-file";
                case "voc": return "audio/voc";
                case "vos": return "video/vosaic";
                case "vox": return "audio/voxware";
                case "vqe": return "audio/x-twinvq-plugin";
                case "vqf": return "audio/x-twinvq";
                case "vql": return "audio/x-twinvq-plugin";
                case "vrml": return "x-world/x-vrml";
                case "vrt": return "x-world/x-vrt";
                case "vsd": return "application/x-visio";
                case "vst": return "application/x-visio";
                case "vsw": return "application/x-visio";
                case "w60": return "application/wordperfect6.0";
                case "w61": return "application/wordperfect6.1";
                case "w6w": return "application/msword";
                case "wav": return "audio/wav";
                case "wb1": return "application/x-qpro";
                case "wbmp": return "image/vnd.wap.wbmp";
                case "web": return "application/vnd.xara";
                case "wiz": return "application/msword";
                case "wk1": return "application/x-123";
                case "wmf": return "windows/metafile";
                case "wml": return "text/vnd.wap.wml";
                case "wmlc": return "application/vnd.wap.wmlc";
                case "wmls": return "text/vnd.wap.wmlscript";
                case "wmlsc": return "application/vnd.wap.wmlscriptc";
                case "word": return "application/msword";
                case "wp": return "application/wordperfect";
                case "wp5": return "application/wordperfect";
                case "wp6": return "application/wordperfect";
                case "wpd": return "application/wordperfect";
                case "wq1": return "application/x-lotus";
                case "wri": return "application/mswrite";
                case "wrl": return "x-world/x-vrml";                                                                                                        
                case "wrz": return "x-world/x-vrml";
                case "wsc": return "text/scriplet";
                case "wsrc": return "application/x-wais-source";
                case "wtk": return "application/x-wintalk";
                case "xbm": return "image/xbm";
                case "xdr": return "video/x-amt-demorun";
                case "xgz": return "xgl/drawing";
                case "xif": return "image/vnd.xiff";
                case "xl": return "application/excel";
                case "xla": return "application/excel";
                case "xlb": return "application/excel";
                case "xlc": return "application/excel";
                case "xld": return "application/excel";
                case "xlk": return "application/excel";
                case "xll": return "application/excel";
                case "xlm": return "application/excel";
                case "xls": return "application/excel";
                case "xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case "xlt": return "application/excel";
                case "xlv": return "application/excel";
                case "xlw": return "application/excel";
                case "xm": return "audio/xm";
                case "xml": return "text/xml";
                case "xmz": return "xgl/movie";
                case "xpix": return "application/x-vnd.ls-xpix";
                case "xpm": return "image/xpm";
                case "x-png": return "image/png";
                case "xsr": return "video/x-amt-showrun";
                case "xwd": return "image/x-xwd";
                case "xyz": return "chemical/x-pdb";
                case "z": return "application/x-compressed";
                case "zip": return "application/zip";
                case "zoo": return "application/octet-stream";
                case "zsh": return "text/x-script.zsh";
                default: return "application/octet-stream";
            }
        }
        //------------------------------------------------------------------------------------        
    }
}
