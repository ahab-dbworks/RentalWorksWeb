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
    public class TemplateController : AppDataController
    {
        public TemplateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(TemplateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/template/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(TemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/template 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<TemplateLogic>(pageno, pagesize, sort, typeof(TemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/template/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<TemplateLogic>(id, typeof(TemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/template 
        [HttpPost]
        public async Task<ActionResult<TemplateLogic>> PostAsync([FromBody]TemplateLogic l)
        {
            return await DoPostAsync<TemplateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/template/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(TemplateLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}