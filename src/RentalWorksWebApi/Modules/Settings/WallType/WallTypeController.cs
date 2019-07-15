using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WallType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"V45pfjoiW04Ix")]
    public class WallTypeController : AppDataController
    {
        public WallTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WallTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/walltype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"VVghKpEn1heH1")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"QaK86vr9wSFO2")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/walltype 
        [HttpGet]
        [FwControllerMethod(Id:"R7W5StVzVTUZ8")]
        public async Task<ActionResult<IEnumerable<WallTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WallTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/walltype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"8lfvrLrfCO3eo")]
        public async Task<ActionResult<WallTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WallTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/walltype 
        [HttpPost]
        [FwControllerMethod(Id:"Y7VWqiiphTIpr")]
        public async Task<ActionResult<WallTypeLogic>> PostAsync([FromBody]WallTypeLogic l)
        {
            return await DoPostAsync<WallTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/walltype/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"l5FkQLYTR9fqG")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WallTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
