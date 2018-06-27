using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.DiscountTemplate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class DiscountTemplateController : AppDataController
    {
        public DiscountTemplateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DiscountTemplateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/discounttemplate/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DiscountTemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discounttemplate 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DiscountTemplateLogic>(pageno, pagesize, sort, typeof(DiscountTemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discounttemplate/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DiscountTemplateLogic>(id, typeof(DiscountTemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/discounttemplate 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]DiscountTemplateLogic l)
        {
            return await DoPostAsync<DiscountTemplateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/discounttemplate/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DiscountTemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}