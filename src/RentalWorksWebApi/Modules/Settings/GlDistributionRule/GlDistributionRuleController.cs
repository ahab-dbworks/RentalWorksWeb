using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.GlDistributionRule
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"71QUDQyIbibs")]
    public class GlDistributionRuleController : AppDataController
    {
        public GlDistributionRuleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GlDistributionRuleLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistributionrule/browse 
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
        // GET api/v1/gldistributionrule 
        [HttpGet]
        [FwControllerMethod(Id:"creqXexG3apd")]
        public async Task<ActionResult<IEnumerable<GlDistributionRuleLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GlDistributionRuleLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/gldistributionrule/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"BWIEKcNomzH7")]
        public async Task<ActionResult<GlDistributionRuleLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GlDistributionRuleLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistributionrule 
        [HttpPost]
        [FwControllerMethod(Id:"nAF3lRel3dXS")]
        public async Task<ActionResult<GlDistributionRuleLogic>> PostAsync([FromBody]GlDistributionRuleLogic l)
        {
            return await DoPostAsync<GlDistributionRuleLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/gldistributionrule/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"olBYTapfO7Rm")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<GlDistributionRuleLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
