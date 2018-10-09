using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.OrderStatusHistory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class OrderStatusHistoryController : AppDataController
    {
        public OrderStatusHistoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderStatusHistoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatushistory/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderStatusHistoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderstatushistory 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatusHistoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderStatusHistoryLogic>(pageno, pagesize, sort, typeof(OrderStatusHistoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderstatushistory/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatusHistoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderStatusHistoryLogic>(id, typeof(OrderStatusHistoryLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}