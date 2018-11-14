using FwStandard.AppManager;
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
    [FwController(Id:"lATsdnAx7B4s")]
    public class OrderStatusHistoryController : AppDataController
    {
        public OrderStatusHistoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderStatusHistoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatushistory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"WVYLSaXhPl5z")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"mHFfQsSaG4U3")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderstatushistory 
        [HttpGet]
        [FwControllerMethod(Id:"ycPmB90d63wb")]
        public async Task<ActionResult<IEnumerable<OrderStatusHistoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderStatusHistoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderstatushistory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"g38Jc77WwV86")]
        public async Task<ActionResult<OrderStatusHistoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderStatusHistoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
