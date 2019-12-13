using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using Fw.Json.Utilities;
using System.Web.Security;
using System.IO;
using System.Text;
using System.Reflection;

namespace Fw.Json.Services
{
    public abstract class FwJsonService : IHttpHandler
    {
        //---------------------------------------------------------------------------------------------
        public bool IsReusable { get { return true; } }
        private List<FwJsonRequestAction> actions = null;
        public class AuthTokenInvalidException : Exception
        {
            
        }
        //---------------------------------------------------------------------------------------------
        public static string GetRequestPath()
        {
            string result;

            result = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static string GetApplicationPath()
        {
            StringBuilder sb;
            string result;
            
            sb = new StringBuilder();
            sb.Append(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority).ToLower()); 
            sb.Append(HttpContext.Current.Request.ApplicationPath.ToLower().Equals("/") ? "" : HttpContext.Current.Request.ApplicationPath.ToLower());
            result = sb.ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public FwJsonService()
        {
            if (actions == null)
            {
                actions = GetRequestActions();
            }
            //if (FwVersion.Current == null)
            //{
            //    using (FileStream fsVersion = new FileStream(HttpContext.Current.Server.MapPath("version.txt"), FileMode.Open, FileAccess.Read))
            //    {
            //        byte[] buffer;
            //        string version;
                
            //        buffer = new byte[fsVersion.Length];
            //        fsVersion.Read(buffer, 0, buffer.Length);
            //        version = System.Text.Encoding.ASCII.GetString(buffer);
            //        FwVersion.SetVersion(version);
            //    }
            //}
        }
        //---------------------------------------------------------------------------------------------
        public void ProcessRequest(HttpContext context)
        {
            string jsonRequest, jsonResponse, requestPath, applicationPath, actionRole, userRole;
            JsonFx.Json.JsonReader reader;
            JsonFx.Json.JsonWriter writer;
            dynamic request, response, session;
            byte[] buffer;
            bool foundMatch, hasPermission;
            List<string> userRoles;
            
            foundMatch = false;
            hasPermission = false;
            response = new ExpandoObject();
            writer = new JsonFx.Json.JsonWriter();
            try
            {
                jsonRequest = string.Empty;
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (context.Request.QueryString["request"] != null)
                {
                    jsonRequest = context.Request.QueryString["request"];
                }
                else
                {
                    buffer = new byte[context.Request.InputStream.Length];
                    context.Request.InputStream.Position = 0;
                    context.Request.InputStream.Read(buffer, 0, Convert.ToInt32(context.Request.InputStream.Length));
                    jsonRequest = context.Request.ContentEncoding.GetString(buffer);
                }
                if (jsonRequest != null)
                {
                    reader = new JsonFx.Json.JsonReader(); 
                    request = reader.Read(jsonRequest);
                    response.request = request;
                    // handle CORS preflight requests, the project's web.config should be setup to return the needed CORS headers such as:
                    //<add name="Access-Control-Allow-Origin" value="*"/>
                    //<add name="Access-Control-Allow-Methods" value="GET, PUT, POST, DELETE, OPTIONS"/>
                    //<add name="Access-Control-Allow-Headers" value="Origin, X-Requested-With, Content-Type, Accept, Authorization"/>
                    //<add name="Access-Control-Max-Age" value="86400"/>
                    if (context.Request.HttpMethod == "OPTIONS")
                    {
                        context.Response.StatusCode = 204;
                        return;
                    }
                    if (request == null)
                    {
                        throw new Exception("Request format is invalid. [FwJsonService.cs:ProcessRequest]");
                    }
                    if (request.clientVersion == FwVersion.Current.FullVersion)
                    {
                        requestPath     = FwJsonService.GetRequestPath();
                        applicationPath = FwJsonService.GetApplicationPath();
                        for (int i = 0; i < actions.Count; i++)
                        {
                            foundMatch = actions[i].IsMatch(requestPath, applicationPath);
                            if (foundMatch)
                            {
                                session   = GetSession(request, response);
                                userRoles = session.security.userRoles;
                                for (int j = 0; j < actions[i].Roles.Length; j++)
                                {
                                    actionRole = actions[i].Roles[j];
                                    for (int k = 0; k < userRoles.Count; k++)
                                    {
                                        userRole = userRoles[k];
                                        hasPermission = (actionRole == userRole);
                                        if (hasPermission)
                                        {
                                            hasPermission = true;
                                            actions[i].OnMatch(request, response, session);
                                            break;
                                        }
                                    }
                                    if (hasPermission)
                                    {
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        if (!foundMatch)
                        {
                            context.Response.StatusCode = 404;
                            throw new Exception("Unable to find a service to handle the request for: " + requestPath + ". [FwJsonService.cs:ProcessRequest]");
                        }
                        else if (!hasPermission)
                        {
                            //context.Response.StatusCode = 500;
                            throw new Exception("You do not have permission to perform the requested action. [FwJsonService.cs:ProcessRequest]");
                        }
                    }
                    if (FwValidate.IsPropertyDefined(response, "filedownload"))
                    {
                        context.Response.ContentType = response.filedownload.contenttype;
                        context.Response.Write(response.filedownload.file);
                    }
                    else
                    {
                        response.serverVersion = FwVersion.Current.FullVersion;
                        jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(response);
                        //jsonResponse = writer.Write(response);
                        context.Response.ContentType = "application/json";
                        context.Response.Write(jsonResponse);
                    }
                }
                else
                {
                    throw new Exception("Request is null. [FwJsonService.cs:ProcessRequest]");
                }
            }
            catch (AuthTokenInvalidException)
            {
                response.authTokenExpired    = true;
                response.serverVersion       = FwVersion.Current.FullVersion;
                jsonResponse                 = writer.Write(response);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonResponse);
            }
            catch (TargetInvocationException ex)
            {
                response = new ExpandoObject();
                if (ex.InnerException != null)
                {
                    response.exception = new ExpandoObject();
                    response.exception.message   = ex.InnerException.Message;
                    response.exception.stack     = ex.InnerException.StackTrace;
                    response.serverVersion       = FwVersion.Current.FullVersion;
                    jsonResponse                 = writer.Write(response);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                response                     = new ExpandoObject();
                response.exception           = new ExpandoObject();
                response.exception.message   = ex.Message;
                response.exception.stack     = ex.StackTrace;
                response.serverVersion       = FwVersion.Current.FullVersion;
                jsonResponse                 = writer.Write(response);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonResponse);
            }
        }
        //---------------------------------------------------------------------------------------------
        protected abstract List<FwJsonRequestAction> GetRequestActions();
        protected abstract dynamic GetSession(dynamic request, dynamic response);
        //---------------------------------------------------------------------------------------------
    }
}
