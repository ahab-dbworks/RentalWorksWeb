using FwStandard.AppManager;
﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebLibrary;

namespace WebApi.Modules.Home.OrderSummary
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"J192VtSdU1ay")]
    public class OrderSummaryController : AppDataController
    {
        public OrderSummaryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderSummaryLogic); }
        //------------------------------------------------------------------------------------
        // GET api/v1/ordersummary/A0000001~P
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"uS3uUEydAzRW")]
        public async Task<ActionResult<OrderSummaryLogic>> GetOneAsync([FromRoute]string id)
        {
            string[] ids = id.Split('~');
            if (ids.Length.Equals(1))
            {
                id = id + "~" + RwConstants.TOTAL_TYPE_PERIOD;
            }
            return await DoGetAsync<OrderSummaryLogic>(id);
        }
        //------------------------------------------------------------------------------------
   }
}
