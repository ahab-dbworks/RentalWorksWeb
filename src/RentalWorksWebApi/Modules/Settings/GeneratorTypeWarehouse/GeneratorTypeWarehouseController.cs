using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.GeneratorTypeWarehouse
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class GeneratorTypeWarehouseController : AppDataController
    {
        public GeneratorTypeWarehouseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneratorTypeWarehouseLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortypewarehouse/browse
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
        // GET api/v1/generatortypewarehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneratorTypeWarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorTypeWarehouseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatortypewarehouse/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneratorTypeWarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorTypeWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortypewarehouse
        [HttpPost]
        public async Task<ActionResult<GeneratorTypeWarehouseLogic>> PostAsync([FromBody]GeneratorTypeWarehouseLogic l)
        {
            return await DoPostAsync<GeneratorTypeWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatortypewarehouse/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}