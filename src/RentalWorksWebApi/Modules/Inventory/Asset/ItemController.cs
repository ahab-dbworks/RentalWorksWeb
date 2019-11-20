using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Settings.InventorySettings.InventoryStatus;
using WebApi;
using WebApi.Modules.Settings.InventorySettings.InventoryCondition;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Settings.AddressSettings.Country;

namespace WebApi.Modules.Inventory.Asset
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "kSugPLvkuNsH")]
    public class ItemController : AppDataController
    {
        public ItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/item/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "8EjRJqgdgpgQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/item/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "g02Y2myXv8pqI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            InventoryStatusLogic s = new InventoryStatusLogic();
            s.SetDependencies(AppConfig, UserSession);
            GetManyResponse<InventoryStatusLogic> r = await s.GetManyAsync<InventoryStatusLogic>(new GetManyRequest());

            Dictionary<string, string> legend = new Dictionary<string, string>();
            foreach (InventoryStatusLogic l in r.Items)
            {
               legend.Add(FwConvert.ToTitleCase(l.InventoryStatus.ToLower()), l.Color);
            }
            legend.Add("QC Required", RwGlobals.QC_REQUIRED_COLOR);
            legend.Add("Suspended", RwGlobals.SUSPEND_COLOR);
            //await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "Y5fE4iVc3UhZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/item 
        [HttpGet]
        [FwControllerMethod(Id: "EsvBT0cfnwU2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/item/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "zjCQVTktDrdU", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/item/bybarcode 
        [HttpGet("bybarcode")]
        [FwControllerMethod(Id: "HtxHyTMbNpqOM", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ItemByBarCodeResponse>> GetOneByBarCodeAsync(string barCode)
        {
            return await ItemFunc.GetByBarCode(AppConfig, UserSession, barCode);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/item 
        [HttpPost]
        [FwControllerMethod(Id: "vf0mEqWKxcv3", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ItemLogic>> NewAsync([FromBody]ItemLogic l)
        {
            return await DoNewAsync<ItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/item/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "9oE9AHLNsB9kS", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ItemLogic>> EditAsync([FromRoute] string id, [FromBody]ItemLogic l)
        {
            return await DoEditAsync<ItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/item/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"NfaaIo4GI", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <ItemLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------
        // POST api/v1/item/validatecondition/browse
        [HttpPost("validatecondition/browse")]
        [FwControllerMethod(Id: "jFuceEMMuHYY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateConditionBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryConditionLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/item/validateinspectionvendor/browse
        [HttpPost("validateinspectionvendor/browse")]
        [FwControllerMethod(Id: "Tv3H7bLugJTs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInspectionVendorBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/item/validatemanufacturer/browse
        [HttpPost("validatemanufacturer/browse")]
        [FwControllerMethod(Id: "U34JNqYScjgd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateManufacturerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/item/validateremitcountry/browse
        [HttpPost("validatecountryoforigin/browse")]
        [FwControllerMethod(Id: "HkB3T4ReM9dX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCountryOfOriginBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
    }
}
