using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.FacilitySettings.Building
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"h0sTItX8Ofd")]
    public class BuildingController : AppDataController
    {
        public BuildingController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BuildingLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/building/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"DY1lTOcVsL8")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"io67bedqPhu")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/building 
        [HttpGet]
        [FwControllerMethod(Id:"vkxnmFTxmqJ")]
        public async Task<ActionResult<IEnumerable<BuildingLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BuildingLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/building/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"J84gEZ17wKP")]
        public async Task<ActionResult<BuildingLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BuildingLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/building 
        [HttpPost]
        [FwControllerMethod(Id:"wGEk23niGHZ")]
        public async Task<ActionResult<BuildingLogic>> PostAsync([FromBody]BuildingLogic l)
        {
            return await DoPostAsync<BuildingLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/building/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"Qj0xSJYCBpR")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<BuildingLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
