using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderTypeDateType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class OrderTypeDateTypeController : AppDataController
    {
        public OrderTypeDateTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderTypeDateTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypedatetype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderTypeDateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypedatetype 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeDateTypeLogic>(pageno, pagesize, sort, typeof(OrderTypeDateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypedatetype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeDateTypeLogic>(id, typeof(OrderTypeDateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypedatetype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderTypeDateTypeLogic l)
        {
            return await DoPostAsync<OrderTypeDateTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertypedatetype/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderTypeDateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}