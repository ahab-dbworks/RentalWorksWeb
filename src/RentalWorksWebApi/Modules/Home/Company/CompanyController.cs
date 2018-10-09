using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.Company
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class CompanyController : AppDataController
    {
        public CompanyController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CompanyLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/company/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/company 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CompanyLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/company/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CompanyLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}