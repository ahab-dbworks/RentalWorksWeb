using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.Sound
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class SoundController : AppDataController
    {
        public SoundController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SoundLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/sound/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/sound/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/sound 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoundLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SoundLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/sound/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<SoundLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SoundLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/sound 
        [HttpPost]
        public async Task<ActionResult<SoundLogic>> PostAsync([FromBody]SoundLogic l)
        {
            return await DoPostAsync<SoundLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/sound/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
