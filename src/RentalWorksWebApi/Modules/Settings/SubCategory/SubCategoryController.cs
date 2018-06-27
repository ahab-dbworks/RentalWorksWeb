using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SubCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class SubCategoryController : AppDataController
    {
        public SubCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SubCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/subcategory/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SubCategoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/subcategory
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SubCategoryLogic>(pageno, pagesize, sort, typeof(SubCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/subcategory/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SubCategoryLogic>(id, typeof(SubCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/subcategory
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SubCategoryLogic l)
        {
            return await DoPostAsync<SubCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/subcategory/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SubCategoryLogic));
        }
        //------------------------------------------------------------------------------------
    }
}