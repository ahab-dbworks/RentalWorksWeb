using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.NumberFormat
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"7hvTP7ZP79z9")]
    public class NumberFormatController : AppDataController
    {
        public NumberFormatController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(NumberFormatLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/numberformat/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"4qRknX0GXBFh")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/numberformat/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"7sIMpeA4BGOj")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/numberformat 
        [HttpGet]
        [FwControllerMethod(Id:"MJnWExrSqGag")]
        public async Task<ActionResult<IEnumerable<NumberFormatLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<NumberFormatLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/numberformat/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"tixPs9bafJrI")]
        public async Task<ActionResult<NumberFormatLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<NumberFormatLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
