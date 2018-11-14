using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.GlDistribution
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"71QUDQyIbibs")]
    public class GlDistributionController : AppDataController
    {
        public GlDistributionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GlDistributionLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistribution/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"TGSUSEa1NH7q")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"Hb2wHF2PK9v2")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/gldistribution 
        [HttpGet]
        [FwControllerMethod(Id:"creqXexG3apd")]
        public async Task<ActionResult<IEnumerable<GlDistributionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GlDistributionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/gldistribution/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"BWIEKcNomzH7")]
        public async Task<ActionResult<GlDistributionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GlDistributionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistribution 
        [HttpPost]
        [FwControllerMethod(Id:"nAF3lRel3dXS")]
        public async Task<ActionResult<GlDistributionLogic>> PostAsync([FromBody]GlDistributionLogic l)
        {
            return await DoPostAsync<GlDistributionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/gldistribution/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"olBYTapfO7Rm")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
