using RentalWorksAPI.api.v2.Data;
using RentalWorksAPI.api.v2.Models;
using RentalWorksAPI.Filters;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentalWorksAPI.api.v2
{
    [AppConfigAuthorize]
    [RoutePrefix("{apiVersion2:apiVersion2Constraint(v2)}/account")]
    public class AccountController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("login")]
        public HttpResponseMessage Login([FromUri]string password, [FromUri]string username="", [FromUri]string email="")
        {
            dynamic req = new ExpandoObject();
            WebUsers response = new WebUsers();

            if (!ModelState.IsValid)
                ThrowError("400", "");
            if (username == "" && email == "")
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Username or Email is required."));

            if (username != "")
            {

            }
            else if (email != "")
            {
                req = AccountData.WebGetUsers(email, password);
            }

            if (req.errno != "0")
            {
                string message = req.errmsg;
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }
            else
            {
                response = AccountData.WebUsersView(req.webusersid);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { user = response });
        }
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("user")]
        public HttpResponseMessage Users([FromUri]List<string> webusersid)
        {
            List<WebUsers> response = new List<WebUsers>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            response = AccountData.GetUsers(webusersid);

            return Request.CreateResponse(HttpStatusCode.OK, new { webusers = response });
        }
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("rwuser")]
        public HttpResponseMessage GetRwUsers([FromUri]string locationid, [FromUri]string departmentid="", [FromUri]string groupsid="")
        {
            List<WebUsers> response = new List<WebUsers>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            response = AccountData.GetRwUsers(locationid, departmentid, groupsid);

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