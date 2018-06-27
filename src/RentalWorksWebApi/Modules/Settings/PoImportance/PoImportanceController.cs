using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PoImportance
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PoImportanceController : AppDataController
    {
        public PoImportanceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoImportanceLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/poimportance/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PoImportanceLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poimportance
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoImportanceLogic>(pageno, pagesize, sort, typeof(PoImportanceLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poimportance/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoImportanceLogic>(id, typeof(PoImportanceLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/poimportance
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PoImportanceLogic l)
        {
            return await DoPostAsync<PoImportanceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/poimportance/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PoImportanceLogic));
        }
        //------------------------------------------------------------------------------------
    }
}