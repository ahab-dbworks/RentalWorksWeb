using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.Rank
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class RankController : AppDataController
    {
        public RankController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RankLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rank/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RankLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rank 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RankLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RankLogic>(pageno, pagesize, sort, typeof(RankLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rank/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<RankLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RankLogic>(id, typeof(RankLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}