using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PartsCategory
{
    [Route("api/v1/[controller]")]
    public class PartsCategoryController : AppDataController
    {
        public PartsCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PartsCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/partscategory/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PartsCategoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/partscategory
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PartsCategoryLogic>(pageno, pagesize, sort, typeof(PartsCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/partscategory/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<PartsCategoryLogic>(id, typeof(PartsCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/partscategory
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PartsCategoryLogic l)
        {
            return await DoPostAsync<PartsCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/partscategory/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PartsCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/partscategory/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}