using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using FwStandard.Security;
using FwCore.Modules.Administrator.Group;

namespace WebApi.Modules.Administrator.Group
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    public class GroupController : FwGroupController
    {
        public GroupController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FwGroupLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/group/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/group 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FwGroupLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FwGroupLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/group/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<FwGroupLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FwGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/group 
        [HttpPost]
        public async Task<ActionResult<FwGroupLogic>> PostAsync([FromBody]FwGroupLogic l)
        {
            return await DoPostAsync<FwGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/group/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 

        // GET api/v1/group/applicationtree/A0000001 
        [HttpGet("applicationtree/{id}")]
        public async Task<ActionResult<FwSecurityTreeNode>> GetApplicationTree([FromRoute]string id)
        {
            return await DoGetApplicationTree(id);
        }
        //---------------------------------------------------------------------------------------------

    }
}