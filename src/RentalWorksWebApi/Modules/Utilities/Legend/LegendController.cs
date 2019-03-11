using FwStandard.AppManager;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using WebLibrary;

namespace WebApi.Modules.Utilities.Legend
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "8U3itpEK3zE8x")]
    public class LegendController : AppDataController
    {
        public LegendController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/legend/contactcompanytype
        //[HttpGet("contactcompanytype")]
        //[FwControllerMethod(Id: "XXXXXXXXx")]
        //public async Task<ActionResult<Dictionary<string, string>>> GetContactCompanyTypeLegend()
        //{
        //    Dictionary<string, string> colors = new Dictionary<string, string>();
        //    colors.Add("Lead", RwGlobals.COMPANY_TYPE_LEAD_COLOR);
        //    colors.Add("Prospect", RwGlobals.COMPANY_TYPE_PROSPECT_COLOR);
        //    colors.Add("Customer", RwGlobals.COMPANY_TYPE_CUSTOMER_COLOR);
        //    colors.Add("Deal", RwGlobals.COMPANY_TYPE_DEAL_COLOR);
        //    colors.Add("Vendor", RwGlobals.COMPANY_TYPE_VENDOR_COLOR);
        //    return new OkObjectResult(colors);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
