using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.LaborType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class LaborTypeController : AppDataController
    {
        public LaborTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(LaborTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/labortype/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(LaborTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/labortype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LaborTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<LaborTypeLogic>(pageno, pagesize, sort, typeof(LaborTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/labortype/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<LaborTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<LaborTypeLogic>(id, typeof(LaborTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/labortype
        [HttpPost]
        public async Task<ActionResult<LaborTypeLogic>> PostAsync([FromBody]LaborTypeLogic l)
        {
            return await DoPostAsync<LaborTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/labortype/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(LaborTypeLogic));
        }
        //------------------------------------------------------------------------------------
    }
}