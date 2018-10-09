using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.Region
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class RegionController : AppDataController
    {
        public RegionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RegionLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/region/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RegionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/region
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<RegionLogic>(pageno, pagesize, sort, typeof(RegionLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/region/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<RegionLogic>> GetAsync(string id)
        {
            return await DoGetAsync<RegionLogic>(id, typeof(RegionLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/region
        [HttpPost]
        public async Task<ActionResult<RegionLogic>> PostAsync([FromBody]RegionLogic l)
        {
            return await DoPostAsync<RegionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/region/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(RegionLogic));
        }
        //------------------------------------------------------------------------------------
    }
}