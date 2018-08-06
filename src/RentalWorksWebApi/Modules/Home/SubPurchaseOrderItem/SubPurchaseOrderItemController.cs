using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.SubPurchaseOrderItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class SubPurchaseOrderItemController : AppDataController
    {
        public SubPurchaseOrderItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SubPurchaseOrderItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subpurchaseorderitem/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subpurchaseorderitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/subpurchaseorderitem 
        //[HttpGet]
        //public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<SubPurchaseOrderItemLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        // GET api/v1/subpurchaseorderitem/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SubPurchaseOrderItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/subpurchaseorderitem 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]SubPurchaseOrderItemLogic l)
        {
            return await DoPostAsync<SubPurchaseOrderItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/subpurchaseorderitem/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
