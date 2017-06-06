﻿using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Data;
using RentalWorksAPI.api.v2.Models;
using RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetailModels;
using RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract;
using RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels;
using RentalWorksAPI.api.v2.Models.WarehouseModels.UnstageItemModels;
using RentalWorksAPI.Filters;
using System.Collections.Generic;
using System.Linq;
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
        [HttpPost]
        [Route("stageitem")]
        public HttpResponseMessage StageItem([FromBody]StageItemRequest request)
        {
            StageItemResponse response = new StageItemResponse();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            foreach (StageItem item in request.items)
            {
                StageItemQry stageItemResult = WarehouseData.StageItem(request.orderid, item.barcode, item.masteritemid, item.qty);
            }


            return Request.CreateResponse(HttpStatusCode.OK, new { Csrs = response });
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("unstageitem")]
        public HttpResponseMessage UnstageItem([FromBody]UnstageItemRequest request)
        {
            UnstageItemResponse response = new UnstageItemResponse();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            foreach (UnstageItemRequestItem item in request.items)
            {
                UnstageItemQry stageItemResult = WarehouseData.UnstageItem(request.orderid, item.barcode, item.masteritemid, item.qty);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { Csrs = response });
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("movetocontract")]
        public HttpResponseMessage MoveToContract([FromBody]MoveToContractRequest request)
        {
            MoveToContractResponse response = new MoveToContractResponse();
            response.order.orderid = request.orderid;
            for (int i = request.items.Count - 1; i >= 0; i--)
            {
                StagedItem requestitem = request.items[i];
                MoveToContractSp responseitem = WarehouseData.MoveToContract(request.usersid, request.orderid, requestitem.barcode, requestitem.masteritemid, requestitem.quantity);
                response.order.items.Add(responseitem);
            }

            if (!ModelState.IsValid)
                ThrowError("400", "");

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