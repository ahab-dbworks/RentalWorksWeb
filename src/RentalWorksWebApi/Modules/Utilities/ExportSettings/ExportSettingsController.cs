using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.ExportSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "F6OTZ7kl2MSD")]
    public class ExportSettingsController : AppDataController
    {
        public ExportSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ExportSettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/exportsettings/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "yvRr40iywjVf")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/exportsettings/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "1h8sUMRr7D6g")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/exportsettings 
        [HttpGet]
        [FwControllerMethod(Id: "xmKCRpcPR6Wf")]
        public async Task<ActionResult<IEnumerable<ExportSettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ExportSettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/exportsettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "B3ZkVuYGb3an")]
        public async Task<ActionResult<ExportSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ExportSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/exportsettings 
        [HttpPost]
        [FwControllerMethod(Id: "yHvGyaL6ePYc")]
        public async Task<ActionResult<ExportSettingsLogic>> PostAsync([FromBody]ExportSettingsLogic l)
        {
            return await DoPostAsync<ExportSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/exportsettings/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "U0INCK4MxulSU")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ExportSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
