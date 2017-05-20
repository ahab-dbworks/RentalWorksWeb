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
    public class DealController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("deal")]
        public HttpResponseMessage GetDeals()
        {
            List<Deal> result = new List<Deal>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = DealData.GetDeal("", "");

            return Request.CreateResponse(HttpStatusCode.OK, new { deals = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("deal/{dealid}")]
        public HttpResponseMessage GetDeal([FromUri]string dealid)
        {
            List<Deal> result = new List<Deal>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = DealData.GetDeal(dealid, "");

            if (result.Count == 0)
                ThrowError("404", "The requested deal was not found.");

            return Request.CreateResponse(HttpStatusCode.OK, new { deal = result[0] } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("deal/dealno={dealno}")]
        public HttpResponseMessage GetDealByNo([FromUri]string dealno)
        {
            List<Deal> result = new List<Deal>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = DealData.GetDeal("", dealno);

            if (result.Count == 0)
                ThrowError("404", "The requested deal was not found.");

            return Request.CreateResponse(HttpStatusCode.OK, new { deal = result[0] } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("deal/save")]
        public HttpResponseMessage ProcessDeal([FromBody]Deal deal)
        {
            List<Deal> result = new List<Deal>();
            dynamic postdata  = new ExpandoObject();
            var watch         = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (string.IsNullOrEmpty(deal.dealid) && string.IsNullOrEmpty(deal.dealno)) {
                watch.Stop();
                AppData.LogWebApiAudit(((deal.dealid == null) ? "" : deal.dealid), "v1/deal/save", new JavaScriptSerializer().Serialize(deal), "", "409: Deal no is required.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("409", "Deal no is required.");
            }

            if ((!string.IsNullOrEmpty(deal.dealid)) && (DealData.GetDeal(deal.dealid, "").Count == 0)) {
                watch.Stop();
                AppData.LogWebApiAudit(((deal.dealid == null) ? "" : deal.dealid), "v1/deal/save", new JavaScriptSerializer().Serialize(deal), "", "404: The requested deal was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested deal was not found.");
            }

            if ((!string.IsNullOrEmpty(deal.customerid)) || (!string.IsNullOrEmpty(deal.customerno))) {
                List<Customer> foundcustomer = CustomerData.GetCustomer(deal.customerid, deal.customerno);
                if (foundcustomer.Count == 0) {
                    watch.Stop();
                    AppData.LogWebApiAudit(((deal.dealid == null) ? "" : deal.dealid), "v1/deal/save", new JavaScriptSerializer().Serialize(deal), "", "404: The customer being assigned was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                    ThrowError("404", "The customer being assigned was not found.");
                } else {
                    deal.customerid = foundcustomer[0].customerid;
                }
            }

            try {
                postdata = DealData.ProcessDeal(deal);
            }
            catch (Exception ex) {
                watch.Stop();
                AppData.LogWebApiAudit(((deal.dealid == null) ? "" : deal.dealid), "v1/deal/save", new JavaScriptSerializer().Serialize(deal), "", "500: " + ex.Message, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("500", ex.Message);
            }
            
            if (postdata.errno != "0") {
                watch.Stop();
                AppData.LogWebApiAudit(((deal.dealid == null) ? "" : deal.dealid), "v1/deal/save", new JavaScriptSerializer().Serialize(deal), "", "500: " + postdata.errmsg, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", postdata.errmsg);
            }

            result = DealData.GetDeal(postdata.dealid, "");
            watch.Stop();
            AppData.LogWebApiAudit(((deal.dealid == null) ? result[0].dealid : deal.dealid), "v1/deal/save", new JavaScriptSerializer().Serialize(deal), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { deal = result[0] } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPut]
        [Route("deal/dealno={dealno}")]
        public HttpResponseMessage ProcessDealByNo([FromBody]Deal deal, [FromUri]string dealno)
        {
            List<Deal> result    = new List<Deal>();
            List<Deal> finddeal  = new List<Deal>();
            dynamic saveresponse = new ExpandoObject();
            var watch            = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            finddeal = DealData.GetDeal("", dealno);
            if (finddeal.Count == 0) {
                watch.Stop();
                AppData.LogWebApiAudit("", "v1/deal/dealno=" + dealno, new JavaScriptSerializer().Serialize(deal), "", "404: The requested deal was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested deal was not found.");
            }

            if ((!string.IsNullOrEmpty(deal.customerid)) || (!string.IsNullOrEmpty(deal.customerno))) {
                List<Customer> foundcustomer = CustomerData.GetCustomer(deal.customerid, deal.customerno);
                if (foundcustomer.Count == 0) {
                    watch.Stop();
                    AppData.LogWebApiAudit("", "v1/deal/dealno=" + dealno, new JavaScriptSerializer().Serialize(deal), "", "404: The customer being assigned was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                    ThrowError("404", "The customer being assigned was not found.");
                } else {
                    deal.customerid = foundcustomer[0].customerid;
                }
            }

            try {
                deal.dealid  = finddeal[0].dealid;
                deal.dealno  = dealno;
                saveresponse = DealData.ProcessDeal(deal);
            }
            catch (Exception ex) {
                watch.Stop();
                AppData.LogWebApiAudit(deal.dealid, "v1/deal/dealno=" + dealno, new JavaScriptSerializer().Serialize(deal), "", "500: " + ex.Message, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("500", ex.Message);
            }
            
            if (saveresponse.errno != "0") {
                watch.Stop();
                AppData.LogWebApiAudit(deal.dealid, "v1/deal/dealno=" + dealno, new JavaScriptSerializer().Serialize(deal), "", "404: " + saveresponse.errmsg, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", saveresponse.errmsg);
            }

            result = DealData.GetDeal(saveresponse.dealid, "");
            watch.Stop();
            AppData.LogWebApiAudit(deal.dealid, "v1/deal/dealno=" + dealno, new JavaScriptSerializer().Serialize(deal), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

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
                case "500": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errmsg));
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
}