using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.RateWarehouse
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class RateWarehouseController : AppDataController
    {
        public RateWarehouseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RateWarehouseLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/ratewarehouse/browse
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
        // GET api/v1/ratewarehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RateWarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RateWarehouseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/ratewarehouse/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<RateWarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RateWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/ratewarehouse
        [HttpPost]
        public async Task<ActionResult<RateWarehouseLogic>> PostAsync([FromBody]RateWarehouseLogic l)
        {
            return await DoPostAsync<RateWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/ratewarehouse/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}