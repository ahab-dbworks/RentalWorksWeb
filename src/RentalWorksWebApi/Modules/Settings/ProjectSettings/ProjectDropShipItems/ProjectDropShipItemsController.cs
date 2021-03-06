using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectDropShipItems
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"XzUwxCWh64FDw")]
    public class ProjectDropShipItemsController : AppDataController
    {
        public ProjectDropShipItemsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectDropShipItemsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdropshipitems/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"bj5zorxSUs4m4", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"JlyjAxKr8otNJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdropshipitems 
        [HttpGet]
        [FwControllerMethod(Id:"AHyS98ZqsnYxT", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ProjectDropShipItemsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectDropShipItemsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdropshipitems/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"jVopV1lXdCOYG", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ProjectDropShipItemsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectDropShipItemsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdropshipitems 
        [HttpPost]
        [FwControllerMethod(Id:"VA7QK3to62gfK", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ProjectDropShipItemsLogic>> NewAsync([FromBody]ProjectDropShipItemsLogic l)
        {
            return await DoNewAsync<ProjectDropShipItemsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/projectdropshipitems/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "lwOHShXiUZDeL", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ProjectDropShipItemsLogic>> EditAsync([FromRoute] string id, [FromBody]ProjectDropShipItemsLogic l)
        {
            return await DoEditAsync<ProjectDropShipItemsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectdropshipitems/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"DTCFk58g3G2Lv", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ProjectDropShipItemsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
