using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.SetSettings.WallDescription
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"0uJgpWp1Mj9Jd")]
    public class WallDescriptionController : AppDataController
    {
        public WallDescriptionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WallDescriptionLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/walldescription/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"8luob28nsAQby", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"cuPjFC9yvmS4I", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/walldescription 
        [HttpGet]
        [FwControllerMethod(Id:"fIJobTCyY83aE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WallDescriptionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WallDescriptionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/walldescription/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"dmlLRiG5C3tP2", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<WallDescriptionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WallDescriptionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/walldescription 
        [HttpPost]
        [FwControllerMethod(Id:"5pU4Veg8Ht1Km", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WallDescriptionLogic>> NewAsync([FromBody]WallDescriptionLogic l)
        {
            return await DoNewAsync<WallDescriptionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/walldescription/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "OOhUonuZGEBBx", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WallDescriptionLogic>> EditAsync([FromRoute] string id, [FromBody]WallDescriptionLogic l)
        {
            return await DoEditAsync<WallDescriptionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/walldescription/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"6PSQBs4UtuSnm", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WallDescriptionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
