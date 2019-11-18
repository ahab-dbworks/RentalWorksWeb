using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WarehouseLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"B1kMAlpwQNPLG")]
    public class WarehouseLocationController : AppDataController
    {
        public WarehouseLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseLocationLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouselocation/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"xSPKAIrjanNeQ")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"D1UIyYMSVtMM0")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehouselocation 
        [HttpGet]
        [FwControllerMethod(Id:"a435jjYplY6qa")]
        public async Task<ActionResult<IEnumerable<WarehouseLocationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseLocationLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehouselocation/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"TFN4jBbpzlf7Q")]
        public async Task<ActionResult<WarehouseLocationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouselocation 
        [HttpPost]
        [FwControllerMethod(Id:"r0xCAWQIwfFGO")]
        public async Task<ActionResult<WarehouseLocationLogic>> PostAsync([FromBody]WarehouseLocationLogic l)
        {
            return await DoPostAsync<WarehouseLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/warehouselocation/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"7QR1igUbEX88B")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WarehouseLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
