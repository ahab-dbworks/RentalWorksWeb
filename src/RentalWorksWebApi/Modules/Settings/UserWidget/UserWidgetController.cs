using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WebUserWidget
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class UserWidgetController : AppDataController
    {
        public UserWidgetController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserWidgetLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/userwidget/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(UserWidgetLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/userwidget 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UserWidgetLogic>(pageno, pagesize, sort, typeof(UserWidgetLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/userwidget/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UserWidgetLogic>(id, typeof(UserWidgetLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/userwidget 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]UserWidgetLogic l)
        {
            return await DoPostAsync<UserWidgetLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/userwidget/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(UserWidgetLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}
