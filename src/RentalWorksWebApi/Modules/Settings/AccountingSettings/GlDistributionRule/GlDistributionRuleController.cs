using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Modules.Settings.AccountingSettings.GlAccount;

namespace WebApi.Modules.Settings.AccountingSettings.GlDistributionRule
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
        [FwControllerMethod(Id:"TGSUSEa1NH7q", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Hb2wHF2PK9v2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/gldistributionrule 
        [HttpGet]
        [FwControllerMethod(Id:"creqXexG3apd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<GlDistributionRuleLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GlDistributionRuleLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/gldistributionrule/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"BWIEKcNomzH7", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<GlDistributionRuleLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GlDistributionRuleLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/gldistributionrule 
        [HttpPost]
        [FwControllerMethod(Id:"nAF3lRel3dXS", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<GlDistributionRuleLogic>> NewAsync([FromBody]GlDistributionRuleLogic l)
        {
            return await DoNewAsync<GlDistributionRuleLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/gldistributionrule/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "XBiXLUfo4NbOE", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<GlDistributionRuleLogic>> EditAsync([FromRoute] string id, [FromBody]GlDistributionRuleLogic l)
        {
            return await DoEditAsync<GlDistributionRuleLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/gldistributionrule/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"olBYTapfO7Rm", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<GlDistributionRuleLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/settings/validateglaccount/browse
        [HttpPost("validateglaccount/browse")]
        [FwControllerMethod(Id: "Lgb2SEdYtXzu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateGlAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
    }
}
