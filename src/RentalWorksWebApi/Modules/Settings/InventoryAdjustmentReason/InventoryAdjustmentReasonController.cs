using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.InventoryAdjustmentReason
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class InventoryAdjustmentReasonController : AppDataController
    {
        public InventoryAdjustmentReasonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryAdjustmentReasonLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryadjustmentreason/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventoryadjustmentreason
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryAdjustmentReasonLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryAdjustmentReasonLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventoryadjustmentreason/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryAdjustmentReasonLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryAdjustmentReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryadjustmentreason
        [HttpPost]
        public async Task<ActionResult<InventoryAdjustmentReasonLogic>> PostAsync([FromBody]InventoryAdjustmentReasonLogic l)
        {
            return await DoPostAsync<InventoryAdjustmentReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventoryadjustmentreason/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}