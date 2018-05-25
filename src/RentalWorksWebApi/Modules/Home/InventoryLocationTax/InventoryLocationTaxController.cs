using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventoryLocationTax
{
    [Route("api/v1/[controller]")]
    public class InventoryLocationTaxController : AppDataController
    {
        public InventoryLocationTaxController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryLocationTaxLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorylocationtax/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorylocationtax 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryLocationTaxLogic>(pageno, pagesize, sort, typeof(InventoryLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorylocationtax/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryLocationTaxLogic>(id, typeof(InventoryLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorylocationtax 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryLocationTaxLogic l)
        {
            return await DoPostAsync<InventoryLocationTaxLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorylocationtax/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryLocationTaxLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorylocationtax/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}