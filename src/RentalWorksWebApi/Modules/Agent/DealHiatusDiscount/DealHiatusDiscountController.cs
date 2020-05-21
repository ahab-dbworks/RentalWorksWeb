using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Agent.DealHiatusDiscount
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "agent-v1")]
    [FwController(Id: "qyEHq2bK1WIJ4")]
    public class DealHiatusDiscountController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public DealHiatusDiscountController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealHiatusDiscountLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealhiatusdiscount/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "qyIqXkfkEhfFU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealhiatusdiscount/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "QyPciNIpVqYRH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/dealhiatusdiscount 
        [HttpGet]
        [FwControllerMethod(Id: "QyPcx8ivgNRcZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DealHiatusDiscountLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealHiatusDiscountLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/dealhiatusdiscount/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "QYPRos1W04OeX", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<DealHiatusDiscountLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealHiatusDiscountLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealhiatusdiscount 
        [HttpPost]
        [FwControllerMethod(Id: "QYu0929xq7Kib", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DealHiatusDiscountLogic>> NewAsync([FromBody]DealHiatusDiscountLogic l)
        {
            return await DoNewAsync<DealHiatusDiscountLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/dealhiatusdiscount/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "qZb3vkitnVNIt", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DealHiatusDiscountLogic>> EditAsync([FromRoute] string id, [FromBody]DealHiatusDiscountLogic l)
        {
            return await DoEditAsync<DealHiatusDiscountLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/dealhiatusdiscount/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "qZkctehwA7NVM", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DealHiatusDiscountLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
