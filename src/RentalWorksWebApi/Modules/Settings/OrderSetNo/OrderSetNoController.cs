using FwStandard.SqlServer;
using System.Collections.Generic;
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
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderSetNoLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordersetno 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderSetNoLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderSetNoLogic>(pageno, pagesize, sort, typeof(OrderSetNoLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordersetno/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderSetNoLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderSetNoLogic>(id, typeof(OrderSetNoLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersetno 
        [HttpPost]
        public async Task<ActionResult<OrderSetNoLogic>> PostAsync([FromBody]OrderSetNoLogic l)
        {
            return await DoPostAsync<OrderSetNoLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordersetno/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderSetNoLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}