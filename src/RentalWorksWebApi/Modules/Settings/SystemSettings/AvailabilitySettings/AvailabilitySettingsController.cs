using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using WebApi.Modules.Settings.CustomerSettings.CustomerStatus;
using WebApi.Modules.Settings.DealSettings.DealStatus;
using WebApi.Modules.Settings.BillingCycleSettings.BillingCycle;
using WebApi.Modules.Settings.InventorySettings.Unit;
using WebApi.Modules.Settings.Rank;
using WebApi.Modules.Administrator.Group;

namespace WebApi.Modules.Settings.SystemSettings.AvailabilitySettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "UXYMLInJl6JMP")]
    public class AvailabilitySettingsController : AppDataController
    {
        public AvailabilitySettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AvailabilitySettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/availabilitysettings/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "mDoMpfZJiGN9N", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/availabilitysettings/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "NvxY1l4VAIGdt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/availabilitysettings 
        [HttpGet]
        [FwControllerMethod(Id: "vY1IOPjEs7oS8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<AvailabilitySettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AvailabilitySettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/availabilitysettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "x2qH5CpVJCLJO", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<AvailabilitySettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AvailabilitySettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/availabilitysettings 
        [HttpPost]
        [FwControllerMethod(Id: "15NYtyWYoQTYV", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<AvailabilitySettingsLogic>> NewAsync([FromBody]AvailabilitySettingsLogic l)
        {
            return await DoNewAsync<AvailabilitySettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/availabilitysettings/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "uEqrPXxnOYQZa", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<AvailabilitySettingsLogic>> EditAsync([FromRoute] string id, [FromBody]AvailabilitySettingsLogic l)
        {
            return await DoEditAsync<AvailabilitySettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/availabilitysettings/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "xBQUWdcisNiim", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <AvailabilitySettingsLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/availabilitysettings/validatedefaultcustomerstatus/browse
        [HttpPost("validatedefaultcustomerstatus/browse")]
        [FwControllerMethod(Id: "px5v4ph4a3ds", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultCustomerStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerStatusLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/availabilitysettings/validatedefaultdealstatus/browse
        [HttpPost("validatedefaultdealstatus/browse")]
        [FwControllerMethod(Id: "4SxUHkhbxqq9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultDealStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealStatusLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/availabilitysettings/validatedefaultdealbillingcycle/browse
        [HttpPost("validatedefaultdealbillingcycle/browse")]
        [FwControllerMethod(Id: "LntN7WpGYcdd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultDealBillingCycleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<BillingCycleLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/availabilitysettings/validatedefaultunit/browse
        [HttpPost("validatedefaultunit/browse")]
        [FwControllerMethod(Id: "7kWpoAsMtcT9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultUnitBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UnitLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/availabilitysettings/validatedefaultrank/browse
        [HttpPost("validatedefaultrank/browse")]
        [FwControllerMethod(Id: "b2OQ9K28g5SQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultRankBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RankLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/availabilitysettings/validatedefaultnonrecurringbillingcycle/browse
        [HttpPost("validatedefaultnonrecurringbillingcycle/browse")]
        [FwControllerMethod(Id: "6D6nNGC1PuWw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultNonRecurringBillingCycleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<BillingCycleLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/availabilitysettings/validatedefaultcontactgroup/browse
        [HttpPost("validatedefaultcontactgroup/browse")]
        [FwControllerMethod(Id: "x2W78MPJDuLf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultContactGroupBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GroupLogic>(browseRequest);
        }
    }
}
