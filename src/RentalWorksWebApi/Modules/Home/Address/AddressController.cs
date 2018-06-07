using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.Address
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class AddressController : AppDataController
    {
        public AddressController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AddressLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/address/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(AddressLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/address 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AddressLogic>(pageno, pagesize, sort, typeof(AddressLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/address/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<AddressLogic>(id, typeof(AddressLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/address 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]AddressLogic l)
        {
            return await DoPostAsync<AddressLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/address/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(AddressLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/address/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
