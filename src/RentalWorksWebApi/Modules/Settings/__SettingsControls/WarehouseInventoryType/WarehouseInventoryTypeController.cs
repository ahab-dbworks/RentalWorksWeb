using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WarehouseInventoryType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"HRLS0W2gCu4lD")]
    public class WarehouseInventoryTypeController : AppDataController
    {
        public WarehouseInventoryTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseInventoryTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouseinventorytype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"45zLou5VVnere")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"utr3t5NFqff8e")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehouseinventorytype 
        [HttpGet]
        [FwControllerMethod(Id:"iK8RNHKzSHFNA")]
        public async Task<ActionResult<IEnumerable<WarehouseInventoryTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseInventoryTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/warehouseinventorytype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"jTQXyI8MEuCot")]
        public async Task<ActionResult<WarehouseInventoryTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseInventoryTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouseinventorytype 
        [HttpPost]
        [FwControllerMethod(Id:"6AvLVvpgdeaf5")]
        public async Task<ActionResult<WarehouseInventoryTypeLogic>> PostAsync([FromBody]WarehouseInventoryTypeLogic l)
        {
            return await DoPostAsync<WarehouseInventoryTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/warehouseinventorytype/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"5UxolgKDk6")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <WarehouseInventoryTypeLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
