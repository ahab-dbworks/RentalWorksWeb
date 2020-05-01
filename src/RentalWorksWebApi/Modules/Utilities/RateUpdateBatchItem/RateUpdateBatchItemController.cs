using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.RateUpdateBatchItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "OU9yAKSPdlQyA")]
    public class RateUpdateBatchItemController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public RateUpdateBatchItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RateUpdateBatchItemLogic); }
        //------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdatebatchitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "OuL5tOY2dh4E9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdatebatchitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "OuTmEo7NrUfGk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rateupdatebatchitem 
        [HttpGet]
        [FwControllerMethod(Id: "OuU72rXwVRTfC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<RateUpdateBatchItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RateUpdateBatchItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rateupdatebatchitem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "OVaxoI2jHKits", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<RateUpdateBatchItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RateUpdateBatchItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/rateupdatebatchitem 
        //[HttpPost]
        //[FwControllerMethod(Id: "ovpY4zypBp9uo", ActionType: FwControllerActionTypes.New)]
        //public async Task<ActionResult<RateUpdateBatchItemLogic>> NewAsync([FromBody]RateUpdateBatchItemLogic l)
        //{
        //    return await DoNewAsync<RateUpdateBatchItemLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// PUT api/v1/rateupdatebatchitem/A0000001 
        //[HttpPut("{id}")]
        //[FwControllerMethod(Id: "oWCeu0WNNEoy9", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<RateUpdateBatchItemLogic>> EditAsync([FromRoute] string id, [FromBody]RateUpdateBatchItemLogic l)
        //{
        //    return await DoEditAsync<RateUpdateBatchItemLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/rateupdatebatchitem/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "owthEmvDioXSh", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<RateUpdateBatchItemLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
