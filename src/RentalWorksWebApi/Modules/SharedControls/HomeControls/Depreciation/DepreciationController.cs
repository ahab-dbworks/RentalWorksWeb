using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.Depreciation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "Wi9NxgGglKjTN")]
    public class DepreciationController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public DepreciationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DepreciationLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/depreciation/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "M8EG20tPVC8Pz", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Adjustment", RwGlobals.DEPRECIATION_ADJUSTMENT_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/depreciation/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "WIHdxl4vd1Rn0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/depreciation/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "WIJ18TOYYZ7Ld", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/depreciation 
        [HttpGet]
        [FwControllerMethod(Id: "WioAfDlTEInS8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DepreciationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DepreciationLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/depreciation/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "WItkUtRHRwbCB", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<DepreciationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DepreciationLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/depreciation 
        [HttpPost]
        [FwControllerMethod(Id: "wjAk5bC4ZAgxF", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DepreciationLogic>> NewAsync([FromBody]DepreciationLogic l)
        {
            return await DoNewAsync<DepreciationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/depreciation/A0000001 
        // this method is temporary. Can be removed once Mike addresses Issue#3150
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "wjmBdCvjE2rFD", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DepreciationLogic>> EditAsync([FromRoute] string id, [FromBody]DepreciationLogic l)
        {
            return await DoEditAsync<DepreciationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/depreciation/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "WjN3F9KILJIgc", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<DepreciationLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
