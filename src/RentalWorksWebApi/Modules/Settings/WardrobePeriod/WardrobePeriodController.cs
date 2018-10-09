using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobePeriod
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WardrobePeriodController : AppDataController
    {
        public WardrobePeriodController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobePeriodLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobeperiod/browse
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
        // GET api/v1/wardrobeperiod
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WardrobePeriodLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobePeriodLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobeperiod/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<WardrobePeriodLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobePeriodLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobeperiod
        [HttpPost]
        public async Task<ActionResult<WardrobePeriodLogic>> PostAsync([FromBody]WardrobePeriodLogic l)
        {
            return await DoPostAsync<WardrobePeriodLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobeperiod/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}