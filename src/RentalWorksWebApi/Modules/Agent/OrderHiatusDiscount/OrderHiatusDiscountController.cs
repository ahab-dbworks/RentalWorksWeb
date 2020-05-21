using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Agent.OrderHiatusDiscount
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "agent-v1")]
    [FwController(Id: "q4N43Gk5H1471")]
    public class OrderHiatusDiscountController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public OrderHiatusDiscountController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderHiatusDiscountLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderhiatusdiscount/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "q61490IIdSqkj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderhiatusdiscount/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "Q6noniZiwNv6j", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderhiatusdiscount 
        [HttpGet]
        [FwControllerMethod(Id: "q6rP9fDIVrpjx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<OrderHiatusDiscountLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderHiatusDiscountLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderhiatusdiscount/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "q7TsV0AQMYcpt", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<OrderHiatusDiscountLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderHiatusDiscountLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderhiatusdiscount 
        [HttpPost]
        [FwControllerMethod(Id: "q7W2RU0SbDkgv", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<OrderHiatusDiscountLogic>> NewAsync([FromBody]OrderHiatusDiscountLogic l)
        {
            return await DoNewAsync<OrderHiatusDiscountLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/orderhiatusdiscount/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "Q8r7qyOtzTXGM", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OrderHiatusDiscountLogic>> EditAsync([FromRoute] string id, [FromBody]OrderHiatusDiscountLogic l)
        {
            return await DoEditAsync<OrderHiatusDiscountLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/orderhiatusdiscount/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "qA42Lu6YRRtGs", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OrderHiatusDiscountLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
