using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.VendorInvoiceNote
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
        [FwControllerMethod(Id: "XC4vUa0H2UNq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicenote/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "RYhk7n1dY0O9u", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoicenote 
        [HttpGet]
        [FwControllerMethod(Id: "ovQFTXKfk6cd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VendorInvoiceNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorInvoiceNoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoicenote/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "v4lLNs6mEV9DQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<VendorInvoiceNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorInvoiceNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicenote 
        [HttpPost]
        [FwControllerMethod(Id: "hgxFxI78pIYX", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VendorInvoiceNoteLogic>> NewAsync([FromBody]VendorInvoiceNoteLogic l)
        {
            return await DoNewAsync<VendorInvoiceNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/vendorinvoicenote/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "bqesPdeyTKSiv", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VendorInvoiceNoteLogic>> EditAsync([FromRoute] string id, [FromBody]VendorInvoiceNoteLogic l)
        {
            return await DoEditAsync<VendorInvoiceNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/vendorinvoicenote/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "iVpANKojZiTxT", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VendorInvoiceNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
