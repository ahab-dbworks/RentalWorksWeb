using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.AdministratorControls.CustomReportLayoutGroup;

namespace WebApi.Modules.Administrator.CustomReportLayoutGroup
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
        [FwControllerMethod(Id: "5l1Q6S3NU7GWH")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutgroup/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "bVK6BBY28J0BX")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customreportlayoutgroup 
        [HttpGet]
        [FwControllerMethod(Id: "90J3SdJrzcZr2")]
        public async Task<ActionResult<IEnumerable<CustomReportLayoutGroupLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomReportLayoutGroupLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customreportlayoutgroup/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "qwn2LLqPcM6o7")]
        public async Task<ActionResult<CustomReportLayoutGroupLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomReportLayoutGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutgroup 
        [HttpPost]
        [FwControllerMethod(Id: "lRvLDf3feKXWV")]
        public async Task<ActionResult<CustomReportLayoutGroupLogic>> PostAsync([FromBody]CustomReportLayoutGroupLogic l)
        {
            return await DoPostAsync<CustomReportLayoutGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/customreportlayoutgroup/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "hKF5VDMeGCk0n")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomReportLayoutGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
