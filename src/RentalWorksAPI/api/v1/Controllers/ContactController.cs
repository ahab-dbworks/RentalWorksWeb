using RentalWorksAPI.api.v1.Data;
using RentalWorksAPI.api.v1.Models;
using RentalWorksAPI.Filters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace RentalWorksAPI.api.v1
{
    [AppConfigAuthorize]
    [RoutePrefix("{apiVersion1:apiVersion1Constraint(v1)}")]
    public class ContactController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("contact")]
        public HttpResponseMessage GetContacts()
        {
            List<Contact> result = new List<Contact>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = ContactData.GetContact("");

            return Request.CreateResponse(HttpStatusCode.OK, new { contacts = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("contact/{contactid}")]
        public HttpResponseMessage GetContact([FromUri]string contactid)
        {
            List<Contact> result = new List<Contact>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = ContactData.GetContact(contactid);

            if (result.Count == 0)
                ThrowError("404", "The requested contact was not found.");

            return Request.CreateResponse(HttpStatusCode.OK, new { contact = result[0] } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("contact/{webusersid}/orders")]
        public HttpResponseMessage GetContactOrders([FromUri]string webusersid, [FromBody]OrderParameters request)
        {
            WebUsers webuserinfo;
            ContactOrders result = new ContactOrders();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            webuserinfo = ContactData.GetWebUser(webusersid);

            if (webuserinfo.webusersid == "")
                ThrowError("404", "The requested contact was not found.");

            result.contactid = webuserinfo.contactid;
            result.orders    = ContactData.GetContactOrders(webuserinfo.webusersid, request);

            return Request.CreateResponse(HttpStatusCode.OK, new { contactorders = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("contact/save")]
        public HttpResponseMessage ProcessContact([FromBody]Contact contact)
        {
            List<Contact> result = new List<Contact>();
            dynamic postdata     = new ExpandoObject();
            var watch            = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if ((!string.IsNullOrEmpty(contact.contactid)) && (ContactData.GetContact(contact.contactid).Count == 0)) {
                watch.Stop();
                AppData.LogWebApiAudit(((contact.contactid == null) ? "" : contact.contactid), "v1/contact/save", new JavaScriptSerializer().Serialize(contact), "", "404: The requested contact was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested contact was not found.");
            }

            try {
                postdata = ContactData.ProcessContact(contact);
            }
            catch (Exception ex) {
                watch.Stop();
                AppData.LogWebApiAudit(((contact.contactid == null) ? "" : contact.contactid), "v1/contact/save", new JavaScriptSerializer().Serialize(contact), "", "500: " + ex.Message, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("500", ex.Message);
            }

            if (postdata.errno != "0") {
                watch.Stop();
                AppData.LogWebApiAudit(((contact.contactid == null) ? "" : contact.contactid), "v1/contact/save", new JavaScriptSerializer().Serialize(contact), "", "404: " + postdata.errmsg, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", postdata.errmsg);
            }

            result = ContactData.GetContact(postdata.contactid);
            watch.Stop();
            AppData.LogWebApiAudit(((contact.contactid == null) ? result[0].contactid : contact.contactid), "v1/contact/save", new JavaScriptSerializer().Serialize(contact), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { contact = result[0] } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("contact/{webusersid}/deals")]
        public HttpResponseMessage GetContactDeals([FromUri]string webusersid)
        {
            ContactDeals result = new ContactDeals();
            WebUsers webuserinfo;

            if (!ModelState.IsValid)
                ThrowError("400", "");

            webuserinfo = ContactData.GetWebUser(webusersid);

            if (webuserinfo.webusersid == "")
                ThrowError("404", "The requested contact was not found.");

            result.contactid = webuserinfo.contactid;
            result.deals     = ContactData.GetContactDeals(webuserinfo.webusersid);

            return Request.CreateResponse(HttpStatusCode.OK, new { contactdeals = result } );
        }
        //----------------------------------------------------------------------------------------------------
        private void ThrowError(string errno, string errmsg)
        {
            switch (errno)
            {
                case "400": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,          ModelState));
                case "404": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,            errmsg));
                case "409": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict,            errmsg));
                case "500": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errmsg));
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
}