using RentalWorksAPI.api.v2.Data;
using RentalWorksAPI.api.v2.Models;
using RentalWorksAPI.api.v2.Models.AccountModels.Login;
using RentalWorksAPI.api.v2.Models.AccountModels.User;
using RentalWorksAPI.api.v2.Models.AccountModels.RwUser;
using RentalWorksAPI.Filters;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.DirectoryServices.AccountManagement;

namespace RentalWorksAPI.api.v2
{
    [AppConfigAuthorize]
    [RoutePrefix("{apiVersion2:apiVersion2Constraint(v2)}/account")]
    public class AccountController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login([FromBody]Login request)
        {
            dynamic req = new ExpandoObject();
            WebUsers response = new WebUsers();

            if (!ModelState.IsValid)
                ThrowError("400", "");
            if (request.username == "" && request.email == "")
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Username or Email is required."));

            if (request.domain != "")
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, request.domain))
                {
                    bool isValid = pc.ValidateCredentials(request.username, request.password);

                    if (isValid)
                    {
                        response = AccountData.WebUsersView("", request.email);
                    }
                    else
                    {
                        string message = "Invalid username and/or password.";
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
                    }
                }
            }
            else
            {
                req = AccountData.WebGetUsers(request.email, request.password);
                if (req.errno != "0")
                {
                    string message = req.errmsg;
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
                }
                else
                {
                    response = AccountData.WebUsersView(req.webusersid, "");
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { user = response });
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("user")]
        public HttpResponseMessage Users([FromBody]User request)
        {
            List<WebUsers> response = new List<WebUsers>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            response = AccountData.GetUsers(request.webusersid);

            return Request.CreateResponse(HttpStatusCode.OK, new { webusers = response });
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("rwuser")]
        public HttpResponseMessage GetRwUsers([FromBody]RwUser request)
        {
            List<WebUsers> response = new List<WebUsers>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            response = AccountData.GetRwUsers(request.locationid, request.departmentid, request.groupsid);

            return Request.CreateResponse(HttpStatusCode.OK, new { webusers = response });
        }
        //----------------------------------------------------------------------------------------------------
        private void ThrowError(string errno, string errmsg)
        {
            switch (errno)
            {
                case "400": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
                case "404": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,   errmsg));
                case "409": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict,   errmsg));
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
}