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
        [FwControllerMethod(Id:"azdILZ8GLYzn")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"suRElv81n6kM")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discounttemplate 
        [HttpGet]
        [FwControllerMethod(Id:"L2KuinkE2zeX")]
        public async Task<ActionResult<IEnumerable<DiscountTemplateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DiscountTemplateLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discounttemplate/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"We0SORgg2QeJ")]
        public async Task<ActionResult<DiscountTemplateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DiscountTemplateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/discounttemplate 
        [HttpPost]
        [FwControllerMethod(Id:"XUoIkpdiGBjB")]
        public async Task<ActionResult<DiscountTemplateLogic>> PostAsync([FromBody]DiscountTemplateLogic l)
        {
            return await DoPostAsync<DiscountTemplateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/discounttemplate/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"gH1ia0r0zdXf")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DiscountTemplateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
