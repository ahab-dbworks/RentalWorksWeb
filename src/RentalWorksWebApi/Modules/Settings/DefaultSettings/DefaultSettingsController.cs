using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.DefaultSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "6pvUgTaKPnjf3")]
    public class DefaultSettingsController : AppDataController
    {
        public DefaultSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DefaultSettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/defaultsettings/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "EF5HatKXARnq0")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/defaultsettings/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "l6KjAhDRXt3Dg")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/defaultsettings 
        [HttpGet]
        [FwControllerMethod(Id: "2onkQXENye3oX")]
        public async Task<ActionResult<IEnumerable<DefaultSettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DefaultSettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/defaultsettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "ICCNnubuyh036")]
        public async Task<ActionResult<DefaultSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DefaultSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/defaultsettings 
        [HttpPost]
        [FwControllerMethod(Id: "XhVAJzkuyDeWy")]
        public async Task<ActionResult<DefaultSettingsLogic>> PostAsync([FromBody]DefaultSettingsLogic l)
        {
            return await DoPostAsync<DefaultSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}
