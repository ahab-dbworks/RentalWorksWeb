using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.PhysicalInventoryInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "BEoHoFVd3JFXN")]
    public class PhysicalInventoryInventoryController : AppDataController
    {
        public PhysicalInventoryInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PhysicalInventoryInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventoryinventory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "V2Cl3r49EVY8B")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventoryinventory/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "31kDu5tRgsaYj")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/physicalinventoryinventory 
        [HttpGet]
        [FwControllerMethod(Id: "lYX4o25qLLXJq")]
        public async Task<ActionResult<IEnumerable<PhysicalInventoryInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PhysicalInventoryInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/physicalinventoryinventory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "CdopKV0vubEcU")]
        public async Task<ActionResult<PhysicalInventoryInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PhysicalInventoryInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventoryinventory 
        [HttpPost]
        [FwControllerMethod(Id: "Mgoxy3IvCQEM3")]
        public async Task<ActionResult<PhysicalInventoryInventoryLogic>> PostAsync([FromBody]PhysicalInventoryInventoryLogic l)
        {
            return await DoPostAsync<PhysicalInventoryInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/physicalinventoryinventory/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "i3spKrXxtqJk1")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PhysicalInventoryInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
