using RentalWorksAPI.api.v2.Data;
using RentalWorksAPI.api.v2.Models;
using RentalWorksAPI.api.v2.Models.InventoryModels.ICodeStatus;
using RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatus;
using RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatusByICode;
using RentalWorksAPI.api.v2.Models.InventoryModels.WarehouseAddToOrder;
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
        [HttpPost]
        [Route("icodestatus")]
        public HttpResponseMessage GetICodeStatuses([FromBody]ICodeStatus request)
        {
            List<ICode> result = new List<ICode>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = InventoryData.GetICodes(request.masterid, request.warehouseid, request.transactionhistoryqty);

            return Request.CreateResponse(HttpStatusCode.OK, new { icodes = result });
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("itemstatus")]
        public HttpResponseMessage GetItemStatus([FromBody]ItemStatus request)
        {
            ItemStatusResponse response = new ItemStatusResponse();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            response = InventoryData.GetItemStatus(request.barcode, request.serialno, request.rfid, request.days);

            return Request.CreateResponse(HttpStatusCode.OK, new { item = response } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("warehouseaddtoorder")]
        public HttpResponseMessage WarehouseAddToOrder([FromUri]string warehouseid)
        {
            List<WarehouseAddToOrderItem> response = new List<WarehouseAddToOrderItem>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            response = InventoryData.GetWarehouseAddToOrder(warehouseid);

            return Request.CreateResponse(HttpStatusCode.OK, new { items = response } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("warehouseaddtoorder")]
        public HttpResponseMessage WarehousesAddToOrder([FromBody]WarehousesAddToOrder request)
        {
            List<WarehouseAddToOrderItem> response = new List<WarehouseAddToOrderItem>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            response = InventoryData.GetWarehousesAddToOrder(request.warehouseids);

            return Request.CreateResponse(HttpStatusCode.OK, new { items = response } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("itemstatusbyicode")]
        public HttpResponseMessage GetStatusByICode([FromBody]ItemStatusByICode request)
        {
            ItemStatusByICodeResponse result = new ItemStatusByICodeResponse();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = InventoryData.GetItemStatusByICode(request.masterid, request.warehouseid);

            return Request.CreateResponse(HttpStatusCode.OK, new { icodes = result });
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