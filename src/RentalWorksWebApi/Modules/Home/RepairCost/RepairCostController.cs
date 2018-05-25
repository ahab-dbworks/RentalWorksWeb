using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.RepairCost
{
    [Route("api/v1/[controller]")]
    public class RepairCostController : AppDataController
    {
        public RepairCostController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RepairCostLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repaircost/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RepairCostLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repaircost 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairCostLogic>(pageno, pagesize, sort, typeof(RepairCostLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repaircost/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairCostLogic>(id, typeof(RepairCostLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repaircost 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RepairCostLogic l)
        {
            return await DoPostAsync<RepairCostLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/repaircost/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RepairCostLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repaircost/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
