using RentalWorksAPI.Filters;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RentalWorksAPI.api.v2.Data;
using RentalWorksAPI.api.v2.Models;

namespace RentalWorksAPI.api.v2
{
    [AppConfigAuthorize]
    [RoutePrefix("{apiVersion2:apiVersion2Constraint(v2)}/maintenance")]
    public class MaintenanceController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("orderunits")]
        public HttpResponseMessage GetOrderUnits()
        {
            List<OrderUnit> result = new List<OrderUnit>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = MaintenanceData.GetOrderUnits();

            return Request.CreateResponse(HttpStatusCode.OK, new { orderunits = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("companydepartment")]
        public HttpResponseMessage GetCompanyDepartments()
        {
            List<CompanyDepartment> result = new List<CompanyDepartment>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = MaintenanceData.GetCompanyDepartments();

            return Request.CreateResponse(HttpStatusCode.OK, new { companydepartments = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("inventorytype")]
        public HttpResponseMessage GetInventoryTypes()
        {
            List<InventoryType> result = new List<InventoryType>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = MaintenanceData.GetInventoryTypes();

            return Request.CreateResponse(HttpStatusCode.OK, new { inventorytypes = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("locationwarehouse")]
        public HttpResponseMessage GetLocationWarehouses()
        {
            List<LocationWarehouse> result = new List<LocationWarehouse>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = MaintenanceData.GetLocationWarehouses();

            return Request.CreateResponse(HttpStatusCode.OK, new { locations = result } );
        }
        //----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("group")]
        public HttpResponseMessage GetGroups()
        {
            List<Group> result = new List<Group>();

            if (!ModelState.IsValid)
                ThrowError("400", "");

            result = MaintenanceData.GetGroups();

            return Request.CreateResponse(HttpStatusCode.OK, new { groups = result } );
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