using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.HomeControls.GeneralItem;
using WebApi.Modules.Inventory.Asset;
namespace WebApi.Modules.HomeControls.InvoiceItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"5xgHiF8dduf")]
    public class InvoiceItemController : AppDataController
    {
        public InvoiceItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InvoiceItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoiceitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"TJ3DIy4fiP3", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoiceitem/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"d7bAG9slbmL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/invoiceitem 
        [HttpGet]
        [FwControllerMethod(Id:"KU2O49lfu35", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InvoiceItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InvoiceItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/invoiceitem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Z9IbCopBSyk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InvoiceItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InvoiceItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoiceitem 
        [HttpPost]
        [FwControllerMethod(Id:"jkTKYrnoJfL", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InvoiceItemLogic>> NewAsync([FromBody]InvoiceItemLogic l)
        {
            return await DoNewAsync<InvoiceItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/invoiceitem/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "xMlmYQBukzsEN", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InvoiceItemLogic>> EditAsync([FromRoute] string id, [FromBody]InvoiceItemLogic l)
        {
            return await DoEditAsync<InvoiceItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/invoiceitem/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"XMj04F2A6B7", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InvoiceItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoiceitem/validateinventory/browse
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "Wr7NKfUqdUFK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GeneralItemLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoiceitem/validateitem/browse
        [HttpPost("validateitem/browse")]
        [FwControllerMethod(Id: "I220TMDKKp70", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateItemBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ItemLogic>(browseRequest);
        }
    }
}
