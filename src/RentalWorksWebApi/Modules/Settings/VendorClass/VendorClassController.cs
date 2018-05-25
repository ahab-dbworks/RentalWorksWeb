using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VendorClass
{
    [Route("api/v1/[controller]")]
    public class VendorClassController : AppDataController
    {
        public VendorClassController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorClassLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorclass/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VendorClassLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorclass
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorClassLogic>(pageno, pagesize, sort, typeof(VendorClassLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorclass/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorClassLogic>(id, typeof(VendorClassLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorclass
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VendorClassLogic l)
        {
            return await DoPostAsync<VendorClassLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendorclass/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VendorClassLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorclass/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
    //------------------------------------------------------------------------------------
}
}