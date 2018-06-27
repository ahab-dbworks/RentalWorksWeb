using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.CompanyContact
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class CompanyContactController : AppDataController
    {
        public CompanyContactController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CompanyContactLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/companycontact/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CompanyContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/companycontact 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CompanyContactLogic>(pageno, pagesize, sort, typeof(CompanyContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/companycontact/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CompanyContactLogic>(id, typeof(CompanyContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/companycontact 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CompanyContactLogic l)
        {
            return await DoPostAsync<CompanyContactLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/companycontact/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CompanyContactLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}