using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
namespace WebApi.Modules.Administrator.AlertCondition
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "9ydIrby0QrLz ")]
    public class AlertConditionController : AppDataController
    {
        public AlertConditionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AlertConditionLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alertcondition/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "vULlOXxRd1O3 ")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alertcondition/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "mPSsPp76B0q5M")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/alertcondition 
        [HttpGet]
        [FwControllerMethod(Id: "fTgwAwswen477")]
        public async Task<ActionResult<IEnumerable<AlertConditionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AlertConditionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/alertcondition/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "j71dzpZqLusE ")]
        public async Task<ActionResult<AlertConditionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AlertConditionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alertcondition 
        [HttpPost]
        [FwControllerMethod(Id: "IJ5itow4Sv3Lb")]
        public async Task<ActionResult<AlertConditionLogic>> PostAsync([FromBody]AlertConditionLogic l)
        {
            return await DoPostAsync<AlertConditionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/alertcondition/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "VCZtxLwPN6vgC")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
