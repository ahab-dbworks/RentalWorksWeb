using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.InventoryAvailabilityDate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"kfQBdYkQaEn")]
    public class InventoryAvailabilityDateController : AppDataController
    {
        public InventoryAvailabilityDateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryAvailabilityDateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryavailabilitydate/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"rlxdkpyctbA")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/inventoryavailabilitydate/exportexcelxlsx/filedownloadname 
        //[HttpPost("exportexcelxlsx/{fileDownloadName}")]
        //[FwControllerMethod(Id:"vl4G5UytHLB")]
        //public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        //{
        //    return await DoExportExcelXlsxFileAsync(browseRequest);
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/inventoryavailabilitydate 
        //[HttpGet]
        //[FwControllerMethod(Id:"N6kkPyOmTsT")]
        //public async Task<ActionResult<IEnumerable<InventoryAvailabilityDateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<InventoryAvailabilityDateLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryavailabilitydate?InventoryId=F010F3BN&WarehouseId=B0029AY5&FromDate=11/01/2018&Todate=11/30/2018
        [HttpGet("")]
        [FwControllerMethod(Id:"bi563cSFahD")]
        public async Task<ActionResult<InventoryAvailabilityDateLogic>> GetCalendarAsync(string InventoryId, string WarehouseId, DateTime FromDate, DateTime ToDate)
        {
            //return await DoGetAsync<InventoryAvailabilityDateLogic>(id);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BrowseRequest request = new BrowseRequest();
                request.pageno = 0;
                request.pagesize = 0;
                request.orderby = string.Empty;

                IDictionary<string, object> uniqueIds = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(InventoryId))
                {
                    uniqueIds.Add("InventoryId", InventoryId);
                }
                if (!string.IsNullOrEmpty(WarehouseId))
                {
                    uniqueIds.Add("WarehouseId", WarehouseId);
                }
                if (FromDate != null)
                {
                    uniqueIds.Add("FromDate", FromDate);
                }
                if (ToDate != null)
                {
                    uniqueIds.Add("ToDate", ToDate);
                }
                request.uniqueids = uniqueIds;

                InventoryAvailabilityDateLogic l = new InventoryAvailabilityDateLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                IEnumerable<InventoryAvailabilityDateLogic> records = await l.SelectAsync<InventoryAvailabilityDateLogic>(request);
                return new OkObjectResult(records);
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                if (ex.InnerException != null)
                {
                    jsonException.Message += $"\n\nInnerException: \n{ex.InnerException.Message}";
                }
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }

        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/inventoryavailabilitydate 
        //[HttpPost]
        //[FwControllerMethod(Id:"lFjbBIMbanE")]
        //public async Task<ActionResult<InventoryAvailabilityDateLogic>> PostAsync([FromBody]InventoryAvailabilityDateLogic l)
        //{
        //    return await DoPostAsync<InventoryAvailabilityDateLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/inventoryavailabilitydate/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"nguPw8yFDkb")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
