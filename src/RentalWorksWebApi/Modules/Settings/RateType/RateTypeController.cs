using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.RateType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class RateTypeController : AppDataController
    {
        public RateTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RateTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ratetype/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ratetype 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RateTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RateTypeLogic>(pageno, pagesize, sort, typeof(RateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ratetype/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<RateTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RateTypeLogic>(id, typeof(RateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}