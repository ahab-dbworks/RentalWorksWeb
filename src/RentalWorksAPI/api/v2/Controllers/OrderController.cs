using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Data;
using RentalWorksAPI.api.v2.Models;
using RentalWorksAPI.api.v2.Models.OrderModels.CsrsDeals;
using RentalWorksAPI.api.v2.Models.OrderModels.LineItems;
using RentalWorksAPI.api.v2.Models.OrderModels.OrdersAndItems;
using RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetailModels;
using RentalWorksAPI.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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

            result = OrderData.GetOrdersAndItems(request.locationid, request.departmentid, request.lastmodifiedfromdate, request.lastmodifiedtodate, request.includeavailabilityqty, request.orderid, request.agentid, request.status, request.dealid);

            return Request.CreateResponse(HttpStatusCode.OK, new { OrdersAndItems = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("orderstatusdetail")]
        public HttpResponseMessage GetOrderStatusDetail([FromUri]string orderid)
        {
            OrderStatusDetailResponse response = new OrderStatusDetailResponse();
            response.order.orderid = orderid;
            response.order.orderdesc = FwSqlCommand.GetData(FwSqlConnection.RentalWorks, "dealorder", "orderid", orderid, "orderdesc").ToString().TrimEnd();
            List<OrderStatus> orderstatus = OrderData.GetOrderStatus(orderid);
            List<OrderStatusDetail> orderstatusdetail = OrderData.GetOrderStatusDetail(orderid);
            foreach (OrderStatus lineitem in orderstatus)
            {
                var detailitems = orderstatusdetail.Where(x => x.masterid == lineitem.masterid);
                lineitem.physicalassets.AddRange(detailitems);
            }

            if (!ModelState.IsValid)
                ThrowError("400", "");

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("lineitems")]
        public HttpResponseMessage LineItems([FromUri]string orderid)
        {
            LineItemsResponse response = new LineItemsResponse();
            response.order.orderid = orderid;
            response.order.items = OrderData.GetLineItems(orderid);

            if (!ModelState.IsValid)
                ThrowError("400", "");

            return Request.CreateResponse(HttpStatusCode.OK, response);
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