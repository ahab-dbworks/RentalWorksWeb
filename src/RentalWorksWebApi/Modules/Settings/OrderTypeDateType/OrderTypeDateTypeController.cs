using FwStandard.SqlServer;
using System.Collections.Generic;
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
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderTypeDateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypedatetype 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderTypeDateTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeDateTypeLogic>(pageno, pagesize, sort, typeof(OrderTypeDateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypedatetype/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderTypeDateTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeDateTypeLogic>(id, typeof(OrderTypeDateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypedatetype 
        [HttpPost]
        public async Task<ActionResult<OrderTypeDateTypeLogic>> PostAsync([FromBody]OrderTypeDateTypeLogic l)
        {
            return await DoPostAsync<OrderTypeDateTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertypedatetype/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderTypeDateTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}