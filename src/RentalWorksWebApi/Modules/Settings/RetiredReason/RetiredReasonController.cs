using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.RetiredReason
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class RetiredReasonController : AppDataController
    {
        public RetiredReasonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RetiredReasonLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/retiredreason/browse
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
        // GET api/v1/retiredreason
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RetiredReasonLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RetiredReasonLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/retiredreason/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<RetiredReasonLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RetiredReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/retiredreason
        [HttpPost]
        public async Task<ActionResult<RetiredReasonLogic>> PostAsync([FromBody]RetiredReasonLogic l)
        {
            return await DoPostAsync<RetiredReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/retiredreason/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<RetiredReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}