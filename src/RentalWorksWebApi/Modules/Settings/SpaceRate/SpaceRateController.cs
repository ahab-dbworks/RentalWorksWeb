using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.SpaceRate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"iWPadFxStXkcL")]
    public class SpaceRateController : AppDataController
    {
        public SpaceRateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SpaceRateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacerate/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"0EOGFrLy6vi5N")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"Oga9rGCvS0nt4")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacerate 
        [HttpGet]
        [FwControllerMethod(Id:"T7MRrCQyN4zgk")]
        public async Task<ActionResult<IEnumerable<SpaceRateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SpaceRateLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacerate/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"wElfQiAWoeT1z")]
        public async Task<ActionResult<SpaceRateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SpaceRateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacerate 
        [HttpPost]
        [FwControllerMethod(Id:"FfZEBRuz7kJHV")]
        public async Task<ActionResult<SpaceRateLogic>> PostAsync([FromBody]SpaceRateLogic l)
        {
            return await DoPostAsync<SpaceRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/spacerate/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"88ZmgTYFIsHXo")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SpaceRateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
