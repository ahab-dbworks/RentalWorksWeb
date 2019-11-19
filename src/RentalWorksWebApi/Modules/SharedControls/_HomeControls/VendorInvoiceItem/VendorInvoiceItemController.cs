using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;

namespace WebApi.Modules.HomeControls.VendorInvoiceItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "mEYOByOhi5yT0")]
    public class VendorInvoiceItemController : AppDataController
    {
        public VendorInvoiceItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorInvoiceItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "ZFg9ekg2gKvkJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceitem/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "DjJV6kvdepQ2B", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoiceitem 
        [HttpGet]
        [FwControllerMethod(Id: "07TLLOuXA0VCj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VendorInvoiceItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorInvoiceItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoiceitem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "em4COhFlmYehk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<VendorInvoiceItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorInvoiceItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceitem 
        [HttpPost]
        [FwControllerMethod(Id: "ntehzJyKlGeV1", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VendorInvoiceItemLogic>> NewAsync([FromBody]VendorInvoiceItemLogic l)
        {
            return await DoNewAsync<VendorInvoiceItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/vendorinvoiceitem/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "o5t7YcsODx3cC", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VendorInvoiceItemLogic>> EditAsync([FromRoute] string id, [FromBody]VendorInvoiceItemLogic l)
        {
            return await DoEditAsync<VendorInvoiceItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/vendorinvoiceitem/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "YpEQmv9EtEgoO", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VendorInvoiceItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
