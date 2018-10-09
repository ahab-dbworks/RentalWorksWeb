using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeColor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WardrobeColorController : AppDataController
    {
        public WardrobeColorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobeColorLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecolor/browse
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
        // GET api/v1/wardrobecolor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WardrobeColorLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeColorLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobecolor/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<WardrobeColorLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeColorLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecolor
        [HttpPost]
        public async Task<ActionResult<WardrobeColorLogic>> PostAsync([FromBody]WardrobeColorLogic l)
        {
            return await DoPostAsync<WardrobeColorLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobecolor/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}