using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WarehouseQuikLocateApprover
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"IBGJoUXyFbKmm")]
    public class WarehouseQuikLocateApproverController : AppDataController
    {
        public WarehouseQuikLocateApproverController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseQuikLocateApproverLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousequiklocateapprover/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"cllsTtCe2CrPb")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"7wxS18bU0MWye")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousequiklocateapprover 
        [HttpGet]
        [FwControllerMethod(Id:"YYRdiBNjFhf3I")]
        public async Task<ActionResult<IEnumerable<WarehouseQuikLocateApproverLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseQuikLocateApproverLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehousequiklocateapprover/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"cwMJYkRr6tCwu")]
        public async Task<ActionResult<WarehouseQuikLocateApproverLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseQuikLocateApproverLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehousequiklocateapprover 
        [HttpPost]
        [FwControllerMethod(Id:"LfLXx625H54Fw")]
        public async Task<ActionResult<WarehouseQuikLocateApproverLogic>> PostAsync([FromBody]WarehouseQuikLocateApproverLogic l)
        {
            return await DoPostAsync<WarehouseQuikLocateApproverLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/warehousequiklocateapprover/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"TksmPB0vNJ2kV")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WarehouseQuikLocateApproverLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
