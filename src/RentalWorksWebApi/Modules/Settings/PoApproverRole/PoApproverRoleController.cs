using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PoApproverRole
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PoApproverRoleController : AppDataController
    {
        public PoApproverRoleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoApproverRoleLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/poapproverrole/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PoApproverRoleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poapproverrole
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoApproverRoleLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoApproverRoleLogic>(pageno, pagesize, sort, typeof(PoApproverRoleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poapproverrole/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<PoApproverRoleLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoApproverRoleLogic>(id, typeof(PoApproverRoleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/poapproverrole
        [HttpPost]
        public async Task<ActionResult<PoApproverRoleLogic>> PostAsync([FromBody]PoApproverRoleLogic l)
        {
            return await DoPostAsync<PoApproverRoleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/poapproverrole/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PoApproverRoleLogic));
        }
        //------------------------------------------------------------------------------------
    }
}