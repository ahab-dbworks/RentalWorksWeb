using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderTypeContactTitle
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class OrderTypeContactTitleController : AppDataController
    {
        public OrderTypeContactTitleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderTypeContactTitleLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypecontacttitle/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderTypeContactTitleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypecontacttitle 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeContactTitleLogic>(pageno, pagesize, sort, typeof(OrderTypeContactTitleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypecontacttitle/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeContactTitleLogic>(id, typeof(OrderTypeContactTitleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypecontacttitle 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderTypeContactTitleLogic l)
        {
            return await DoPostAsync<OrderTypeContactTitleLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertypecontacttitle/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderTypeContactTitleLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}