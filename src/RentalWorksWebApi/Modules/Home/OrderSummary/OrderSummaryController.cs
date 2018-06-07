using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Home.OrderSummary
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class OrderSummaryController : AppDataController
    {
        public OrderSummaryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderSummaryLogic); }
        //------------------------------------------------------------------------------------
        // GET api/v1/ordersummary/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderSummaryLogic>(id, typeof(OrderSummaryLogic));
        }
        //------------------------------------------------------------------------------------
   }
}