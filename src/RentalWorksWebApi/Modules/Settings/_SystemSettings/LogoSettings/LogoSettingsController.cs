using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SystemSettings.LogoSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "FM7iCQcVmmUqK")]
    public class LogoSettingsController : AppDataController
    {
        public LogoSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(LogoSettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/logosettings/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id: "ZyKhIG1cqUyqT", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/logosettings/exportexcelxlsx/filedownloadname
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "OdHpM2C2HUovb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/logosettings
        [HttpGet]
        [FwControllerMethod(Id: "fltHROiaWTc1m", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<LogoSettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<LogoSettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/logosettings/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "Z5Vhjt4wF2jjU", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<LogoSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<LogoSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/logosettings
        [HttpPost]
        [FwControllerMethod(Id: "I3Z3AMwh29I3i", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<LogoSettingsLogic>> NewAsync([FromBody]LogoSettingsLogic l)
        {
            return await DoNewAsync<LogoSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/logosetting/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "OO8oyiKu3ZyqC", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<LogoSettingsLogic>> EditAsync([FromRoute] string id, [FromBody]LogoSettingsLogic l)
        {
            return await DoEditAsync<LogoSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}
