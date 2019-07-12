using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Data;
using RentalWorksAPI.api.v2.Models;
using RentalWorksAPI.api.v2.Models.OrderModels.CsrsDeals;
using RentalWorksAPI.api.v2.Models.OrderModels.OrdersAndItems;
using RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetail;
using RentalWorksAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace RentalWorksAPI.api.v2
{
    [AppConfigAuthorize]
    [RoutePrefix("{apiVersion2:apiVersion2Constraint(v2)}/order")]
    public class OrderController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("csrsdeals")]
        public HttpResponseMessage GetCsrsDeals([FromBody]CsrsDeals request)
        {
            List<Csrs> result = new List<Csrs>();
            Csrs csrs = new Csrs();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            for (int i = 0; i < request.csrid.Count; i++)
            {
                csrs = OrderData.GetCsrs(request.locationid, request.csrid[i]);

                result.Add(csrs);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { Csrs = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("ordersanditems")]
        public HttpResponseMessage GetOrdersAndItems([FromBody]OrdersAndItems request)
        {
            List<OrdersAndItemsResponse> result = new List<OrdersAndItemsResponse>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = OrderData.GetOrdersAndItems(request.locationid, request.departmentid, request.lastmodifiedfromdate, request.lastmodifiedtodate, request.orderid, request.agentid, request.status, request.dealid);

            return Request.CreateResponse(HttpStatusCode.OK, new { OrdersAndItems = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("orderstatusdetail")]
        public HttpResponseMessage GetOrderStatusDetail([FromUri]string orderid)
        {
            OrderStatusDetailResponse response = new OrderStatusDetailResponse();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            response.orderid   = orderid;
            response.orderdesc = FwSqlCommand.GetData(FwSqlConnection.RentalWorks, "dealorder", "orderid", orderid, "orderdesc").ToString().TrimEnd();
            response.items     = OrderData.GetOrderStatus(orderid);

            return Request.CreateResponse(HttpStatusCode.OK, new { Order = response } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("lineitemadd")]
        public HttpResponseMessage ProcessOrderLineItem([FromBody]OrderItems orderitem)
        {
            dynamic orderitemresponse;
            OrderItems result = new OrderItems();
            var watch         = System.Diagnostics.Stopwatch.StartNew();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (OrderData.GetOrder(orderitem.orderid, "O", null, "", "", "").Count == 0) {
                watch.Stop();
                AppData.LogWebApiAudit(orderitem.orderid, "v2/order/lineitemadd", new JavaScriptSerializer().Serialize(orderitem), "", "404: The requested order was not found.", Convert.ToInt32(watch.Elapsed.TotalSeconds));
                ThrowError("404", "The requested order was not found.");
            }

            result.items = new List<OrderItem>();
            for (int i = 0; i < orderitem.items.Count; i++)
            {
                orderitemresponse = OrderData.ProcessAddOrderItem(orderitem.items[i], orderitem.orderid);
                if (orderitemresponse.errno != "0")
                {
                    watch.Stop();
                    AppData.LogWebApiAudit(orderitem.orderid, "v2/order/lineitemadd", new JavaScriptSerializer().Serialize(orderitem), "", "500: " + orderitemresponse.errmsg, Convert.ToInt32(watch.Elapsed.TotalSeconds));
                    ThrowError("500", orderitemresponse.errmsg);
                }
                else
                {
                    OrderItem itemordered = new OrderItem();

                    itemordered.masteritemid         = orderitemresponse.masteritemid;
                    itemordered.masterid             = orderitemresponse.masterid;
                    itemordered.description          = orderitemresponse.description;
                    itemordered.rentfromdate         = orderitemresponse.rentfromdate;
                    itemordered.rentfromtime         = orderitemresponse.rentfromtime;
                    itemordered.renttodate           = orderitemresponse.renttodate;
                    itemordered.renttotime           = orderitemresponse.renttotime;
                    itemordered.qtyordered           = orderitemresponse.qtyordered;
                    itemordered.unit                 = orderitemresponse.unit;
                    itemordered.price                = orderitemresponse.price;
                    itemordered.daysinwk             = orderitemresponse.daysinwk;
                    itemordered.notes                = orderitemresponse.notes;
                    itemordered.parentid             = orderitemresponse.parentid;
                    itemordered.nestedmasteritemid   = orderitemresponse.nestedmasteritemid;
                    
                    itemordered.unitextended         = orderitemresponse.unitextended;
                    itemordered.periodextended       = orderitemresponse.periodextended;
                    itemordered.weeklyextended       = orderitemresponse.weeklyextended;
                    itemordered.taxable              = orderitemresponse.taxable;
                    itemordered.inactive             = orderitemresponse.inactive;
                    itemordered.itemorder            = orderitemresponse.itemorder;

                    result.items.Add(itemordered);

                    if ((orderitemresponse.itemclass == "C") || (orderitemresponse.itemclass == "K"))
                    {
                        result.items.AddRange(OrderData.FuncCompleteAddProcessMasterItem(orderitem.orderid, orderitemresponse.masteritemid, orderitemresponse.parentid, orderitemresponse.qtyordered));
                    }
                }
            }

            OrderData.UpdateOrderTimeStamp(orderitem.orderid);

            result.orderid = orderitem.orderid;
            //result.items   = OrderData.GetOrderItems(orderitem.orderid, itemsadded.ToArray());
            watch.Stop();
            AppData.LogWebApiAudit(orderitem.orderid, "v2/order/lineitemadd", new JavaScriptSerializer().Serialize(orderitem), new JavaScriptSerializer().Serialize(result), "", Convert.ToInt32(watch.Elapsed.TotalSeconds));

            return Request.CreateResponse(HttpStatusCode.OK, new { order = result } );
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