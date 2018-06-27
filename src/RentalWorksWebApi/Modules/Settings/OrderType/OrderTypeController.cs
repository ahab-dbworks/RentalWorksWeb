using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace WebApi.Modules.Settings.OrderType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class OrderTypeController : AppDataController
    {
        public OrderTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertype/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertype 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeLogic>(pageno, pagesize, sort, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertype/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeLogic>(id, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertype 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderTypeLogic l)
        {
            return await DoPostAsync<OrderTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertype/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}