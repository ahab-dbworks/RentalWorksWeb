using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.CurrencyExchangeRate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "UfURKoOaUi87C")]
    public class CurrencyExchangeRateController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public CurrencyExchangeRateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CurrencyExchangeRateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/currencyexchangerate/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "uFVZ6FgJQu05h", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/currencyexchangerate/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "UfyrmJtf9juy8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/currencyexchangerate 
        [HttpGet]
        [FwControllerMethod(Id: "UgfqNeCRqE5Cp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CurrencyExchangeRateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CurrencyExchangeRateLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/currencyexchangerate/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "uhS9UiQoWPSl0", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CurrencyExchangeRateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CurrencyExchangeRateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/currencyexchangerate 
        [HttpPost]
        [FwControllerMethod(Id: "UIByik9EQ8aie", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CurrencyExchangeRateLogic>> NewAsync([FromBody]CurrencyExchangeRateLogic l)
        {
            return await DoNewAsync<CurrencyExchangeRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/currencyexchangerate/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "UiD9QwXZSyauZ", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CurrencyExchangeRateLogic>> EditAsync([FromRoute] string id, [FromBody]CurrencyExchangeRateLogic l)
        {
            return await DoEditAsync<CurrencyExchangeRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/currencyexchangerate/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "UiegmTtaubpxB", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CurrencyExchangeRateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
