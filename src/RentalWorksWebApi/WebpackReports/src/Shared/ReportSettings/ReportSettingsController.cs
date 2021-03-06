using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Reports.Shared.ReportSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "arqFEggnNSrA6")]
    public class ReportSettingsController : AppDataController
    {
        public ReportSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ReportSettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/reportsettings/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "AuNgKmAusHfI", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/reportsettings/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "n6E1QK307fkj", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/reportsettings 
        [HttpGet]
        [FwControllerMethod(Id: "TpNesZ6mZDBa", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<IEnumerable<ReportSettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ReportSettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/reportsettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "daW5YOTtGaa5H", ActionType: FwControllerActionTypes.View, ValidateSecurityGroup: false)]
        public async Task<ActionResult<ReportSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ReportSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/reportsettings 
        [HttpPost]
        [FwControllerMethod(Id: "N8frmU0rQrlN", ActionType: FwControllerActionTypes.New, ValidateSecurityGroup: false)]
        public async Task<ActionResult<ReportSettingsLogic>> PostAsync([FromBody]ReportSettingsLogic l)
        {
            return await DoPostAsync<ReportSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/reportsettings/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "gDf2TSDtuIsb", ActionType: FwControllerActionTypes.Edit, ValidateSecurityGroup: false)]
        public async Task<ActionResult<ReportSettingsLogic>> EditAsync([FromRoute] string id, [FromBody]ReportSettingsLogic l)
        {
            return await DoEditAsync<ReportSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/reportsettings/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "xr50gH6Te2JFI", ActionType: FwControllerActionTypes.Delete, ValidateSecurityGroup: false)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ReportSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
