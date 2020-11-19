using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.CreditCard.CreditCardPinPad
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "wnNXVnqIrCgF")]
    public class CreditCardPinPadController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public CreditCardPinPadController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CreditCardPinPadLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/creditcardpinpad/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "n4E5SFTSSvlV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/creditcardpinpad/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "ljG0P1mAkybI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/creditcardpinpad 
        [HttpGet]
        [FwControllerMethod(Id: "JvkYs4FbbtoR", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CreditCardPinPadLogic>>> GetManyAsync([FromQuery] int pageno, [FromQuery] int pagesize, [FromQuery] string sort)
        {
            return await DoGetAsync<CreditCardPinPadLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/creditcardpinpad/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "O5nuKRebLANM", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CreditCardPinPadLogic>> GetOneAsync([FromRoute] string id)
        {
            return await DoGetAsync<CreditCardPinPadLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/creditcardpinpad 
        [HttpPost]
        [FwControllerMethod(Id: "DDOQcK3Y35wy", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CreditCardPinPadLogic>> NewAsync([FromBody] CreditCardPinPadLogic l)
        {
            return await DoNewAsync<CreditCardPinPadLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/creditcardpinpad/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "v5AX0hT04YmT", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CreditCardPinPadLogic>> EditAsync([FromRoute] string id, [FromBody] CreditCardPinPadLogic l)
        {
            return await DoEditAsync<CreditCardPinPadLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/creditcardpinpad/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "O20Ct97jQHwX", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute] string id)
        {
            return await DoDeleteAsync<CreditCardPinPadLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
