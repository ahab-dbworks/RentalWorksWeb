using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.InvoiceCreationBatch
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"mWgRguSHvPhO")]
    public class InvoiceCreationBatchController : AppDataController
    {
        public InvoiceCreationBatchController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InvoiceCreationBatchLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicecreationbatch/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"Do7uXnPrzeHj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicecreationbatch/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"mxQI4tBvUnx7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/invoicecreationbatch 
        [HttpGet]
        [FwControllerMethod(Id:"ZysxnX4eSWgq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InvoiceCreationBatchLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InvoiceCreationBatchLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/invoicecreationbatch/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"QWt1C3MeHX4Z", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InvoiceCreationBatchLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InvoiceCreationBatchLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
