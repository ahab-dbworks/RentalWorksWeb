using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Administrator.CustomReportLayoutGroup
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "11txpzVKVGi2")]
    public class CustomReportLayoutGroupController : AppDataController
    {
        public CustomReportLayoutGroupController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomReportLayoutGroupLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutgroup/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "EbgBVyEmJjb8")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutgroup/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "BINtb1PjRFnuJ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customreportlayoutgroup 
        [HttpGet]
        [FwControllerMethod(Id: "ksm4EdpWEIlP")]
        public async Task<ActionResult<IEnumerable<CustomReportLayoutGroupLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomReportLayoutGroupLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customreportlayoutgroup/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "hEP97F7gZTtS")]
        public async Task<ActionResult<CustomReportLayoutGroupLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomReportLayoutGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutgroup 
        [HttpPost]
        [FwControllerMethod(Id: "ogvXUP6wJjkhh")]
        public async Task<ActionResult<CustomReportLayoutGroupLogic>> PostAsync([FromBody]CustomReportLayoutGroupLogic l)
        {
            return await DoPostAsync<CustomReportLayoutGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/customreportlayoutgroup/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "S5OEQ9jQgwsS")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomReportLayoutGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
