using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.OrderStatusDetail
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class OrderStatusDetailController : AppDataController
    {
        public OrderStatusDetailController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderStatusDetailLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatusdetail/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderStatusDetailLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/orderstatusdetail 
        //[HttpGet]
        //public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<OrderStatusDetailLogic>(pageno, pagesize, sort, typeof(OrderStatusDetailLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/orderstatusdetail/A0000001 
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<OrderStatusDetailLogic>(id, typeof(OrderStatusDetailLogic));
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/orderstatusdetail 
        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody]OrderStatusDetailLogic l)
        //{
        //    return await DoPostAsync<OrderStatusDetailLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/orderstatusdetail/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(OrderStatusDetailLogic));
        //}
        ////------------------------------------------------------------------------------------ 
    }
}