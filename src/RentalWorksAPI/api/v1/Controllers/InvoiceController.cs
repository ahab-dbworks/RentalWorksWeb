using RentalWorksAPI.api.v1.Data;
using RentalWorksAPI.api.v1.Models;
using RentalWorksAPI.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentalWorksAPI.api.v1
{
    [AppConfigAuthorize]
    [RoutePrefix("{apiVersion1:apiVersion1Constraint(v1)}")] 
    public class InvoiceController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("invoice/{asofdate:datetime}")]
        public HttpResponseMessage GetInvoicesAsOf([FromUri]string asofdate)
        {
            List<Invoice> result = new List<Invoice>();
            DateTime dDate;

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (DateTime.TryParse(asofdate, out dDate))
            {
                String.Format("{0:yyyy/MM/d}", dDate);
            }
            else
            {
                string message = string.Format("The date input ({0}) is invalid", asofdate);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }

            result = InvoiceData.GetInvoicesFrom(dDate.ToString());

            return Request.CreateResponse(HttpStatusCode.OK, new { invoices = result } );
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