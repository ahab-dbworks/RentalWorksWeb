using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.MarketSegmentJob
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"OWZGrnUnJHon")]
    public class MarketSegmentJobController : AppDataController
    {
        public MarketSegmentJobController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MarketSegmentJobLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/marketsegmentjob/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"rEWtqVOjjHcS", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"KxuBxua4a6NC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/marketsegmentjob 
        [HttpGet]
        [FwControllerMethod(Id:"TRc5xQxmFZPE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<MarketSegmentJobLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MarketSegmentJobLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/marketsegmentjob/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"kRTqC1QAwirr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<MarketSegmentJobLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<MarketSegmentJobLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/marketsegmentjob 
        [HttpPost]
        [FwControllerMethod(Id:"7D2WgzY4hqDe", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<MarketSegmentJobLogic>> NewAsync([FromBody]MarketSegmentJobLogic l)
        {
            return await DoNewAsync<MarketSegmentJobLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/marketsegmentjob/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "peqMbnq0QucGB", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<MarketSegmentJobLogic>> EditAsync([FromRoute] string id, [FromBody]MarketSegmentJobLogic l)
        {
            return await DoEditAsync<MarketSegmentJobLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/marketsegmentjob/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"BWExghGlV9kl", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<MarketSegmentJobLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
