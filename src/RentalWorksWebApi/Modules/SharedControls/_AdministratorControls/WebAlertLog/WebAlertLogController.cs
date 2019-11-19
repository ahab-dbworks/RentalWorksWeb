using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.Modules.Administrator.WebAlertLog;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.AdministratorControls.WebAlertLog
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
        [FwControllerMethod(Id: "71KyXeCS17Wt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/webalertlog/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "dZbUl3JcKftNx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/webalertlog 
        [HttpGet]
        [FwControllerMethod(Id: "TeeZowPzYWW2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WebAlertLogLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WebAlertLogLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/webalertlog/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "hsrZoaUktWax", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<WebAlertLogLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WebAlertLogLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/webalertlog 
        [HttpPost]
        [FwControllerMethod(Id: "z7FZ8SZWVp1Me", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WebAlertLogLogic>> NewAsync([FromBody]WebAlertLogLogic l)
        {
            return await DoNewAsync<WebAlertLogLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/webalertlog/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "EPTmz32qPMMpL", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WebAlertLogLogic>> EditAsync([FromRoute] string id, [FromBody]WebAlertLogLogic l)
        {
            return await DoEditAsync<WebAlertLogLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/webalertlog/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "IVXuvsvLXHTXf", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WebAlertLogLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
