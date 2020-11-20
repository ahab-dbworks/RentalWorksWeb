using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using WebApi.Modules.Utilities.GLDistribution;

namespace WebApi.Modules.Inventory.Retired
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "I9OA43GGHPNFf")]
    public class RetiredController : AppDataController
    {
        public RetiredController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RetiredLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/retired/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "IAJakjbK04PHQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/retired/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "ibDi8oSWvmOXb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/retired 
        [HttpGet]
        [FwControllerMethod(Id: "IBJi23JaPug2X", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<RetiredLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RetiredLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/retired/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "iCI01PTIilYmw", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<RetiredLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RetiredLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/retired 
        //[HttpPost]
        //[FwControllerMethod(Id: "ICjk6uImSscUK", ActionType: FwControllerActionTypes.New)]
        //public async Task<ActionResult<RetiredLogic>> NewAsync([FromBody]RetiredLogic l)
        //{
        //    return await DoNewAsync<RetiredLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// PUT api/v1/retired/A0000001 
        //[HttpPut("{id}")]
        //[FwControllerMethod(Id: "IcTJYDlKIrDK6", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<RetiredLogic>> EditAsync([FromRoute] string id, [FromBody]RetiredLogic l)
        //{
        //    return await DoEditAsync<RetiredLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/retired/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "IcyBZnWQlffM4", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<RetiredLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/retired/gldistribution/browse 
        [HttpPost("gldistribution/browse")]
        [FwControllerMethod(Id: "hOSpd7SYLZgmj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> GLDistribution_BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GLDistributionLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
