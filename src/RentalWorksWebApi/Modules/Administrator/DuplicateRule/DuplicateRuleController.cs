using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using FwStandard.Modules.Administrator.DuplicateRule;

namespace WebApi.Modules.Administrator.DuplicateRule
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id:"v7oBspDLjli8")]
    public class DuplicateRuleController : AppDataController
    {
        public DuplicateRuleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DuplicateRuleLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/duplicaterule/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"TNoZJqqWtCtX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"6pO4GGkY7kq5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/duplicaterule 
        [HttpGet]
        [FwControllerMethod(Id:"kbxYnSe1o9a6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DuplicateRuleLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DuplicateRuleLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/duplicaterule/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"DGrrSsT7QH96", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<DuplicateRuleLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DuplicateRuleLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/duplicaterule 
        [HttpPost]
        [FwControllerMethod(Id:"9r5DmjZNOwQK", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DuplicateRuleLogic>> NewAsync([FromBody]DuplicateRuleLogic l)
        {
            return await DoNewAsync<DuplicateRuleLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/duplicaterule/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "LKEFEoPvAIS4E", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DuplicateRuleLogic>> EditAsync([FromRoute] string id, [FromBody]DuplicateRuleLogic l)
        {
            return await DoEditAsync<DuplicateRuleLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/duplicaterule/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"odX2t317pvT1", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DuplicateRuleLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
