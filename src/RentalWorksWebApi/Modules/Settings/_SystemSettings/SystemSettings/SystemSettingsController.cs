using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SystemSettings.SystemSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "v3tPhS7Ug7qgO")]
    public class SystemSettingsController : AppDataController
    {
        public SystemSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SystemSettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/systemsettings/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "uZJW9CmNiLlu8")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/systemsettings/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "wXsbIRUU1uD52")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/systemsettings 
        [HttpGet]
        [FwControllerMethod(Id: "JEjEzbNTnsw75")]
        public async Task<ActionResult<IEnumerable<SystemSettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SystemSettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/systemsettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "BBX83CGMRS6QY")]
        public async Task<ActionResult<SystemSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SystemSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/systemsettings 
        [HttpPost]
        [FwControllerMethod(Id: "3gu2FTCNbhLzH")]
        public async Task<ActionResult<SystemSettingsLogic>> PostAsync([FromBody]SystemSettingsLogic l)
        {
            return await DoPostAsync<SystemSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}
