using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using FwStandard.Utilities;
using WebApi.Modules.Billing.Receipt;
using WebApi.Modules.Billing.ProcessCreditCard;

namespace WebApi.Modules.HomeControls.DealCredit
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "OCkLGwclipEA")]
    public class DealCreditController : AppDataController
    {
        readonly IProcessCreditCardPlugin processCreditCardPlugin;
        public DealCreditController(IOptions<FwApplicationConfig> appConfig, IProcessCreditCardPlugin processCreditCardPlugin) : base(appConfig)
        {
            logicType = typeof(DealCreditLogic);
            this.processCreditCardPlugin = processCreditCardPlugin;
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealcredit/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "Mr3OFcb7XiKr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody] BrowseRequest browseRequest)
        {
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
        // POST api/v1/dealcredit/refund
        [HttpPost("refund")]
        [FwControllerMethod(Id: "aVvPPeYAmQEN", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ReceiptLogic>> RefundAsync([FromBody] CreditCardRefundRequest request)
        {
            return await ReceiptLogic.DoCreditCardRefundAsync(this.AppConfig, this.UserSession, processCreditCardPlugin,  request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
