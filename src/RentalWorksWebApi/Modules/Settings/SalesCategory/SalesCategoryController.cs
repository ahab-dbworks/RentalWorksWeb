using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SalesCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class SalesCategoryController : AppDataController
    {
        public SalesCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SalesCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SalesCategoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/salescategory
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SalesCategoryLogic>(pageno, pagesize, sort, typeof(SalesCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/salescategory/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<SalesCategoryLogic>(id, typeof(SalesCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SalesCategoryLogic l)
        {
            return await DoPostAsync<SalesCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/salescategory/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SalesCategoryLogic));
        }
        //------------------------------------------------------------------------------------
    }
}