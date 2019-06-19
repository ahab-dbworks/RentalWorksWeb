using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.Purchase
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "bjrFUKcULCmHV")]
    public class PurchaseController : AppDataController
    {
        public PurchaseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchase/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "QXmTkxxmQkbag")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchase/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "hWwTc3pPLaQ2b")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchase 
        [HttpGet]
        [FwControllerMethod(Id: "8ibPKDCKMujEk")]
        public async Task<ActionResult<IEnumerable<PurchaseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PurchaseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchase/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "tddySjH9h3iuM")]
        public async Task<ActionResult<PurchaseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PurchaseLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchase 
        [HttpPost]
        [FwControllerMethod(Id: "4VebdFfpGOdwk")]
        public async Task<ActionResult<PurchaseLogic>> PostAsync([FromBody]PurchaseLogic l)
        {
            return await DoPostAsync<PurchaseLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/purchase/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "DOPJQHEa9a3Ij")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
