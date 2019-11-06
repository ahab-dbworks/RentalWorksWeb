using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.Modules.Administrator.WebAlertLog;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Administrator.WebAlertLog
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "x6SZhutIpRi2")]
    public class WebAlertLogController : AppDataController
    {
        public WebAlertLogController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WebAlertLogLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/webalertlog/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "71KyXeCS17Wt")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/webalertlog/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "dZbUl3JcKftNx")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/webalertlog 
        [HttpGet]
        [FwControllerMethod(Id: "TeeZowPzYWW2")]
        public async Task<ActionResult<IEnumerable<WebAlertLogLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WebAlertLogLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/webalertlog/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "hsrZoaUktWax")]
        public async Task<ActionResult<WebAlertLogLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WebAlertLogLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/webalertlog 
        [HttpPost]
        [FwControllerMethod(Id: "z7FZ8SZWVp1Me")]
        public async Task<ActionResult<WebAlertLogLogic>> PostAsync([FromBody]WebAlertLogLogic l)
        {
            return await DoPostAsync<WebAlertLogLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/webalertlog/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "IVXuvsvLXHTXf")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WebAlertLogLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
