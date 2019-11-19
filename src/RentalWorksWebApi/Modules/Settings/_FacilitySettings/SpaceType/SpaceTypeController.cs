using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.FacilitySettings.SpaceType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"lVjqEX5l2s8ZS")]
    public class SpaceTypeController : AppDataController
    {
        public SpaceTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SpaceTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacetype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"CTeDqQpVjmsg9", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"WBUZ2Cd1e1ryL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacetype 
        [HttpGet]
        [FwControllerMethod(Id:"YtTqGsmAHXcpV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<SpaceTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SpaceTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacetype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"M97cDbyZYJNFO", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<SpaceTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SpaceTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacetype 
        [HttpPost]
        [FwControllerMethod(Id:"PyC1VeiUw9bEo", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<SpaceTypeLogic>> NewAsync([FromBody]SpaceTypeLogic l)
        {
            return await DoNewAsync<SpaceTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/spacetype/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "FfL3XRYhe3yzs", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<SpaceTypeLogic>> EditAsync([FromRoute] string id, [FromBody]SpaceTypeLogic l)
        {
            return await DoEditAsync<SpaceTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/spacetype/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"JUMhAJCu2y14x", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SpaceTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
