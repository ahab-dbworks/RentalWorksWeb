using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Administrator.Group;

namespace WebApi.Modules.AdministratorControls.CustomReportLayoutGroup
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "N5ZpGhzZvahV2")]
    public class CustomReportLayoutGroupController : AppDataController
    {
        public CustomReportLayoutGroupController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomReportLayoutGroupLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutgroup/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "5l1Q6S3NU7GWH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutgroup/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "bVK6BBY28J0BX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customreportlayoutgroup 
        [HttpGet]
        [FwControllerMethod(Id: "90J3SdJrzcZr2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CustomReportLayoutGroupLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomReportLayoutGroupLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customreportlayoutgroup/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "qwn2LLqPcM6o7", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CustomReportLayoutGroupLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomReportLayoutGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutgroup 
        [HttpPost]
        [FwControllerMethod(Id: "lRvLDf3feKXWV", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CustomReportLayoutGroupLogic>> NewAsync([FromBody]CustomReportLayoutGroupLogic l)
        {
            return await DoNewAsync<CustomReportLayoutGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/customreportlayoutgroup/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "CPcoJ7KtL4ge", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CustomReportLayoutGroupLogic>> EditAsync([FromRoute] string id, [FromBody]CustomReportLayoutGroupLogic l)
        {
            return await DoEditAsync<CustomReportLayoutGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/customreportlayoutgroup/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "hKF5VDMeGCk0n", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomReportLayoutGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customreportlayoutgroup/validategroupname/browse
        [HttpPost("validategroupname/browse")]
        [FwControllerMethod(Id: "jFmtm95g6IQY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateGroupBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GroupLogic>(browseRequest);
        }
    }
}
