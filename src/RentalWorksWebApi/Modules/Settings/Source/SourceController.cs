using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Source
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class SourceController : AppDataController
    {
        public SourceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SourceLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/source/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SourceLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/source
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SourceLogic>(pageno, pagesize, sort, typeof(SourceLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/source/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<SourceLogic>(id, typeof(SourceLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/source
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SourceLogic l)
        {
            return await DoPostAsync<SourceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/source/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SourceLogic));
        }
        //------------------------------------------------------------------------------------
    }
}