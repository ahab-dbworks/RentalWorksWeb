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
    [RoutePrefix("{apiVersion2:apiVersion2Constraint(v2)}/inventory")]
    public class InventoryController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("icodestatus")]
        public HttpResponseMessage GetICodeStatuses([FromUri]string warehouseid, [FromUri]int? transactionhistoryqty=0, [FromUri]string masterid="")
        {
            List<ICode> result = new List<ICode>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = InventoryData.GetICodes(masterid, warehouseid, transactionhistoryqty);

            return Request.CreateResponse(HttpStatusCode.OK, new { icodes = result } );
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