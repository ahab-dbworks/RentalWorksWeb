using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;

namespace WebApi.Modules.Home.InventoryAvailabilityDate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class InventoryAvailabilityDateController : AppDataController
    {
        public InventoryAvailabilityDateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryAvailabilityDateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryavailabilitydate/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/inventoryavailabilitydate/exportexcelxlsx/filedownloadname 
        //[HttpPost("exportexcelxlsx/{fileDownloadName}")]
        //public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        //{
        //    return await DoExportExcelXlsxFileAsync(browseRequest);
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/inventoryavailabilitydate 
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<InventoryAvailabilityDateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<InventoryAvailabilityDateLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/inventoryavailabilitydate/A0000001 
        //[HttpGet("{id}")]
        //public async Task<ActionResult<InventoryAvailabilityDateLogic>> GetOneAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<InventoryAvailabilityDateLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/inventoryavailabilitydate 
        //[HttpPost]
        //public async Task<ActionResult<InventoryAvailabilityDateLogic>> PostAsync([FromBody]InventoryAvailabilityDateLogic l)
        //{
        //    return await DoPostAsync<InventoryAvailabilityDateLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/inventoryavailabilitydate/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
