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
    public class QuoteController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Get a list of all quotes.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("quote")]
        public HttpResponseMessage GetQuotes([FromBody]OrderParameters request)
        {
            List<Order> result = new List<Order>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = OrderData.GetOrder("", "Q", request.statuses, request.rental, request.sales, request.datestamp);

            return Request.CreateResponse(HttpStatusCode.OK, new { quotes = result } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Get a specific quote.
        /// </summary>
        /// <param name="quoteid">Quote Id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("quote/{quoteid:alpha}")]
        public HttpResponseMessage GetQuote([FromUri]string quoteid)
        {
            List<Order> orders = new List<Order>();
            Order result       = new Order();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            orders = OrderData.GetOrder(quoteid, "Q", null, "", "", "");

            if (orders.Count == 0)
                ThrowError("404", "The requested quote was not found.");

            result       = orders[0];
            result.items = OrderData.GetOrderItems(result.orderid);

            return Request.CreateResponse(HttpStatusCode.OK, new { quote = result } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Create or update a quote.
        /// </summary>
        /// <param name="quote"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("quote/save")]
        public HttpResponseMessage ProcessQuote([FromBody]Order quote)
        {
            Order result     = new Order();
            dynamic postdata = new ExpandoObject();
            var watch        = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if ((!string.IsNullOrEmpty(quote.orderid)) && (OrderData.GetOrder(quote.orderid, "Q", null, "", "", "").Count == 0)) {
                watch.Stop();
                AppData.LogWebApiAudit(((quote.orderid == null) ? "" : quote.orderid), "v1/quote/save", new JavaScriptSerializer().Serialize(quote), "", "404: The requested quote was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested quote was not found.");
            }

            if ((quote.deal != null) && ((!string.IsNullOrEmpty(quote.deal.dealid)) || (!string.IsNullOrEmpty(quote.deal.dealno)))) {
                List<Deal> founddeal = DealData.GetDeal(quote.deal.dealid, quote.deal.dealno);
                if (founddeal.Count == 0) {
                    watch.Stop();
                    AppData.LogWebApiAudit(((quote.orderid == null) ? "" : quote.orderid), "v1/quote/save", new JavaScriptSerializer().Serialize(quote), "", "404: The deal being assigned was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                    ThrowError("404", "The deal being assigned was not found.");
                } else {
                    quote.deal.dealid = founddeal[0].dealid;
                }
            }

            try {
                postdata = OrderData.ProcessQuote(quote);
            }
            catch (Exception ex) {
                watch.Stop();
                AppData.LogWebApiAudit(((quote.orderid == null) ? "" : quote.orderid), "v1/quote/save", new JavaScriptSerializer().Serialize(quote), "", "500: " + ex.Message, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("500", ex.Message);
            }

            if (postdata.errno != "0") {
                watch.Stop();
                AppData.LogWebApiAudit(((quote.orderid == null) ? "" : quote.orderid), "v1/quote/save", new JavaScriptSerializer().Serialize(quote), "", "409: " + postdata.errmsg, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("409", postdata.errmsg);
            }

            result = OrderData.GetOrder(postdata.orderid, "Q", null, "", "", "")[0];
            watch.Stop();
            AppData.LogWebApiAudit(((quote.orderid == null) ? result.orderid : quote.orderid), "v1/quote/save", new JavaScriptSerializer().Serialize(quote), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { quote = result } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Submit a quote.
        /// </summary>
        /// <param name="quote"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("quote/submit")]
        public HttpResponseMessage SubmitQuote([FromBody]OrderSubmit quote)
        {
            Order result     = new Order();
            dynamic response = new ExpandoObject();
            Error returna    = new Error();
            var watch        = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (OrderData.GetOrder(quote.orderid, "Q", null, "", "", "").Count == 0) {
                watch.Stop();
                AppData.LogWebApiAudit(quote.orderid, "v1/quote/submit", new JavaScriptSerializer().Serialize(quote), "", "404: The requested quote was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested quote was not found.");
            }

            response = OrderData.WebSubmitQuote(quote.orderid, quote.webusersid);

            returna.errno  = response.errno;
            returna.errmsg = response.errmsg;
            watch.Stop();
            AppData.LogWebApiAudit(quote.orderid, "v1/quote/submit", new JavaScriptSerializer().Serialize(quote), new JavaScriptSerializer().Serialize(returna), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { response = returna } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates a new version of a quote.
        /// </summary>
        /// <param name="quote"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("quote/version")]
        public HttpResponseMessage NewQuoteVersion([FromBody]OrderSubmit quote)
        {
            Order result                   = new Order();
            NewQuoteVersionResult response = new NewQuoteVersionResult();
            var watch                      = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (OrderData.GetOrder(quote.orderid, "Q", null, "", "", "").Count == 0) {
                watch.Stop();
                AppData.LogWebApiAudit(quote.orderid, "v1/quote/version", new JavaScriptSerializer().Serialize(quote), "", "404: The requested order was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested order was not found.");
            }

            response = OrderData.ProcessQuoteNewVersion(quote.orderid, quote.webusersid);
            watch.Stop();
            AppData.LogWebApiAudit(quote.orderid, "v1/quote/version", new JavaScriptSerializer().Serialize(quote), new JavaScriptSerializer().Serialize(response), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { response = response } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Add or update a single or multiple line items on a quote.
        /// </summary>
        /// <param name="orderitem"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("quote/lineitems")]
        public HttpResponseMessage ProcessQuoteLineItem([FromBody]OrderItems orderitem)
        {
            dynamic orderitemresponse;
            OrderItems result = new OrderItems();
            var watch         = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (OrderData.GetOrder(orderitem.orderid, "Q", null, "", "", "").Count == 0) {
                watch.Stop();
                AppData.LogWebApiAudit(orderitem.orderid, "v1/quote/lineitems", new JavaScriptSerializer().Serialize(orderitem), "", "404: The requested order was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested order was not found.");
            }

            for (int i = 0; i < orderitem.items.Count; i++)
            {
                orderitemresponse = OrderData.ProcessOrderItem(orderitem.items[i], orderitem.orderid);
                if (orderitemresponse.errno != "0")
                {
                    watch.Stop();
                    AppData.LogWebApiAudit(orderitem.orderid, "v1/quote/lineitems", new JavaScriptSerializer().Serialize(orderitem), "", "500: " + orderitemresponse.errmsg, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                    ThrowError("500", orderitemresponse.errmsg);
                }
            }

            OrderData.UpdateOrderTimeStamp(orderitem.orderid);

            result.orderid = orderitem.orderid;
            result.items   = OrderData.GetOrderItems(orderitem.orderid);
            watch.Stop();
            AppData.LogWebApiAudit(orderitem.orderid, "v1/quote/lineitems", new JavaScriptSerializer().Serialize(orderitem), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { order = result } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Removes a line item from a quote.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("quote/removelineitem")]
        public HttpResponseMessage RemoveLineItem([FromBody]DeleteLineItem request)
        {
            Error result = new Error();
            var watch    = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (OrderData.GetOrder(request.orderid, "Q", null, "", "", "").Count == 0) {
                watch.Stop();
                AppData.LogWebApiAudit(request.orderid, "v1/quote/removelineitem", new JavaScriptSerializer().Serialize(request), "", "404: The requested quote was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested quote was not found.");
            }

            result = OrderData.APIWebDeleteMasterItem(request.orderid, request.masteritemid);
            watch.Stop();
            AppData.LogWebApiAudit(request.orderid, "v1/quote/removelineitem", new JavaScriptSerializer().Serialize(request), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { response = result } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Converts a quote to an order.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("quote/quotetoorder")]
        public HttpResponseMessage QuoteToOrder([FromBody]QuoteToOrderParameters request)
        {
            QuoteToOrderResult result = new QuoteToOrderResult();
            var watch                 = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (OrderData.GetOrder(request.orderid, "Q", null, "", "", "").Count == 0) {
                watch.Stop();
                AppData.LogWebApiAudit(request.orderid, "v1/quote/quotetoorder", new JavaScriptSerializer().Serialize(request), "", "404: The requested quote was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested quote was not found.");
            }

            try {
                result = OrderData.QuoteToOrder(request.orderid, request.webusersid);
                watch.Stop();
                AppData.LogWebApiAudit(request.orderid, "v1/quote/quotetoorder", new JavaScriptSerializer().Serialize(request), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));
            }
            catch (Exception ex) {
                watch.Stop();
                AppData.LogWebApiAudit(request.orderid, "v1/quote/quotetoorder", new JavaScriptSerializer().Serialize(request), "", "500: " + ex.Message, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("500", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { response = result } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clears all items, item notes, and notes from a quote.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("quote/{quoteid:alpha}/clearquote")]
        public HttpResponseMessage WebClearQuote([FromUri]string quoteid)
        {
            Error result = new Error();
            var watch    = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (OrderData.GetOrder(quoteid, "Q", null, "", "", "").Count == 0) {
                watch.Stop();
                AppData.LogWebApiAudit(quoteid, "v1/quote/" + quoteid + "/clearquote", new JavaScriptSerializer().Serialize(quoteid), "", "404: The requested quote was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested quote was not found.");
            }

            result = OrderData.WebClearQuote(quoteid);
            watch.Stop();
            AppData.LogWebApiAudit(quoteid, "v1/quote/" + quoteid + "/clearquote", new JavaScriptSerializer().Serialize(quoteid), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { response = result } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Cancels a quote.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("quote/cancel")]
        public HttpResponseMessage CancelQuote([FromBody]CancelOrderParameters request)
        {
            Error result = new Error();
            Order quote  = new Order();
            var watch    = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (OrderData.GetOrder(request.orderid, "Q", null, "", "", "").Count == 0) {
                watch.Stop();
                AppData.LogWebApiAudit(request.orderid, "v1/quote/cancel", new JavaScriptSerializer().Serialize(request), "", "404: The requested quote was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested quote was not found.");
            }

            result = OrderData.CancelDealOrderWeb(request.orderid, request.webusersid);

            if (result.errno != "0") {
                watch.Stop();
                AppData.LogWebApiAudit(request.orderid, "v1/quote/cancel", new JavaScriptSerializer().Serialize(request), "", result.errno + ": " + result.errmsg, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError(result.errno, result.errmsg);
            }

            watch.Stop();
            AppData.LogWebApiAudit(request.orderid, "v1/quote/cancel", new JavaScriptSerializer().Serialize(request), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { response = result } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Copy a Quote.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("quote/copy")]
        public HttpResponseMessage CopyOrder([FromBody]CopyOrderParameters request)
        {
            Error result = new Error();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (OrderData.GetOrder(request.orderid, "Q", null, "", "", "").Count == 0)
            {
                watch.Stop();
                AppData.LogWebApiAudit(request.orderid, "v1/quote/copy", new JavaScriptSerializer().Serialize(request), "", "404: The requested quote was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested quote was not found.");
            }

            result = OrderData.CopyQuoteOrder(request);

            if (result.errno != "0")
            {
                watch.Stop();
                AppData.LogWebApiAudit(request.orderid, "v1/quote/copy", new JavaScriptSerializer().Serialize(request), "", result.errno + ": " + result.errmsg, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError(result.errno, result.errmsg);
            }

            watch.Stop();
            AppData.LogWebApiAudit(request.orderid, "v1/quote/copy", new JavaScriptSerializer().Serialize(request), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { response = result });
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