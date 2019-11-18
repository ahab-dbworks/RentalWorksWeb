using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WebUserWidget
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"KyEyiVluAsCjQ")]
    public class UserWidgetController : AppDataController
    {
        public UserWidgetController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserWidgetLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/userwidget/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"PeHTbjKsLHtnu")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"zpF2l1RuADt8I")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/userwidget 
        [HttpGet]
        [FwControllerMethod(Id:"W4DljMPTyEAIh")]
        public async Task<ActionResult<IEnumerable<UserWidgetLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UserWidgetLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/userwidget/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"VQHoY29L1Iidj")]
        public async Task<ActionResult<UserWidgetLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UserWidgetLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/userwidget 
        [HttpPost]
        [FwControllerMethod(Id:"uHu8JjxydlCt0")]
        public async Task<ActionResult<UserWidgetLogic>> PostAsync([FromBody]UserWidgetLogic l)
        {
            return await DoPostAsync<UserWidgetLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/userwidget/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"owpUAateZ4CZX")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<UserWidgetLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
