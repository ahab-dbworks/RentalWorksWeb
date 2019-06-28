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
    public class OrderController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Get a list of all orders.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("order")]
        public HttpResponseMessage GetOrders([FromBody]OrderParameters request)
        {
            List<Order> result = new List<Order>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = OrderData.GetOrder("", "O", request.statuses, request.rental, request.sales, request.datestamp);

            return Request.CreateResponse(HttpStatusCode.OK, new { orders = result } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Get a specific order.
        /// </summary>
        /// <param name="orderid">Order Id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("order/{orderid}")]
        public HttpResponseMessage GetOrder([FromUri]string orderid)
        {
            List<Order> orders = new List<Order>();
            Order result       = new Order();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            orders = OrderData.GetOrder(orderid, "O", null, "", "", "");

            if (orders.Count == 0)
                ThrowError("404", "The requested order was not found.");

            result       = orders[0];
            result.items = OrderData.GetOrderItems(orderid);

            return Request.CreateResponse(HttpStatusCode.OK, new { order = result } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Create or update a order.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("order/save")]
        public HttpResponseMessage ProcessOrder([FromBody]Order order)
        {
            Order result     = new Order();
            dynamic postdata = new ExpandoObject();
            var watch        = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if ((!string.IsNullOrEmpty(order.orderid)) && (OrderData.GetOrder(order.orderid, "O", null, "", "", "").Count == 0)) {
                watch.Stop();
                AppData.LogWebApiAudit(((order.orderid == null) ? "" : order.orderid), "v1/order/save", new JavaScriptSerializer().Serialize(order), "", "404: The requested order was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested order was not found.");
            }

            if ((order.deal != null) && ((!string.IsNullOrEmpty(order.deal.dealid)) || (!string.IsNullOrEmpty(order.deal.dealno)))) {
                List<Deal> founddeal = DealData.GetDeal(order.deal.dealid, order.deal.dealno);
                if (founddeal.Count == 0) {
                    watch.Stop();
                    AppData.LogWebApiAudit(((order.orderid == null) ? "" : order.orderid), "v1/order/save", new JavaScriptSerializer().Serialize(order), "", "404: The deal being assigned was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                    ThrowError("404", "The deal being assigned was not found.");
                } else {
                    order.deal.dealid = founddeal[0].dealid;
                }
            }

            try {
                postdata = OrderData.ProcessQuote(order);
            }
            catch (Exception ex) {
                watch.Stop();
                AppData.LogWebApiAudit(((order.orderid == null) ? "" : order.orderid), "v1/order/save", new JavaScriptSerializer().Serialize(order), "", "500: " + ex.Message, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("500", ex.Message);
            }

            if (postdata.errno != "0") {
                watch.Stop();
                AppData.LogWebApiAudit(((order.orderid == null) ? "" : order.orderid), "v1/order/save", new JavaScriptSerializer().Serialize(order), "", "409: " + postdata.errmsg, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("409", postdata.errmsg);
            }

            result = OrderData.GetOrder(postdata.orderid, "O", null, "", "", "")[0];
            watch.Stop();
            AppData.LogWebApiAudit(((order.orderid == null) ? result.orderid : order.orderid), "v1/order/save", new JavaScriptSerializer().Serialize(order), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { order = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("order/lineitems")]
        public HttpResponseMessage ProcessOrderLineItem([FromBody]OrderItems orderitem)
        {
            dynamic orderitemresponse;
            OrderItems result = new OrderItems();
            var watch         = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (OrderData.GetOrder(orderitem.orderid, "O", null, "", "", "").Count == 0) {
                watch.Stop();
                AppData.LogWebApiAudit(orderitem.orderid, "v1/order/lineitems", new JavaScriptSerializer().Serialize(orderitem), "", "404: The requested order was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested order was not found.");
            }

            for (int i = 0; i < orderitem.items.Count; i++)
            {
                orderitemresponse = OrderData.ProcessOrderItem(orderitem.items[i], orderitem.orderid);
                if (orderitemresponse.errno != "0")
                {
                    watch.Stop();
                    AppData.LogWebApiAudit(orderitem.orderid, "v1/order/lineitems", new JavaScriptSerializer().Serialize(orderitem), "", "404: The requested order was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                    ThrowError("500", orderitemresponse.errmsg);
                }
            }

            OrderData.UpdateOrderTimeStamp(orderitem.orderid);

            result.orderid = orderitem.orderid;
            result.items   = OrderData.GetOrderItems(orderitem.orderid);
            watch.Stop();
            AppData.LogWebApiAudit(orderitem.orderid, "v1/order/lineitems", new JavaScriptSerializer().Serialize(orderitem), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { order = result } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Cancels an order.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("order/cancel")]
        public HttpResponseMessage CancelOrder([FromBody]CancelOrderParameters request)
        {
            Error result = new Error();
            var watch    = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (OrderData.GetOrder(request.orderid, "O", null, "", "", "").Count == 0) {
                watch.Stop();
                AppData.LogWebApiAudit(request.orderid, "v1/order/cancel", new JavaScriptSerializer().Serialize(request), "", "404: The requested order was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested order was not found.");
            }

            result = OrderData.CancelDealOrderWeb(request.orderid, request.webusersid);

            if (result.errno != "0") {
                watch.Stop();
                AppData.LogWebApiAudit(request.orderid, "v1/order/cancel", new JavaScriptSerializer().Serialize(request), "", result.errno + ": " + result.errmsg, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError(result.errno, result.errmsg);
            }

            watch.Stop();
            AppData.LogWebApiAudit(request.orderid, "v1/order/cancel", new JavaScriptSerializer().Serialize(request), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { response = result } );
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Copy an order.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("order/copy")]
        public HttpResponseMessage CopyOrder([FromBody]CopyOrderParameters request)
        {
            Error result = new Error();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (OrderData.GetOrder(request.orderid, "O", null, "", "", "").Count == 0)
            {
                watch.Stop();
                AppData.LogWebApiAudit(request.orderid, "v1/order/copy", new JavaScriptSerializer().Serialize(request), "", "404: The requested order was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested order was not found.");
            }

            result = OrderData.CopyQuoteOrder(request);

            if (result.errno != "0")
            {
                watch.Stop();
                AppData.LogWebApiAudit(request.orderid, "v1/order/copy", new JavaScriptSerializer().Serialize(request), "", result.errno + ": " + result.errmsg, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError(result.errno, result.errmsg);
            }

            watch.Stop();
            AppData.LogWebApiAudit(request.orderid, "v1/order/copy", new JavaScriptSerializer().Serialize(request), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

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