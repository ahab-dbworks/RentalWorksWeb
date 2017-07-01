using RentalWorksAPI.api.v2.Data;
using RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract;
using RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels;
using RentalWorksAPI.api.v2.Models.WarehouseModels.UnstageItemModels;
using RentalWorksAPI.Filters;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentalWorksAPI.api.v2
{
    [AppConfigAuthorize]
    [RoutePrefix("{apiVersion2:apiVersion2Constraint(v2)}/warehouse")]
    public class WarehouseController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("stageitem")]
        public HttpResponseMessage StageItem([FromBody]StageItemRequest request)
        {
            StageItemResponse response = new StageItemResponse();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            response = WarehouseData.StageItem(request.orderid, request.webusersid, request.items);

            return Request.CreateResponse(HttpStatusCode.OK, new { order = response });
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("unstageitem")]
        public HttpResponseMessage UnstageItem([FromBody]UnstageItemRequest request)
        {
            UnstageItemResponse response = new UnstageItemResponse();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            response = WarehouseData.UnstageItem(request.orderid, request.webusersid, request.items);

            return Request.CreateResponse(HttpStatusCode.OK, new { order = response });
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("movetocontract")]
        public HttpResponseMessage MoveToContract([FromBody]MoveToContractRequest request)
        {
            MoveToContractResponse response = new MoveToContractResponse();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            response = WarehouseData.MoveToContract(request.orderid, request.webusersid, request.items);

            return Request.CreateResponse(HttpStatusCode.OK, new { order = response });
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