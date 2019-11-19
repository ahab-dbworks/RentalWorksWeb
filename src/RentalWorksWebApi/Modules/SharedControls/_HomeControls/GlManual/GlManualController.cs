using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.GlManual
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "00B9yDUY6RQfB")]
    public class GlManualController : AppDataController
    {
        public GlManualController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GlManualLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/glmanual/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "01BuY3hf5GooX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/glmanual/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "02cCcIeigorsT", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/glmanual 
        [HttpGet]
        [FwControllerMethod(Id: "02ctPwEor7YPo", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<GlManualLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GlManualLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/glmanual/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "03BSFdjqspuXn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<GlManualLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GlManualLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/glmanual 
        [HttpPost]
        [FwControllerMethod(Id: "040xI7MUiNLJX", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<GlManualLogic>> NewAsync([FromBody]GlManualLogic l)
        {
            return await DoNewAsync<GlManualLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/glmanual/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "suattxPbtlvgP", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<GlManualLogic>> EditAsync([FromRoute] string id, [FromBody]GlManualLogic l)
        {
            return await DoEditAsync<GlManualLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/glmanual/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "04aQ9FD12Mu80", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<GlManualLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
