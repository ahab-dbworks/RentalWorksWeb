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
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RateWarehouseLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/ratewarehouse
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RateWarehouseLogic>(pageno, pagesize, sort, typeof(RateWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/ratewarehouse/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<RateWarehouseLogic>(id, typeof(RateWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/ratewarehouse
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RateWarehouseLogic l)
        {
            return await DoPostAsync<RateWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/ratewarehouse/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RateWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
    }
}