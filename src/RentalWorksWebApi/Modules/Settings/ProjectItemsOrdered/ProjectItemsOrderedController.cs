using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.ProjectItemsOrdered
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"oB5CeVYRCU1EG")]
    public class ProjectItemsOrderedController : AppDataController
    {
        public ProjectItemsOrderedController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectItemsOrderedLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectitemsordered/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"gTJTVPbtbH2dE")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"PHkGCr5DiUCxU")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectitemsordered 
        [HttpGet]
        [FwControllerMethod(Id:"mgxlLUiiF74pp")]
        public async Task<ActionResult<IEnumerable<ProjectItemsOrderedLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectItemsOrderedLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectitemsordered/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"9nq9kNphPPK2O")]
        public async Task<ActionResult<ProjectItemsOrderedLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectItemsOrderedLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectitemsordered 
        [HttpPost]
        [FwControllerMethod(Id:"wUoa0YgnubByc")]
        public async Task<ActionResult<ProjectItemsOrderedLogic>> PostAsync([FromBody]ProjectItemsOrderedLogic l)
        {
            return await DoPostAsync<ProjectItemsOrderedLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectitemsordered/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"jYcF1q8Hial5P")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ProjectItemsOrderedLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
