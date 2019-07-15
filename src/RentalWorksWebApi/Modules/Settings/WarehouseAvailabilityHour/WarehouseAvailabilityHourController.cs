using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WarehouseAvailabilityHour
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"1iBtCdzhTkio4")]
    public class WarehouseAvailabilityHourController : AppDataController
    {
        public WarehouseAvailabilityHourController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseAvailabilityHourLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouseavailabilityhour/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"dbuIXnlP1OyzU")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"mFouGMvy6bf5Q")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehouseavailabilityhour 
        [HttpGet]
        [FwControllerMethod(Id:"he4HIae0QmxaX")]
        public async Task<ActionResult<IEnumerable<WarehouseAvailabilityHourLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseAvailabilityHourLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehouseavailabilityhour/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"GkGENyJt6vGxe")]
        public async Task<ActionResult<WarehouseAvailabilityHourLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseAvailabilityHourLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouseavailabilityhour 
        [HttpPost]
        [FwControllerMethod(Id:"gDVOqEX1dCjCz")]
        public async Task<ActionResult<WarehouseAvailabilityHourLogic>> PostAsync([FromBody]WarehouseAvailabilityHourLogic l)
        {
            return await DoPostAsync<WarehouseAvailabilityHourLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/warehouseavailabilityhour/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"274xAgVHBrPtX")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WarehouseAvailabilityHourLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
