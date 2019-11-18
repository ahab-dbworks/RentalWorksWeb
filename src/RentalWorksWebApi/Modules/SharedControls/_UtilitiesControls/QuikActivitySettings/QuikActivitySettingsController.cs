using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
namespace WebApi.Modules.Utilities.QuikActivitySettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "35rFOziBRCojt")]
    public class QuikActivitySettingsController : AppDataController
    {
        public QuikActivitySettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(QuikActivitySettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quikactivitysettings/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "35sdVijaTEUM")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quikactivitysettings/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "36L9jQuAKWMn")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/quikactivitysettings 
        [HttpGet]
        [FwControllerMethod(Id: "37J0reAzyLBL")]
        public async Task<ActionResult<IEnumerable<QuikActivitySettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<QuikActivitySettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/quikactivitysettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "3ayi8I9vwIKt")]
        public async Task<ActionResult<QuikActivitySettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<QuikActivitySettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/quikactivitysettings 
        [HttpPost]
        [FwControllerMethod(Id: "3b9tLr7vi7K4")]
        public async Task<ActionResult<QuikActivitySettingsLogic>> PostAsync([FromBody]QuikActivitySettingsLogic l)
        {
            return await DoPostAsync<QuikActivitySettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/quikactivitysettings/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "3bf1WgNHvIyF ")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<QuikActivitySettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
