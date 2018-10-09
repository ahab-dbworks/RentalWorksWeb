using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleTypeWarehouse
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class VehicleTypeWarehouseController : AppDataController
    {
        public VehicleTypeWarehouseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleTypeWarehouseLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicletypewarehouse/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicletypewarehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleTypeWarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleTypeWarehouseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicletypewarehouse/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleTypeWarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleTypeWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicletypewarehouse
        [HttpPost]
        public async Task<ActionResult<VehicleTypeWarehouseLogic>> PostAsync([FromBody]VehicleTypeWarehouseLogic l)
        {
            return await DoPostAsync<VehicleTypeWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehicletypewarehouse/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}