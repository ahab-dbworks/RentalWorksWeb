using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.OrganizationType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class OrganizationTypeController : AppDataController
    {
        public OrganizationTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrganizationTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/organizationtype/browse
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
        // GET api/v1/organizationtype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationTypeLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<OrganizationTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/organizationtype/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationTypeLogic>> GetAsync(string id)
        {
            return await DoGetAsync<OrganizationTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/organizationtype
        [HttpPost]
        public async Task<ActionResult<OrganizationTypeLogic>> PostAsync([FromBody]OrganizationTypeLogic l)
        {
            return await DoPostAsync<OrganizationTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/organizationtype/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}