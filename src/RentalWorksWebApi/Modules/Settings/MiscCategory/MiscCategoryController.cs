using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.MiscCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class MiscCategoryController : AppDataController
    {
        public MiscCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MiscCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/misccategory/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(MiscCategoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misccategory
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MiscCategoryLogic>(pageno, pagesize, sort, typeof(MiscCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misccategory/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<MiscCategoryLogic>(id, typeof(MiscCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/misccategory
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MiscCategoryLogic l)
        {
            return await DoPostAsync<MiscCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/misccategory/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(MiscCategoryLogic));
        }
        //------------------------------------------------------------------------------------
    }
}