using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.AvailabilityKeepFreshLog
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "OFP98sWaSXqD3")]
    public class AvailabilityKeepFreshLogController : AppDataController
    {
        public AvailabilityKeepFreshLogController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AvailabilityKeepFreshLogLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/availabilitykeepfreshlog/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "pPMhLloOa35IV", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/availabilitykeepfreshlog/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "v0p6DYMcoBPUB", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/availabilitykeepfreshlog 
        [HttpGet]
        [FwControllerMethod(Id: "SBqVa2vFhXEfJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<AvailabilityKeepFreshLogLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AvailabilityKeepFreshLogLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/availabilitykeepfreshlog/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "wEZvVYa9ACbOa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<AvailabilityKeepFreshLogLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AvailabilityKeepFreshLogLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/availabilitykeepfreshlog 
        //[HttpPost]
        //[FwControllerMethod(Id: "LYgo9SzeH7lfR", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<AvailabilityKeepFreshLogLogic>> PostAsync([FromBody]AvailabilityKeepFreshLogLogic l)
        //{
        //    return await DoPostAsync<AvailabilityKeepFreshLogLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/availabilitykeepfreshlog/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "QjARQX8fKzKjS", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <AvailabilityKeepFreshLogLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
