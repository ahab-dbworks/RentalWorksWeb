using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeCare
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WardrobeCareController : AppDataController
    {
        public WardrobeCareController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobeCareLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecare/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WardrobeCareLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobecare
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeCareLogic>(pageno, pagesize, sort, typeof(WardrobeCareLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobecare/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeCareLogic>(id, typeof(WardrobeCareLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecare
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WardrobeCareLogic l)
        {
            return await DoPostAsync<WardrobeCareLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobecare/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WardrobeCareLogic));
        }
        //------------------------------------------------------------------------------------
    }
}