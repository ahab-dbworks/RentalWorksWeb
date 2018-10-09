using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.GlAccount
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class GlAccountController : AppDataController
    {
        public GlAccountController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GlAccountLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/glaccount/browse
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
        // GET api/v1/glaccount
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GlAccountLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GlAccountLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/glaccount/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<GlAccountLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GlAccountLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/glaccount
        [HttpPost]
        public async Task<ActionResult<GlAccountLogic>> PostAsync([FromBody]GlAccountLogic l)
        {
            return await DoPostAsync<GlAccountLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/glaccount/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}