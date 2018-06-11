using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.GeneratorWatts
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class GeneratorWattsController : AppDataController
    {
        public GeneratorWattsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneratorWattsLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorwatts/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(GeneratorWattsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorwatts
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<GeneratorWattsLogic>(pageno, pagesize, sort, typeof(GeneratorWattsLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorwatts/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<GeneratorWattsLogic>(id, typeof(GeneratorWattsLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorwatts
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<IActionResult> PostAsync([FromBody]GeneratorWattsLogic l)
        {
            return await DoPostAsync<GeneratorWattsLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatorwatts/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(GeneratorWattsLogic));
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}