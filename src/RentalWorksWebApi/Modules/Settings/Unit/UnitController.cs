using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Unit
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class UnitController : AppDataController
    {
        public UnitController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UnitLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/unit/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(UnitLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/unit
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UnitLogic>(pageno, pagesize, sort, typeof(UnitLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/unit/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UnitLogic>(id, typeof(UnitLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/unit
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]UnitLogic l)
        {
            return await DoPostAsync<UnitLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/unit/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(UnitLogic));
        }
        //------------------------------------------------------------------------------------
    }
}