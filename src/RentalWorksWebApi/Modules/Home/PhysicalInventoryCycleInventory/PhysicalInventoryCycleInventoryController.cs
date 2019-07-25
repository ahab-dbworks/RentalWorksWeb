using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.PhysicalInventoryCycleInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "juyq8FkxJPR5Q")]
    public class PhysicalInventoryCycleInventoryController : AppDataController
    {
        public PhysicalInventoryCycleInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PhysicalInventoryCycleInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventorycycleinventory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "GwO069bIU8q7X")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventorycycleinventory/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "qTYHT385W14JO")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/physicalinventorycycleinventory 
        [HttpGet]
        [FwControllerMethod(Id: "0OLGeg9p8seDC")]
        public async Task<ActionResult<IEnumerable<PhysicalInventoryCycleInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PhysicalInventoryCycleInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/physicalinventorycycleinventory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "SxDKfqP6mAjjE")]
        public async Task<ActionResult<PhysicalInventoryCycleInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PhysicalInventoryCycleInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventorycycleinventory 
        [HttpPost]
        [FwControllerMethod(Id: "WnSydK7diEE53")]
        public async Task<ActionResult<PhysicalInventoryCycleInventoryLogic>> PostAsync([FromBody]PhysicalInventoryCycleInventoryLogic l)
        {
            return await DoPostAsync<PhysicalInventoryCycleInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/physicalinventorycycleinventory/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "9i0ofsxK0wV78")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PhysicalInventoryCycleInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
