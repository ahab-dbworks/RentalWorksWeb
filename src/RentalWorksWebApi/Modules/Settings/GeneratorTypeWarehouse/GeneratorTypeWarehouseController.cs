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
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(GeneratorTypeWarehouseLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatortypewarehouse
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorTypeWarehouseLogic>(pageno, pagesize, sort, typeof(GeneratorTypeWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatortypewarehouse/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorTypeWarehouseLogic>(id, typeof(GeneratorTypeWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortypewarehouse
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]GeneratorTypeWarehouseLogic l)
        {
            return await DoPostAsync<GeneratorTypeWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatortypewarehouse/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(GeneratorTypeWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
    }
}