using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.Payment
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "Y7YC6NpLqX8kx")]
    public class PaymentController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public PaymentController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PaymentLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/payment/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "Y8plvKHzbbJPv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/payment/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "Y966BxoU9GaTT", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/payment/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "DC2saRJSipdW3", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Foreign Currency", RwGlobals.FOREIGN_CURRENCY_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/payment 
        [HttpGet]
        [FwControllerMethod(Id: "Y9ePWOz90DFZr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PaymentLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PaymentLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/payment/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "y9OrvcH1WQQ4R", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<PaymentLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PaymentLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/payment 
        [HttpPost]
        [FwControllerMethod(Id: "YA1slDDhm7nCv", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PaymentLogic>> NewAsync([FromBody]PaymentLogic l)
        {
            return await DoNewAsync<PaymentLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/payment/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "yaDkJFkrMenVX", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PaymentLogic>> EditAsync([FromRoute] string id, [FromBody]PaymentLogic l)
        {
            return await DoEditAsync<PaymentLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/payment/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "YAEY9PPoRZIbg", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PaymentLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
