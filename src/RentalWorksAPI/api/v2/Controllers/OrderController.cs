using RentalWorksAPI.api.v2.Data;
using RentalWorksAPI.api.v2.Models;
using RentalWorksAPI.Filters;
using System.Collections.Generic;
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
        [HttpGet]
        [Route("csrsdeals")]
        public HttpResponseMessage GetCsrsDeals([FromUri]string locationid, [FromUri]List<string> csrid)
        {
            List<Csrs> result = new List<Csrs>();
            Csrs csrs = new Csrs();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            for (int i = 0; i < csrid.Count; i++)
            {
                csrs = OrderData.GetCsrs(locationid, csrid[i]);

                result.Add(csrs);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { Csrs = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("ordersanditems")]
        public HttpResponseMessage GetOrdersAndItems([FromUri]string locationid, [FromUri]string departmentid="", [FromUri]string lastmodifiedfromdate="",
                                                     [FromUri]string lastmodifiedtodate="", [FromUri]string includeavailabilityqty="", [FromUri]string orderid="",
                                                     [FromUri]List<string> agentid=null, [FromUri]List<string> status=null, [FromUri]List<string> dealid=null)
        {
            List<OrdersAndItems> result = new List<OrdersAndItems>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = OrderData.GetOrdersAndItems(locationid, departmentid, lastmodifiedfromdate, lastmodifiedtodate, includeavailabilityqty, orderid, agentid, status, dealid);

            return Request.CreateResponse(HttpStatusCode.OK, new { OrdersAndItems = result } );
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