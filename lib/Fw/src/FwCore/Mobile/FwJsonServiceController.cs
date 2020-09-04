using FwCore.Controllers;
using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FwCore.Mobile
{
    public abstract class FwJsonServiceController: FwController
    {
        private List<FwJsonRequestAction> actions = null;
        System.Text.Json.JsonSerializerOptions jsonSerializerOptions;
        public class AuthTokenInvalidException : Exception
        {

        }
        //---------------------------------------------------------------------------------------------
        public FwJsonServiceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig)
        {
            if (actions == null)
            {
                actions = GetRequestActions();
            }
            jsonSerializerOptions = new System.Text.Json.JsonSerializerOptions
            {
                Converters = { new FwDynamicJsonConverter() }
            };
        }
        //---------------------------------------------------------------------------------------------
        protected static string GetRequestPath(ControllerContext context)
        {
            string result;

            result = context.HttpContext.Request.Path.Value.ToLower();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        protected static string GetApplicationPath(ControllerContext context)
        {
            StringBuilder sb;
            string result;

            sb = new StringBuilder();
            //sb.Append(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority).ToLower());
            //sb.Append(HttpContext.Current.Request.ApplicationPath.ToLower().Equals("/") ? "" : HttpContext.Current.Request.ApplicationPath.ToLower());
            sb.Append(context.HttpContext.Request.Path.Value);
            result = sb.ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        protected abstract List<FwJsonRequestAction> GetRequestActions();
        protected abstract dynamic GetSession(dynamic request, dynamic response);
        //---------------------------------------------------------------------------------------------
        protected async Task<ActionResult<ExpandoObject>> ProcessRequestAsync(ControllerContext context, string requestPath)
        {
            await Task.CompletedTask;

            string actionRole, userRole;
            dynamic request, response, session;
            bool foundMatch, hasPermission;
            List<string> userRoles;

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                string jsonRequest = await reader.ReadToEndAsync();
                request = System.Text.Json.JsonSerializer.Deserialize<dynamic>(jsonRequest, jsonSerializerOptions);
            }
            foundMatch = false;
            hasPermission = false;
            response = new ExpandoObject();
            try
            {
                if (request != null)
                {
                    response.request = request;
                    if (context.HttpContext.Request.Method == "OPTIONS")
                    {
                        return StatusCode(204);
                    }
                    if (request == null)
                    {
                        throw new Exception("Request format is invalid. [FwJsonService.cs:ProcessRequest]");
                    }
                    for (int i = 0; i < actions.Count; i++)
                    {
                        foundMatch = actions[i].IsMatch(requestPath);
                        if (foundMatch)
                        {
                            session = GetSession(request, response);
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
                                        await actions[i].OnMatch(request, response, session);
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
                        return NotFound("Unable to find a service to handle the request for: " + requestPath);
                    }
                    else if (!hasPermission)
                    {
                        throw new Exception("You do not have permission to perform the requested action. [FwJsonService.cs:ProcessRequest]");
                    }
                    if (FwValidate.IsPropertyDefined(response, "filedownload"))
                    {
                        return File(response.filedownload.file, response.filedownload.contenttype);
                    }
                    else
                    {
                        return Ok(response);
                    }
                }
                else
                {
                    throw new Exception("Request is null. [FwJsonService.cs:ProcessRequest]");
                }
            }
            catch (AuthTokenInvalidException)
            {
                response.authTokenExpired = true;
                return Ok(response);
            }
            catch (FwNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FwBadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (TargetInvocationException ex)
            {
                response = new ExpandoObject();
                if (ex.InnerException != null)
                {
                    response.exception = new ExpandoObject();
                    response.exception.message = ex.InnerException.Message;
                    response.exception.stack = ex.InnerException.StackTrace;
                    return StatusCode(500, ex.Message + ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                response = new ExpandoObject();
                response.exception = new ExpandoObject();
                response.exception.message = ex.Message;
                response.exception.stack = ex.StackTrace;
                return StatusCode(500, ex.Message + ex.StackTrace);
            }
            return NotFound();
        }
        //---------------------------------------------------------------------------------------------
    }
}
