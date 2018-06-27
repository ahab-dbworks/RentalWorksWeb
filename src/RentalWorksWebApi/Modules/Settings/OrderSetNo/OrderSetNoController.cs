using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderSetNo
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class OrderSetNoController : AppDataController
    {
        public OrderSetNoController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderSetNoLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersetno/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderSetNoLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordersetno 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderSetNoLogic>(pageno, pagesize, sort, typeof(OrderSetNoLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordersetno/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderSetNoLogic>(id, typeof(OrderSetNoLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersetno 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderSetNoLogic l)
        {
            return await DoPostAsync<OrderSetNoLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordersetno/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderSetNoLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}