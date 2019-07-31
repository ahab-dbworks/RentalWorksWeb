using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.AccountingSettings
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
        [FwControllerMethod(Id: "1rI3oLxBnav8w")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/accountingsettings/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "hdo78WqUx9RYf")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/accountingsettings 
        [HttpGet]
        [FwControllerMethod(Id: "FPyuoW6CYkO1G")]
        public async Task<ActionResult<IEnumerable<AccountingSettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AccountingSettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/accountingsettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "acozmH5B1QtDD")]
        public async Task<ActionResult<AccountingSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AccountingSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/accountingsettings 
        [HttpPost]
        [FwControllerMethod(Id: "eQZSAhX6sbAVc")]
        public async Task<ActionResult<AccountingSettingsLogic>> PostAsync([FromBody]AccountingSettingsLogic l)
        {
            return await DoPostAsync<AccountingSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/accountingsettings/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "PKTZK0Lljf5rh")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<AccountingSettingsLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
