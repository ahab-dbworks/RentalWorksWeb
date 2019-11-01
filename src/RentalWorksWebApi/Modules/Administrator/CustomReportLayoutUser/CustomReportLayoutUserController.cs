using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Administrator.CustomReportLayoutUser
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "nHNdXDBX6m6cp")]
    public class CustomReportLayoutUserController : AppDataController
    {
        public CustomReportLayoutUserController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomReportLayoutUserLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutuser/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "NsKm1SwS3Mjo")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutuser/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "B5l2KgRvlVnz")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customreportlayoutuser 
        [HttpGet]
        [FwControllerMethod(Id: "XMW4mqgFmyK2p")]
        public async Task<ActionResult<IEnumerable<CustomReportLayoutUserLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomReportLayoutUserLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customreportlayoutuser/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "fW60ZtXxzQsf")]
        public async Task<ActionResult<CustomReportLayoutUserLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomReportLayoutUserLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutuser 
        [HttpPost]
        [FwControllerMethod(Id: "GEwAjdPQd4kS")]
        public async Task<ActionResult<CustomReportLayoutUserLogic>> PostAsync([FromBody]CustomReportLayoutUserLogic l)
        {
            return await DoPostAsync<CustomReportLayoutUserLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/customreportlayoutuser/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "IQZscu9OBtw9")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomReportLayoutUserLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
