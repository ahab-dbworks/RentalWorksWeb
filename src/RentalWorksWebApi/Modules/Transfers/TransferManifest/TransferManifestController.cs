using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.HomeControls.Manifest;
using WebApi.Modules.Warehouse.Contract;
using WebLibrary;

//dummy-security-controller 
namespace WebApi.Modules.Transfers.TransferManifest
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"tc2HgrtvGDJ5")]
    public class TransferManifestController : AppDataController
    {
        public TransferManifestController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ManifestLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/contract/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id: "pHByLgOgIDym", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ManifestLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contract/legend
        [HttpGet("legend")]
        [FwControllerMethod(Id: "MQclt0CnbwnS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            //legend.Add("Unassigned Items", RwGlobals.QUOTE_ORDER_ON_HOLD_COLOR);
            //legend.Add("Pending Exchanges", RwGlobals.QUOTE_ORDER_NO_CHARGE_COLOR);
            legend.Add("Migrated", RwGlobals.CONTRACT_MIGRATED_COLOR);
            //legend.Add("Inactive Deal", RwGlobals.ORDER_LATE_COLOR);
            //legend.Add("Truck (No Charge)", RwGlobals.ORDER_LATE_COLOR);
            legend.Add("Adjusted Billing Date", RwGlobals.CONTRACT_BILLING_DATE_ADJUSTED_COLOR);
            legend.Add("Voided Items", RwGlobals.CONTRACT_ITEM_VOIDED_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "vwnOtRVgKY36", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync<ManifestLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contract
        [HttpGet]
        [FwControllerMethod(Id: "8VMMrfwmdQPK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ManifestLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ManifestLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contract/A0000001
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ManifestLogic))]
        [ProducesResponseType(404)]
        [FwControllerMethod(Id: "y0DWKwX3vRdq", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ManifestLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ManifestLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contract
        [HttpPost]
        [FwControllerMethod(Id: "3aW0QsKWXfmG", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ManifestLogic>> NewAsync([FromBody]ManifestLogic l)
        {
            return await DoNewAsync<ManifestLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/contract/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "n0569aPNQLqD", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ManifestLogic>> EditAsync([FromRoute] string id, [FromBody]ManifestLogic l)
        {
            return await DoEditAsync<ManifestLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/contract/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "o9Fob9KqRxSr", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ManifestLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}

