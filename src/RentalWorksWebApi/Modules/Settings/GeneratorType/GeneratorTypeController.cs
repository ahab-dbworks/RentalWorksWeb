using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.GeneratorType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class GeneratorTypeController : AppDataController
    {
        public GeneratorTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneratorTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(GeneratorTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatortype
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorTypeLogic>(pageno, pagesize, sort, typeof(GeneratorTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatortype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorTypeLogic>(id, typeof(GeneratorTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]GeneratorTypeLogic l)
        {
            return await DoPostAsync<GeneratorTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatortype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(GeneratorTypeLogic));
        }
        //------------------------------------------------------------------------------------
    }
}