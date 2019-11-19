using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.ProjectSettings.ProjectAsBuild
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"CiTY0pLnyroMa")]
    public class ProjectAsBuildController : AppDataController
    {
        public ProjectAsBuildController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectAsBuildLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectasbuild/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"QSfQBJ4CmTQNT")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"a2FYmTzUZbQnd")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectasbuild 
        [HttpGet]
        [FwControllerMethod(Id:"nYNarYMBZAAb0")]
        public async Task<ActionResult<IEnumerable<ProjectAsBuildLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectAsBuildLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectasbuild/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"mDW7AdooZZWTj")]
        public async Task<ActionResult<ProjectAsBuildLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectAsBuildLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectasbuild 
        [HttpPost]
        [FwControllerMethod(Id:"vrA7eTko8oJs8")]
        public async Task<ActionResult<ProjectAsBuildLogic>> PostAsync([FromBody]ProjectAsBuildLogic l)
        {
            return await DoPostAsync<ProjectAsBuildLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectasbuild/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"EvBV2PpuYCwbn")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ProjectAsBuildLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
