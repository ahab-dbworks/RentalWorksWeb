using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PoSettings.PoApprover
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"kaGlUrLG9GjN")]
    public class PoApproverController : AppDataController
    {
        public PoApproverController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoApproverLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprover/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"QhSyIviB2Xmn")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"5PV59EQmvAf0")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/poapprover 
        [HttpGet]
        [FwControllerMethod(Id:"1K60SzR2NbEx")]
        public async Task<ActionResult<IEnumerable<PoApproverLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoApproverLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/poapprover/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"F24bmvivCYf4")]
        public async Task<ActionResult<PoApproverLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoApproverLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/poapprover 
        [HttpPost]
        [FwControllerMethod(Id:"NQQ7hDodlcRn")]
        public async Task<ActionResult<PoApproverLogic>> PostAsync([FromBody]PoApproverLogic l)
        {
            return await DoPostAsync<PoApproverLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/poapprover/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"AoMJdHYdcTVw")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PoApproverLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
