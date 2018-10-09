using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.ProjectDropShipItems
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class ProjectDropShipItemsController : AppDataController
    {
        public ProjectDropShipItemsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectDropShipItemsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdropshipitems/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ProjectDropShipItemsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdropshipitems 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDropShipItemsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectDropShipItemsLogic>(pageno, pagesize, sort, typeof(ProjectDropShipItemsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectdropshipitems/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDropShipItemsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectDropShipItemsLogic>(id, typeof(ProjectDropShipItemsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectdropshipitems 
        [HttpPost]
        public async Task<ActionResult<ProjectDropShipItemsLogic>> PostAsync([FromBody]ProjectDropShipItemsLogic l)
        {
            return await DoPostAsync<ProjectDropShipItemsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectdropshipitems/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ProjectDropShipItemsLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}