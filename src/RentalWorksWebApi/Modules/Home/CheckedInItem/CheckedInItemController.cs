using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.CheckedInItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class CheckedInItemController : AppDataController
    {
        public CheckedInItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CheckedInItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/stageditem/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/stageditem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/stageditem 
        //[HttpGet]
        //public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<CheckedInItemLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/stageditem/A0000001 
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<CheckedInItemLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/stageditem 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]CheckedInItemLogic l)
        //{
        //    return await DoPostAsync<CheckedInItemLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/stageditem/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
