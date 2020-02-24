using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Settings.VendorSettings.VendorClass;
using WebApi.Modules.Agent.Customer;
using WebApi.Modules.Settings.AddressSettings.Country;
using WebApi.Modules.Settings.RateType;
using WebApi.Modules.Settings.BillingCycleSettings.BillingCycle;
using WebApi.Modules.Settings.PaymentSettings.PaymentTerms;
using WebApi.Modules.Settings.VendorSettings.OrganizationType;
using WebApi.Modules.Settings.PoSettings.PoClassification;
using WebApi.Modules.Settings.CurrencySettings.Currency;

namespace WebApi.Modules.Agent.Vendor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"cwytGLEcUzJdn")]
    public partial class VendorController : AppDataController
    {
        public VendorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"qISJ4BW5wxoWa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ZUpDxBZVU2ahJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendor
        [HttpGet]
        [FwControllerMethod(Id:"AIdpqgM5qd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VendorLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<VendorLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendor/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"vkY3kGToPg", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<VendorLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<VendorLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor
        [HttpPost]
        [FwControllerMethod(Id:"PNyHqNlPW2crp", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VendorLogic>> NewAsync([FromBody]VendorLogic l)
        {
            return await DoNewAsync<VendorLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/vendo/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "WSr25oUTzQPKK", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VendorLogic>> EditAsync([FromRoute] string id, [FromBody]VendorLogic l)
        {
            return await DoEditAsync<VendorLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendor/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"5iVmNJZGN9LDk", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<VendorLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/validateofficelocation/browse
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "g71WzhrVwBxD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/validatevendorclass/browse
        [HttpPost("validatevendorclass/browse")]
        [FwControllerMethod(Id: "a8yMblJ74b6z", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateVendorClassBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorClassLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/validatecustomer/browse
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "2IYv7p2oANMM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/validatecountry/browse
        [HttpPost("validatecountry/browse")]
        [FwControllerMethod(Id: "5tb15GTkGX6c", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/validaterate/browse
        [HttpPost("validaterate/browse")]
        [FwControllerMethod(Id: "XFOvQYgfbC50", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRateBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RateTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/validatebillingcycle/browse
        [HttpPost("validatebillingcycle/browse")]
        [FwControllerMethod(Id: "t5XgIldwvURR", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateBillingCycleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<BillingCycleLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/validatepaymentterms/browse
        [HttpPost("validatepaymentterms/browse")]
        [FwControllerMethod(Id: "hRcxsU2dMVHB", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePaymentTermsBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTermsLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/validateorganizationtype/browse
        [HttpPost("validateorganizationtype/browse")]
        [FwControllerMethod(Id: "KGGLHHLDykI9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrganizationTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrganizationTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/validatepoclass/browse
        [HttpPost("validatepoclass/browse")]
        [FwControllerMethod(Id: "RMLxTJQdX7n8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePoClassBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PoClassificationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/validatecurrency/browse
        [HttpPost("validatecurrency/browse")]
        [FwControllerMethod(Id: "VSWqJ1oGyxGb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCurrencyBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CurrencyLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/validateremitcountry/browse
        [HttpPost("validateremitcountry/browse")]
        [FwControllerMethod(Id: "MCXXjot6lcPO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRemitCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
    }
}
