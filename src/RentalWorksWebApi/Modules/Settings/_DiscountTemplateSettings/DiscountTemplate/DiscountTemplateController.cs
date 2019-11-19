using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.DiscountTemplateSettings.DiscountTemplate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"1uoU0MeI7hIu")]
    public class DiscountTemplateController : AppDataController
    {
        public DiscountTemplateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DiscountTemplateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/discounttemplate/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"azdILZ8GLYzn", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"suRElv81n6kM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discounttemplate 
        [HttpGet]
        [FwControllerMethod(Id:"L2KuinkE2zeX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DiscountTemplateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DiscountTemplateLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discounttemplate/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"We0SORgg2QeJ", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<DiscountTemplateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DiscountTemplateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/discounttemplate 
        [HttpPost]
        [FwControllerMethod(Id:"XUoIkpdiGBjB", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DiscountTemplateLogic>> NewAsync([FromBody]DiscountTemplateLogic l)
        {
            return await DoNewAsync<DiscountTemplateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/discounttemplate/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "DVTvZQOnWO6TA", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DiscountTemplateLogic>> EditAsync([FromRoute] string id, [FromBody]DiscountTemplateLogic l)
        {
            return await DoEditAsync<DiscountTemplateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/discounttemplate/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"gH1ia0r0zdXf", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DiscountTemplateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
