using RentalWorksAPI.api.v2.Data;
using RentalWorksAPI.api.v2.Models;
using RentalWorksAPI.Filters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentalWorksAPI.api.v2.Controllers
{
    [AppConfigAuthorize]
    public class DealV2Controller : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("v2/deal")]
        public HttpResponseMessage GetDeals([FromUri]string dealid="", [FromUri]string dealno="")
        {
            List<Deal> result = new List<Deal>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = DealV2Data.GetDeal(dealid, dealno);

            if ((result.Count == 0) && (string.IsNullOrEmpty(dealid)) && (string.IsNullOrEmpty(dealno)))
                ThrowError("404", "No deals were found.");
            if ((result.Count == 0) && ((!string.IsNullOrEmpty(dealid)) || (!string.IsNullOrEmpty(dealno))))
                ThrowError("404", "The requested deal was not found.");

            return Request.CreateResponse(HttpStatusCode.OK, new { deals = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("v2/deal")]
        public HttpResponseMessage ProcessDeal([FromBody]Deal deal)
        {
            List<Deal> result = new List<Deal>();
            dynamic saveresponse;

            if (!ModelState.IsValid)
                ThrowError("400", "");

            saveresponse = DealV2Data.ProcessDeal(deal);
            
            if (saveresponse.errno != "0")
                ThrowError(saveresponse.errno, saveresponse.errmsg);

            result = DealV2Data.GetDeal(saveresponse.dealid, "");

            return Request.CreateResponse(HttpStatusCode.OK, new { deal = result[0] } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPut]
        [Route("v2/deal")]
        public HttpResponseMessage ProcessDealByNo([FromBody]Deal deal, [FromUri]string dealid="", [FromUri]string dealno="")
        {
            List<Deal> result    = new List<Deal>();
            List<Deal> finddeal  = new List<Deal>();
            dynamic saveresponse = new ExpandoObject();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if ((string.IsNullOrEmpty(dealid)) && (string.IsNullOrEmpty(dealno)))
                ThrowError("400", "Either dealid or dealno must be passed.");

            finddeal = DealV2Data.GetDeal(dealid, dealno);
            if ((finddeal.Count == 0) && ((!string.IsNullOrEmpty(dealid)) || (!string.IsNullOrEmpty(dealno))))
                ThrowError("404", "The requested deal was not found.");

            try {
                deal.dealid  = finddeal[0].dealid;
                deal.dealno  = finddeal[0].dealno;
                saveresponse = DealV2Data.ProcessDeal(deal);
            }
            catch (Exception ex) {
                ThrowError("500", ex.Message);
            }
            
            if (saveresponse.errno != "0")
                ThrowError("404", saveresponse.errmsg);

            result = DealV2Data.GetDeal(saveresponse.dealid, "");

            return Request.CreateResponse(HttpStatusCode.OK, new { deal = result[0] } );
        }
        //----------------------------------------------------------------------------------------------------
        private void ThrowError(string errno, string errmsg)
        {
            switch (errno)
            {
                case "400": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,          ModelState));
                case "404": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,            errmsg));
                case "409": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict,            errmsg));
                default:    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errmsg));
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
}
