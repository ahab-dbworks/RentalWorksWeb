using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderItemRecType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class OrderItemRecTypeController : AppDataController
    {
        public OrderItemRecTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderItemRecTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderitemrectype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderItemRecTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderitemrectype 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderItemRecTypeLogic>(pageno, pagesize, sort, typeof(OrderItemRecTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderitemrectype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderItemRecTypeLogic>(id, typeof(OrderItemRecTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/orderitemrectype 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]OrderItemRecTypeLogic l)
        //{
        //    return await DoPostAsync<OrderItemRecTypeLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/orderitemrectype/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(OrderItemRecTypeLogic));
        //}
        ////------------------------------------------------------------------------------------ 
    }
}