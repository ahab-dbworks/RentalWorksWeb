using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.CustomerSettings.CustomerStatus;
using WebApi.Modules.Settings.DealSettings.DealStatus;
using WebApi.Modules.Settings.BillingCycleSettings.BillingCycle;
using WebApi.Modules.Settings.InventorySettings.Unit;
using WebApi.Modules.Settings.Rank;
using WebApi.Modules.Administrator.Group;
using WebApi.Modules.Settings.PaymentSettings.PaymentTerms;
using WebApi.Modules.Settings.CustomerSettings.CreditStatus;

namespace WebApi.Modules.Settings.SystemSettings.DefaultSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "6pvUgTaKPnjf3")]
    public class DefaultSettingsController : AppDataController
    {
        public DefaultSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DefaultSettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/defaultsettings/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "EF5HatKXARnq0", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/defaultsettings/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "l6KjAhDRXt3Dg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/defaultsettings 
        [HttpGet]
        [FwControllerMethod(Id: "2onkQXENye3oX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DefaultSettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DefaultSettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/defaultsettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "ICCNnubuyh036", ActionType: FwControllerActionTypes.View, ValidateSecurityGroup: false)]
        public async Task<ActionResult<DefaultSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DefaultSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/defaultsettings 
        [HttpPost]
        [FwControllerMethod(Id: "XhVAJzkuyDeWy", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DefaultSettingsLogic>> NewAsync([FromBody]DefaultSettingsLogic l)
        {
            return await DoNewAsync<DefaultSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/defaultsettings/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "10Lm04sc6C6Tb", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DefaultSettingsLogic>> EditAsync([FromRoute] string id, [FromBody]DefaultSettingsLogic l)
        {
            return await DoEditAsync<DefaultSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/defaultsettings/validatedefaultcustomerstatus/browse
        [HttpPost("validatedefaultcustomerstatus/browse")]
        [FwControllerMethod(Id: "Tq2q0xAsdIlY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultCustomerStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerStatusLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/defaultsettings/validatedefaultpaymentterms/browse
        [HttpPost("validatedefaultpaymentterms/browse")]
        [FwControllerMethod(Id: "KJayci0dUlZR3", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultPaymentTermsBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTermsLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/defaultsettings/validatedefaultcreditstatus/browse
        [HttpPost("validatedefaultcreditstatus/browse")]
        [FwControllerMethod(Id: "TX7v0LRtZaprB", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultCreditStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CreditStatusLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/defaultsettings/validatedefaultdealstatus/browse
        [HttpPost("validatedefaultdealstatus/browse")]
        [FwControllerMethod(Id: "DUF0rFdf1Snr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultDealStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealStatusLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/defaultsettings/validatedefaultdealbillingcycle/browse
        [HttpPost("validatedefaultdealbillingcycle/browse")]
        [FwControllerMethod(Id: "rTS88VjrtVrk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultDealBillingCycleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<BillingCycleLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/defaultsettings/validatedefaultunit/browse
        [HttpPost("validatedefaultunit/browse")]
        [FwControllerMethod(Id: "F99QTnaWNd2L", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultUnitBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UnitLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/defaultsettings/validatedefaultrank/browse
        [HttpPost("validatedefaultrank/browse")]
        [FwControllerMethod(Id: "wSHVw72bdL9g", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultRankBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RankLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/defaultsettings/validatedefaultnonrecurringbillingcycle/browse
        [HttpPost("validatedefaultnonrecurringbillingcycle/browse")]
        [FwControllerMethod(Id: "NBtsA3GREvvs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultNonRecurringBillingCycleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<BillingCycleLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/defaultsettings/validatedefaultcontactgroup/browse
        [HttpPost("validatedefaultcontactgroup/browse")]
        [FwControllerMethod(Id: "GxvORzNn1hLq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultContactGroupBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GroupLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
    }
}
