using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventorySubstitute
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class InventorySubstituteController : AppDataController
    {
        public InventorySubstituteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventorySubstituteLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysubstitute/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventorySubstituteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorysubstitute 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventorySubstituteLogic>(pageno, pagesize, sort, typeof(InventorySubstituteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorysubstitute/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventorySubstituteLogic>(id, typeof(InventorySubstituteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysubstitute 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventorySubstituteLogic l)
        {
            return await DoPostAsync<InventorySubstituteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorysubstitute/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventorySubstituteLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}