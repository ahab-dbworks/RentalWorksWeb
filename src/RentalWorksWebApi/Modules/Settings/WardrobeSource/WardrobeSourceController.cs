using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeSource
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WardrobeSourceController : AppDataController
    {
        public WardrobeSourceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobeSourceLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobesource/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WardrobeSourceLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobesource
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WardrobeSourceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeSourceLogic>(pageno, pagesize, sort, typeof(WardrobeSourceLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobesource/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<WardrobeSourceLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeSourceLogic>(id, typeof(WardrobeSourceLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobesource
        [HttpPost]
        public async Task<ActionResult<WardrobeSourceLogic>> PostAsync([FromBody]WardrobeSourceLogic l)
        {
            return await DoPostAsync<WardrobeSourceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobesource/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WardrobeSourceLogic));
        }
        //------------------------------------------------------------------------------------
    }
}