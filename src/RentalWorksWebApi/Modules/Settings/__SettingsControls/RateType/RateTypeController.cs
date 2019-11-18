using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.RateType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"CjdyyS2tmek4p")]
    public class RateTypeController : AppDataController
    {
        public RateTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RateTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ratetype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"uyEBSFelDqPkR")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"rvgwybWSzFBCo")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ratetype 
        [HttpGet]
        [FwControllerMethod(Id:"RZ9uc9Sy7L9oT")]
        public async Task<ActionResult<IEnumerable<RateTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RateTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ratetype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"bnveqiLMI4M9c")]
        public async Task<ActionResult<RateTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RateTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
