using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.BarCodeRange
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
        [FwControllerMethod(Id:"HbMs8bqVtYS")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"PWmqlX0X17R")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/barcoderange 
        [HttpGet]
        [FwControllerMethod(Id:"mZCkgCdLlnd")]
        public async Task<ActionResult<IEnumerable<BarCodeRangeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BarCodeRangeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/barcoderange/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"NteLYEXr5ry")]
        public async Task<ActionResult<BarCodeRangeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BarCodeRangeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/barcoderange 
        [HttpPost]
        [FwControllerMethod(Id:"QC04JbhltMY")]
        public async Task<ActionResult<BarCodeRangeLogic>> PostAsync([FromBody]BarCodeRangeLogic l)
        {
            return await DoPostAsync<BarCodeRangeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/barcoderange/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"0r9ng0eQReC")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<BarCodeRangeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
