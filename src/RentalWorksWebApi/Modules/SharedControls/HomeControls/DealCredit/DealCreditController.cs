using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using FwStandard.Utilities;

namespace WebApi.Modules.HomeControls.DealCredit
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "OCkLGwclipEA")]
    public class DealCreditController : AppDataController
    {
        public DealCreditController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealCreditLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealcredit/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "Mr3OFcb7XiKr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            if (FwValidate.IsPropertyDefined(browseRequest.uniqueids, "OrderId"))
            { 
                string orderId = browseRequest.uniqueids.OrderId;
                using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
                {
                    browseRequest.uniqueids.DealId = (await FwSqlCommand.GetDataAsync(conn, AppConfig.DatabaseSettings.QueryTimeout, "dealorder", "orderid", browseRequest.uniqueids.OrderId, "dealid")).ToString();
                }
            }
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealcredit/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "YYczHwYhGlho", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
