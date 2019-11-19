using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectDrawings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"e0Ylzlhkp2wY0")]
    public class ProjectDrawingsController : AppDataController
    {
        public ProjectDrawingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectDrawingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdrawings/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"9hmLJqn8x1odt", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"8ysUeLVigeJVD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdrawings 
        [HttpGet]
        [FwControllerMethod(Id:"uj1pmrGAcYNp0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ProjectDrawingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectDrawingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdrawings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"yfGcARjmcXmXS", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ProjectDrawingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectDrawingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdrawings 
        [HttpPost]
        [FwControllerMethod(Id:"HaN7rwYrdSLYA", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ProjectDrawingsLogic>> NewAsync([FromBody]ProjectDrawingsLogic l)
        {
            return await DoNewAsync<ProjectDrawingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/projectdrawings/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "235tvINWuy6O7", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ProjectDrawingsLogic>> EditAsync([FromRoute] string id, [FromBody]ProjectDrawingsLogic l)
        {
            return await DoEditAsync<ProjectDrawingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectdrawings/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"8uzEz1R5dYXac", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ProjectDrawingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
