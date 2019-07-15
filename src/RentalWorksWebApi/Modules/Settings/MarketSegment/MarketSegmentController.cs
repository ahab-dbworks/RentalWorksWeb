using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.MarketSegment
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"NPu4Lci1ndrl")]
    public class MarketSegmentController : AppDataController
    {
        public MarketSegmentController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MarketSegmentLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/marketsegment/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"6JUDhVmAD5qr")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"e6BDz9zBMbpP")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/marketsegment 
        [HttpGet]
        [FwControllerMethod(Id:"ikTkl1wjmouf")]
        public async Task<ActionResult<IEnumerable<MarketSegmentLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MarketSegmentLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/marketsegment/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"H2Kd1WJdxGp3")]
        public async Task<ActionResult<MarketSegmentLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<MarketSegmentLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/marketsegment 
        [HttpPost]
        [FwControllerMethod(Id:"2u71tDgPAQQ0")]
        public async Task<ActionResult<MarketSegmentLogic>> PostAsync([FromBody]MarketSegmentLogic l)
        {
            return await DoPostAsync<MarketSegmentLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/marketsegment/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"TOjDvk0ZFEDW")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<MarketSegmentLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
