using RentalWorksAPI.api.v1.Data;
using RentalWorksAPI.api.v1.Models;
using RentalWorksAPI.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace RentalWorksAPI.api.v1
{
    [AppConfigAuthorize]
    [RoutePrefix("{apiVersion1:apiVersion1Constraint(v1)}")] 
    public class InventoryController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("inventory/rental/{asofdate:datetime}")]
        public HttpResponseMessage GetRentalInventoryAsOf([FromUri]string asofdate)
        {
            DateTime dDate;
            List<RentalItem> result = new List<RentalItem>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (!DateTime.TryParse(asofdate, out dDate))
            {
                string message = string.Format("The date input ({0}) is invalid", asofdate);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }

            result = InventoryData.GetRentalItemsAsOf(asofdate);

            return Request.CreateResponse(HttpStatusCode.OK, new { rentalinventory = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("inventory/sales/{asofdate:datetime}")]
        public HttpResponseMessage GetSalesInventoryAsOf([FromUri]string asofdate)
        {
            DateTime dDate;
            List<SaleItem> result = new List<SaleItem>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            if (!DateTime.TryParse(asofdate, out dDate))
            {
                string message = string.Format("The date input ({0}) is invalid", asofdate);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }

            result = InventoryData.GetSalesItemsAsOf(asofdate);

            return Request.CreateResponse(HttpStatusCode.OK, new { salesinventory = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("inventory/highlyuseditems")]
        public HttpResponseMessage GetHighlyUsedInventory([FromBody]HighlyUsedItem request)
        {
            List<RentalItem> result = new List<RentalItem>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = InventoryData.HighlyUsedInventory(request.dealid, request.departmentid);

            return Request.CreateResponse(HttpStatusCode.OK, new { highlyuseditems = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("inventory/completesandkits")]
        public HttpResponseMessage GetCompletesAndKits([FromBody]CompletesAndKits request)
        {
            List<CompleteKit> result = new List<CompleteKit>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = InventoryData.GetCompletesAndKits(request.packageid, request.warehouseid);

            return Request.CreateResponse(HttpStatusCode.OK, new { completesandkits = result } );
        }
        //----------------------------------------------------------------------------------------------------
        private void ThrowError(string errno, string errmsg)
        {
            switch (errno)
            {
                case "400": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
                case "404": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,   errmsg));
                case "409": throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict,   errmsg));
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
}