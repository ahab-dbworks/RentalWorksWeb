using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderTypeLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class OrderTypeLocationController : AppDataController
    {
        public OrderTypeLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderTypeLocationLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypelocation/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderTypeLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypelocation 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderTypeLocationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeLocationLogic>(pageno, pagesize, sort, typeof(OrderTypeLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypelocation/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderTypeLocationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeLocationLogic>(id, typeof(OrderTypeLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypelocation 
        [HttpPost]
        public async Task<ActionResult<OrderTypeLocationLogic>> PostAsync([FromBody]OrderTypeLocationLogic l)
        {
            return await DoPostAsync<OrderTypeLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertypelocation/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderTypeLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}