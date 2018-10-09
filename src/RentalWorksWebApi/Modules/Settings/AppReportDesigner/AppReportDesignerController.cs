using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.AppReportDesigner
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class AppReportDesignerController : AppDataController
    {
        public AppReportDesignerController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AppReportDesignerLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/appreportdesigner/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/appreportdesigner 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppReportDesignerLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AppReportDesignerLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/appreportdesigner/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<AppReportDesignerLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AppReportDesignerLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/appreportdesigner 
        [HttpPost]
        public async Task<ActionResult<AppReportDesignerLogic>> PostAsync([FromBody]AppReportDesignerLogic l)
        {
            return await DoPostAsync<AppReportDesignerLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/appreportdesigner/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
