using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Administrator.WidgetUser
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "CTzXYDyNzi8ET")]
    public class WidgetUserController : AppDataController
    {
        public WidgetUserController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WidgetUserLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widgetuser/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "OPQ0JlbFANWJp")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widgetuser/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "MDuTcomyl6z7i")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/widgetuser 
        [HttpGet]
        [FwControllerMethod(Id: "z6nbmjdfcDCO0")]
        public async Task<ActionResult<IEnumerable<WidgetUserLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WidgetUserLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/widgetuser/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "HrVCkFS9XpT1L")]
        public async Task<ActionResult<WidgetUserLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WidgetUserLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widgetuser 
        [HttpPost]
        [FwControllerMethod(Id: "KC4EcdPL6WPKs")]
        public async Task<ActionResult<WidgetUserLogic>> PostAsync([FromBody]WidgetUserLogic l)
        {
            return await DoPostAsync<WidgetUserLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/widgetuser/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "gk7Y9few9S5qn")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
