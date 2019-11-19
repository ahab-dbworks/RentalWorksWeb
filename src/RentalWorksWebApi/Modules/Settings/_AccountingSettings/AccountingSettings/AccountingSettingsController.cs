using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.AccountingSettings.AccountingSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "Xxp77cNVPq29d")]
    public class AccountingSettingsController : AppDataController
    {
        public AccountingSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AccountingSettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/accountingsettings/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "1rI3oLxBnav8w", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/accountingsettings/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "hdo78WqUx9RYf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/accountingsettings 
        [HttpGet]
        [FwControllerMethod(Id: "FPyuoW6CYkO1G", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<AccountingSettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AccountingSettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/accountingsettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "acozmH5B1QtDD", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<AccountingSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AccountingSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/accountingsettings 
        [HttpPost]
        [FwControllerMethod(Id: "eQZSAhX6sbAVc", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<AccountingSettingsLogic>> NewAsync([FromBody]AccountingSettingsLogic l)
        {
            return await DoNewAsync<AccountingSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/accountingsettings/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "2Q222zsdChQmF", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<AccountingSettingsLogic>> EditAsync([FromRoute] string id, [FromBody]AccountingSettingsLogic l)
        {
            return await DoEditAsync<AccountingSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/accountingsettings/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "PKTZK0Lljf5rh", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<AccountingSettingsLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
