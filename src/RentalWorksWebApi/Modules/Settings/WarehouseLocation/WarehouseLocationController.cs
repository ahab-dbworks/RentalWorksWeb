using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WarehouseLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WarehouseLocationController : AppDataController
    {
        public WarehouseLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseLocationLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouselocation/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WarehouseLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehouselocation 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseLocationLogic>(pageno, pagesize, sort, typeof(WarehouseLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehouselocation/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseLocationLogic>(id, typeof(WarehouseLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouselocation 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WarehouseLocationLogic l)
        {
            return await DoPostAsync<WarehouseLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/warehouselocation/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WarehouseLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}