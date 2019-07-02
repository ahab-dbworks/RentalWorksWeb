using RentalWorksAPI.api.v1.Data;
using RentalWorksAPI.api.v1.Models;
using RentalWorksAPI.Filters;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentalWorksAPI.api.v1
{
    [AppConfigAuthorize]
    [RoutePrefix("{apiVersion1:apiVersion1Constraint(v1)}")] 
    public class AccountController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("account/login")]
        public HttpResponseMessage Login([FromBody]Login request)
        {
            dynamic req = new ExpandoObject();
            WebUsers response = new WebUsers();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            req = AccountData.WebGetUsers(request.email, request.password);
            if (req.errno != "0")
            {
                //string message = req.errmsg;
                string message = "Invalid username and/or password.";
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }
            else
            {
                response = AccountData.WebUsersView(req.webusersid);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { user = response } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("account/changepassword")]
        public HttpResponseMessage ChangePassword([FromBody]ChangePassword request)
        {
            dynamic req = new ExpandoObject();
            Error response = new Error();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            response = AccountData.WebUsersSetPassword(request.webusersid, request.password);

            return Request.CreateResponse(HttpStatusCode.OK, new { response = response } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("account/resetpassword")]
        public HttpResponseMessage ResetPassword([FromBody]ResetPassword request)
        {
            dynamic req = new ExpandoObject();
            Error response = new Error();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            response = AccountData.WebUsersResetPassword(request.email);
            
            return Request.CreateResponse(HttpStatusCode.OK, new { response = response } );
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