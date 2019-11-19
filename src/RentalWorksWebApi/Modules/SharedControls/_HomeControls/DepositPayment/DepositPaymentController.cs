using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.DepositPayment
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "0caPL0Rjz0bX9")]
    public class DepositPaymentController : AppDataController
    {
        public DepositPaymentController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DepositPaymentLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/depositpayment/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "0cSOu7mq6GLcJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/depositpayment/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "0CvONqc5v5cVa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/depositpayment 
        [HttpGet]
        [FwControllerMethod(Id: "0d3jWZ95dVRKU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DepositPaymentLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DepositPaymentLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/depositpayment/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "0d5AK66T0yxc6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DepositPaymentLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DepositPaymentLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/depositpayment 
        //[HttpPost] 
        //[FwControllerMethod(Id: "0DgpwFZviclhB"), ActionType: FwControllerActionTypes.Edit] 
        //public async Task<ActionResult<DepositPaymentLogic>> PostAsync([FromBody]DepositPaymentLogic l) 
        //{ 
        //return await DoPostAsync<DepositPaymentLogic>(l); 
        //} 
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/depositpayment/A0000001 
        //[HttpDelete("{id}")] 
        //[FwControllerMethod(Id: "0EOu855g7XlyV"), ActionType: FwControllerActionTypes.Delete] 
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id) 
        //{ 
        //return await DoDeleteAsync<DepositPaymentLogic>(id); 
        //} 
        ////------------------------------------------------------------------------------------ 
    }
}
