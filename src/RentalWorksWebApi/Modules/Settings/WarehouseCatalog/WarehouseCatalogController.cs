using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WarehouseCatalog
{
    [Route("api/v1/[controller]")]
    public class WarehouseCatalogController : AppDataController
    {
        public WarehouseCatalogController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseCatalogLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousecatalog/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WarehouseCatalogLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousecatalog 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseCatalogLogic>(pageno, pagesize, sort, typeof(WarehouseCatalogLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousecatalog/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseCatalogLogic>(id, typeof(WarehouseCatalogLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousecatalog 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WarehouseCatalogLogic l)
        {
            return await DoPostAsync<WarehouseCatalogLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/warehousecatalog/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WarehouseCatalogLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousecatalog/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}