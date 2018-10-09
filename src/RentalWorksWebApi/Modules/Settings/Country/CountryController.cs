using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Country
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class CountryController : AppDataController
    {
        public CountryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CountryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/Country/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
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
        // GET api/v1/Country
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<ActionResult<IEnumerable<CountryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CountryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/Country/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<ActionResult<CountryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CountryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/Country
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<ActionResult<CountryLogic>> PostAsync([FromBody]CountryLogic l)
        {
            return await DoPostAsync<CountryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/Country/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}