using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.VendorInvoiceNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "8YECGu7qFOty")]
    public class VendorInvoiceNoteController : AppDataController
    {
        public VendorInvoiceNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorInvoiceNoteLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicenote/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "XC4vUa0H2UNq")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicenote/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "RYhk7n1dY0O9u")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoicenote 
        [HttpGet]
        [FwControllerMethod(Id: "ovQFTXKfk6cd")]
        public async Task<ActionResult<IEnumerable<VendorInvoiceNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorInvoiceNoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoicenote/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "v4lLNs6mEV9DQ")]
        public async Task<ActionResult<VendorInvoiceNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorInvoiceNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicenote 
        [HttpPost]
        [FwControllerMethod(Id: "hgxFxI78pIYX")]
        public async Task<ActionResult<VendorInvoiceNoteLogic>> PostAsync([FromBody]VendorInvoiceNoteLogic l)
        {
            return await DoPostAsync<VendorInvoiceNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/vendorinvoicenote/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "iVpANKojZiTxT")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VendorInvoiceNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
