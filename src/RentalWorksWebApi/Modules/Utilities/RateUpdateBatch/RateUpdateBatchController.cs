using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.RateUpdateBatch
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "nYawyUik0MXW7")]
    public class RateUpdateBatchController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public RateUpdateBatchController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RateUpdateBatchLogic); }
        //------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdatebatch/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "NYdUlMjsSL3wY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdatebatch/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "nypWrI4xPfqjb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rateupdatebatch 
        [HttpGet]
        [FwControllerMethod(Id: "NYRzR5gU26nrk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<RateUpdateBatchLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RateUpdateBatchLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rateupdatebatch/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "nz8idA9tU4N6q", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<RateUpdateBatchLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RateUpdateBatchLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/rateupdatebatch 
        //[HttpPost]
        //[FwControllerMethod(Id: "NZIWFKTGTuQOd", ActionType: FwControllerActionTypes.New)]
        //public async Task<ActionResult<RateUpdateBatchLogic>> NewAsync([FromBody]RateUpdateBatchLogic l)
        //{
        //    return await DoNewAsync<RateUpdateBatchLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// PUT api/v1/rateupdatebatch/A0000001 
        //[HttpPut("{id}")]
        //[FwControllerMethod(Id: "nzIwmvOZ9jZYl", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<RateUpdateBatchLogic>> EditAsync([FromRoute] string id, [FromBody]RateUpdateBatchLogic l)
        //{
        //    return await DoEditAsync<RateUpdateBatchLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/rateupdatebatch/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "nzTDFWwCdsmtP", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<RateUpdateBatchLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
