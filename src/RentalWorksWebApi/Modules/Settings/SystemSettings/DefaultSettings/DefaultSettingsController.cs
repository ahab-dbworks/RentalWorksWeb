using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SystemSettings.DefaultSettings
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
        [FwControllerMethod(Id: "EF5HatKXARnq0", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/defaultsettings/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "l6KjAhDRXt3Dg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/defaultsettings 
        [HttpGet]
        [FwControllerMethod(Id: "2onkQXENye3oX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DefaultSettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DefaultSettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/defaultsettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "ICCNnubuyh036", ActionType: FwControllerActionTypes.View, ValidateSecurityGroup: false)]
        public async Task<ActionResult<DefaultSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DefaultSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/defaultsettings 
        [HttpPost]
        [FwControllerMethod(Id: "XhVAJzkuyDeWy", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DefaultSettingsLogic>> NewAsync([FromBody]DefaultSettingsLogic l)
        {
            return await DoNewAsync<DefaultSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/defaultsettings/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "10Lm04sc6C6Tb", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DefaultSettingsLogic>> EditAsync([FromRoute] string id, [FromBody]DefaultSettingsLogic l)
        {
            return await DoEditAsync<DefaultSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}
