using FwStandard.AppManager;
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
    [FwController(Id:"Cs6Vpnk0qDKuF")]
    public class RankController : AppDataController
    {
        public RankController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RankLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rank/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"yKcjz5ZQvIpRE", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"xzNEfOWU1xrgA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rank 
        [HttpGet]
        [FwControllerMethod(Id:"b0lKRAfG3cwMi", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<RankLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RankLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rank/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"7AFhRn6SUvJ6s", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<RankLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RankLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
