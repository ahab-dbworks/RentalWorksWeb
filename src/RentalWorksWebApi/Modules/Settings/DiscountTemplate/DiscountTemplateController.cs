using FwStandard.SqlServer;
using System.Collections.Generic;
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
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DiscountTemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discounttemplate 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountTemplateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DiscountTemplateLogic>(pageno, pagesize, sort, typeof(DiscountTemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discounttemplate/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountTemplateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DiscountTemplateLogic>(id, typeof(DiscountTemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/discounttemplate 
        [HttpPost]
        public async Task<ActionResult<DiscountTemplateLogic>> PostAsync([FromBody]DiscountTemplateLogic l)
        {
            return await DoPostAsync<DiscountTemplateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/discounttemplate/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DiscountTemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}