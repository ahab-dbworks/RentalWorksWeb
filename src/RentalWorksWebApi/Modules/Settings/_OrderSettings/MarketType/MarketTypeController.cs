using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderSettings.MarketType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"sEgqHq5tov4n")]
    public class MarketTypeController : AppDataController
    {
        public MarketTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MarketTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/markettype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"h6mK4QUAFwq1", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"vcTWWSKkglVu", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/markettype 
        [HttpGet]
        [FwControllerMethod(Id:"CdaD3Y24xYwC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<MarketTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MarketTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/markettype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"g25CgeLkHROf", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<MarketTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<MarketTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/markettype 
        [HttpPost]
        [FwControllerMethod(Id:"NmTBhGAyxCx1", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<MarketTypeLogic>> NewAsync([FromBody]MarketTypeLogic l)
        {
            return await DoNewAsync<MarketTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/markettype/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "250K7jRqyjv6d", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<MarketTypeLogic>> EditAsync([FromRoute] string id, [FromBody]MarketTypeLogic l)
        {
            return await DoEditAsync<MarketTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/markettype/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"HHNAHIlSAEjg", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<MarketTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
