using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Settings.ExportSettings.DataExportFormat
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "ItSDcv89HNNo")]
    public class DataExportFormatController : AppDataController
    {
     
        public DataExportFormatController(IOptions<FwApplicationConfig> appConfig, IHostingEnvironment hostingEnvironment) : base(appConfig) { logicType = typeof(DataExportFormatLogic);}
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dataexportformat/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "PLMjZ0LHSXSc", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup:false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dataexportformat/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "HLTa6HyJyMrr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/dataexportformat 
        [HttpGet]
        [FwControllerMethod(Id: "0jwzSINL47zT", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DataExportFormatLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DataExportFormatLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/dataexportformat/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "LdmsYQCIRftL", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<DataExportFormatLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DataExportFormatLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dataexportformat 
        [HttpPost]
        [FwControllerMethod(Id: "XtYWesUL0dje", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DataExportFormatLogic>> PostAsync([FromBody]DataExportFormatLogic l)
        {
            return await DoNewAsync<DataExportFormatLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/dataexportformat/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "LDcNaeKvhHYM", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DataExportFormatLogic>> EditAsync([FromRoute] string id, [FromBody]DataExportFormatLogic l)
        {
            return await DoEditAsync<DataExportFormatLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/dataexportformat/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "r56BPxANoqA3", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DataExportFormatLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
