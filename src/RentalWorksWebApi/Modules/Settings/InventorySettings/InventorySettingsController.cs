using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.InventorySettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "fdUclyoOCTYbx")]
    public class InventorySettingsController : AppDataController
    {
        public InventorySettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventorySettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysettings/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "3FyjvWTl50w3v")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysettings/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "mrtsAFG6nhFFh")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorysettings 
        [HttpGet]
        [FwControllerMethod(Id: "7XP2z9LnG9Mj4")]
        public async Task<ActionResult<IEnumerable<InventorySettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventorySettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorysettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "eNVRBUAAQxKcG")]
        public async Task<ActionResult<InventorySettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventorySettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysettings 
        [HttpPost]
        [FwControllerMethod(Id: "DczWhwgZsHeZZ")]
        public async Task<ActionResult<InventorySettingsLogic>> PostAsync([FromBody]InventorySettingsLogic l)
        {
            return await DoPostAsync<InventorySettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}
