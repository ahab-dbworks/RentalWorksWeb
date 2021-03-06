using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.SystemSettings.EmailSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "7MWuq0m77CnZ8")]
    public class EmailSettingsController : AppDataController
    {
        public EmailSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(EmailSettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/emailsettings/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "fl3P8RRc2ZPC", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/emailsettings/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "i7Aj63M9bV4K", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/emailsettings 
        [HttpGet]
        [FwControllerMethod(Id: "n5EyqQfaTLgr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<EmailSettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<EmailSettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/emailsettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "n5jtyfQLYC5c2", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<EmailSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<EmailSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/emailsettings 
        [HttpPost]
        [FwControllerMethod(Id: "XD3d0jwLrii65", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<EmailSettingsLogic>> NewAsync([FromBody]EmailSettingsLogic l)
        {
            return await DoNewAsync<EmailSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/emailsettings/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "crCCsp2aLp9m1", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<EmailSettingsLogic>> EditAsync([FromRoute] string id, [FromBody]EmailSettingsLogic l)
        {
            return await DoEditAsync<EmailSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/emailsettings/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "JXBw6zydRQb8", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <EmailSettingsLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
