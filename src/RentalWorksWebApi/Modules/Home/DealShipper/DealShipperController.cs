using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.DealShipper
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class DealShipperController : AppDataController
    {
        public DealShipperController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealShipperLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealshipper/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DealShipperLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/dealshipper 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealShipperLogic>(pageno, pagesize, sort, typeof(DealShipperLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/dealshipper/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealShipperLogic>(id, typeof(DealShipperLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealshipper 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]DealShipperLogic l)
        {
            return await DoPostAsync<DealShipperLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/dealshipper/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DealShipperLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}