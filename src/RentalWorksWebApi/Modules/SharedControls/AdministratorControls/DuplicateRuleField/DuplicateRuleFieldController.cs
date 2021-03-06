using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.AdministratorControls.DuplicateRuleField
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id:"DlPBQCJUC0iK")]
    public class DuplicateRuleFieldController : AppDataController
    {
        public DuplicateRuleFieldController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DuplicateRuleFieldLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/duplicaterulefield/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"zfhwcCDX7Ww3", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ZAv1QFyz70OA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/duplicaterulefield 
        [HttpGet]
        [FwControllerMethod(Id:"WT2q2haLaceh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DuplicateRuleFieldLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DuplicateRuleFieldLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/duplicaterulefield/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"KsObXvcTzudr", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<DuplicateRuleFieldLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DuplicateRuleFieldLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/duplicaterulefield 
        [HttpPost]
        [FwControllerMethod(Id:"dzMpY9wro7xX", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DuplicateRuleFieldLogic>> NewAsync([FromBody]DuplicateRuleFieldLogic l)
        {
            return await DoNewAsync<DuplicateRuleFieldLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/duplicaterulefield/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "hRJuvvqTi4mHw", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DuplicateRuleFieldLogic>> EditAsync([FromRoute] string id, [FromBody]DuplicateRuleFieldLogic l)
        {
            return await DoEditAsync<DuplicateRuleFieldLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/duplicaterulefield/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"UDmjAIO3ivXI", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DuplicateRuleFieldLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
