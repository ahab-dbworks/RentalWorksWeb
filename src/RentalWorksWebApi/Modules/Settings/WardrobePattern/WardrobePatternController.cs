using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobePattern
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WardrobePatternController : AppDataController
    {
        public WardrobePatternController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobePatternLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobepattern/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WardrobePatternLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobepattern
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WardrobePatternLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobePatternLogic>(pageno, pagesize, sort, typeof(WardrobePatternLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobepattern/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<WardrobePatternLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobePatternLogic>(id, typeof(WardrobePatternLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobepattern
        [HttpPost]
        public async Task<ActionResult<WardrobePatternLogic>> PostAsync([FromBody]WardrobePatternLogic l)
        {
            return await DoPostAsync<WardrobePatternLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobepattern/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WardrobePatternLogic));
        }
        //------------------------------------------------------------------------------------
    }
}