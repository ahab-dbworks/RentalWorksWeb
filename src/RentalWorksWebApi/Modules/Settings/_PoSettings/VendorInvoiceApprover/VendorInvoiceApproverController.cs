using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.VendorInvoiceApprover
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"3Hhg9Bl5Rm1mT")]
    public class VendorInvoiceApproverController : AppDataController
    {
        public VendorInvoiceApproverController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorInvoiceApproverLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceapprover/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ZfU3ujQgYN8CK")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"1ba1KbE4fTWjc")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoiceapprover 
        [HttpGet]
        [FwControllerMethod(Id:"PMkRwT0lIGIGp")]
        public async Task<ActionResult<IEnumerable<VendorInvoiceApproverLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorInvoiceApproverLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoiceapprover/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Iti5XAiUb0mGN")]
        public async Task<ActionResult<VendorInvoiceApproverLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorInvoiceApproverLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceapprover 
        [HttpPost]
        [FwControllerMethod(Id:"B1rBGrm0H5Ny7")]
        public async Task<ActionResult<VendorInvoiceApproverLogic>> PostAsync([FromBody]VendorInvoiceApproverLogic l)
        {
            return await DoPostAsync<VendorInvoiceApproverLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/vendorinvoiceapprover/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"IrjXa5WBYpIQf")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VendorInvoiceApproverLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
