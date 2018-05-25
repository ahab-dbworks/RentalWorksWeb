using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Administrator.CustomModule
{
    [Route("api/v1/[controller]")]
    public class CustomModuleController : AppDataController
    {
        public CustomModuleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomModuleLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/custommodule/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CustomModuleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/custommodule 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomModuleLogic>(pageno, pagesize, sort, typeof(CustomModuleLogic));
        }
        //------------------------------------------------------------------------------------ 

    }
}