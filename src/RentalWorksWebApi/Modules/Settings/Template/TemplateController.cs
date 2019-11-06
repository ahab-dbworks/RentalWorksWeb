using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.Template
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"74uXyH7jXXbZM")]
    public class TemplateController : AppDataController
    {
        public TemplateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(TemplateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/template/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"E4bGZf3GhckKT")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"QKvpiTn4OyzQU")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/template 
        [HttpGet]
        [FwControllerMethod(Id:"q1VbZFAgHkZaZ")]
        public async Task<ActionResult<IEnumerable<TemplateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<TemplateLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/template/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"0Xv0F2r9jmBJZ")]
        public async Task<ActionResult<TemplateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<TemplateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/template 
        [HttpPost]
        [FwControllerMethod(Id:"5pRKdQ69h2mLK")]
        public async Task<ActionResult<TemplateLogic>> PostAsync([FromBody]TemplateLogic l)
        {
            return await DoPostAsync<TemplateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/template/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"MbDonOp1dphBB")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<TemplateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
