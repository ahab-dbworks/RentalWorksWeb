using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Warehouse.Activity
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "warehouse-v1")]
    [FwController(Id: "dOXDoJpNXHwlp")]
    public class ActivityController : AppDataController
    {
        public ActivityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ActivityLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/activity/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "DoxYlQ3m74o7u", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/activity/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "Dp3vWQz0YlkRG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/activity 
        [HttpGet]
        [FwControllerMethod(Id: "dPqLDrPyb3ijz", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ActivityLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ActivityLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/activity/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "DQ9POzO8w97r7", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ActivityLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ActivityLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/activity 
        [HttpPost]
        [FwControllerMethod(Id: "Dr4Pnus534GOp", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ActivityLogic>> NewAsync([FromBody]ActivityLogic l)
        {
            return await DoNewAsync<ActivityLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/activity/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "DRCMNE8EGhzhh", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ActivityLogic>> EditAsync([FromRoute] string id, [FromBody]ActivityLogic l)
        {
            return await DoEditAsync<ActivityLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/activity/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "driUiZiWYzv7W", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ActivityLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
