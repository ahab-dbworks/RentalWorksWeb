using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WarehouseDepartment
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WarehouseDepartmentController : AppDataController
    {
        public WarehouseDepartmentController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseDepartmentLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousedepartment/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WarehouseDepartmentLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousedepartment 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseDepartmentLogic>(pageno, pagesize, sort, typeof(WarehouseDepartmentLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousedepartment/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseDepartmentLogic>(id, typeof(WarehouseDepartmentLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousedepartment 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WarehouseDepartmentLogic l)
        {
            return await DoPostAsync<WarehouseDepartmentLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/warehousedepartment/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(WarehouseDepartmentLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/warehousedepartment/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}