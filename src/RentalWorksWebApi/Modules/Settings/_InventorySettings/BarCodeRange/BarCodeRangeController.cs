using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.InventorySettings.BarCodeRange
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"akTqMO0zmuc")]
    public class BarCodeRangeController : AppDataController
    {
        public BarCodeRangeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BarCodeRangeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/barcoderange/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"HbMs8bqVtYS", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"PWmqlX0X17R", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/barcoderange 
        [HttpGet]
        [FwControllerMethod(Id:"mZCkgCdLlnd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<BarCodeRangeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BarCodeRangeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/barcoderange/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"NteLYEXr5ry", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<BarCodeRangeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BarCodeRangeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/barcoderange 
        [HttpPost]
        [FwControllerMethod(Id:"QC04JbhltMY", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<BarCodeRangeLogic>> NewAsync([FromBody]BarCodeRangeLogic l)
        {
            return await DoNewAsync<BarCodeRangeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/barcoderange/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "pXjm7weosRPhl", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<BarCodeRangeLogic>> EditAsync([FromRoute] string id, [FromBody]BarCodeRangeLogic l)
        {
            return await DoEditAsync<BarCodeRangeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/barcoderange/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"0r9ng0eQReC", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<BarCodeRangeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
