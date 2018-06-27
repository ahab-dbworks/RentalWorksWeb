using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Administrator.Control
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    public class ControlController : AppDataController
    {
        public ControlController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ControlLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/control/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ControlLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/control 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ControlLogic>(pageno, pagesize, sort, typeof(ControlLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/control/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ControlLogic>(id, typeof(ControlLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/control 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ControlLogic l)
        {
            return await DoPostAsync<ControlLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/control/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ControlLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}