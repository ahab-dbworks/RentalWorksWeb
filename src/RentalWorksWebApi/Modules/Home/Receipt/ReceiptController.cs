using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
namespace WebApi.Modules.Home.Receipt
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class ReceiptController : AppDataController
    {
        public ReceiptController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ReceiptLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receipt/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receipt/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/receipt 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ReceiptLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/receipt/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ReceiptLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receipt 
        [HttpPost]
        public async Task<ActionResult<ReceiptLogic>> PostAsync([FromBody]ReceiptLogic l)
        {
            return await DoPostAsync<ReceiptLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/receipt/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
