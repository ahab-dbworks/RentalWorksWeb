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
    public class SpaceRateController : AppDataController
    {
        public SpaceRateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SpaceRateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacerate/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SpaceRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacerate 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpaceRateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SpaceRateLogic>(pageno, pagesize, sort, typeof(SpaceRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/spacerate/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<SpaceRateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SpaceRateLogic>(id, typeof(SpaceRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/spacerate 
        [HttpPost]
        public async Task<ActionResult<SpaceRateLogic>> PostAsync([FromBody]SpaceRateLogic l)
        {
            return await DoPostAsync<SpaceRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/spacerate/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SpaceRateLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}