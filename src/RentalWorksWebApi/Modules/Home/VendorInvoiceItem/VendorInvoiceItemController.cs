using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
namespace WebApi.Modules.Home.VendorInvoiceItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class VendorInvoiceItemController : AppDataController
    {
        public VendorInvoiceItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorInvoiceItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceitem/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceitem/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoiceitem 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendorInvoiceItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorInvoiceItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoiceitem/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<VendorInvoiceItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorInvoiceItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceitem 
        [HttpPost]
        public async Task<ActionResult<VendorInvoiceItemLogic>> PostAsync([FromBody]VendorInvoiceItemLogic l)
        {
            return await DoPostAsync<VendorInvoiceItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/vendorinvoiceitem/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VendorInvoiceItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
