using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.DepartmentSettings.Department;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Agent.Customer;
using WebApi.Modules.Settings.DealSettings.DealType;
using WebApi.Modules.Settings.DealSettings.DealClassification;
using WebApi.Modules.Settings.DealSettings.ProductionType;
using WebApi.Modules.Settings.AddressSettings.Country;
using WebApi.Modules.Settings.DealSettings.DealStatus;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Settings.BillingCycleSettings.BillingCycle;
using WebApi.Modules.Settings.PaymentSettings.PaymentType;
using WebApi.Modules.Settings.PaymentSettings.PaymentTerms;
using WebApi.Modules.Settings.RateType;
using WebApi.Modules.Agent.Contact;
using WebApi.Modules.Settings.CustomerSettings.CreditStatus;
using WebApi.Modules.Agent.Vendor;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Agent.Deal
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"8WdRib388fFF")]
    public partial class DealController : AppDataController
    {
        public DealController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/deal/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"2GGRtZiCZeW7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"FHXGMbYmyiFG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/deal 
        [HttpGet]
        [FwControllerMethod(Id:"xQzGjNkxeW2l", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DealLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/deal/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"flXfXIRxf178", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<DealLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/deal 
        [HttpPost]
        [FwControllerMethod(Id:"cEdOt5p0EujB", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DealLogic>> NewAsync([FromBody]DealLogic l)
        {
            return await DoNewAsync<DealLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/deal/many
        [HttpPost("many")]
        [FwControllerMethod(Id: "IlYaNj4Ib2wv")]
        public async Task<List<ActionResult<DealLogic>>> PostAsync([FromBody] List<DealLogic> l)
        {
            FwBusinessLogicList l2 = new FwBusinessLogicList();
            l2.AddRange(l);
            return await DoPostAsync<DealLogic>(l2);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/deal/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "USCSo3Zj66sBe", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DealLogic>> EditAsync([FromRoute] string id, [FromBody]DealLogic l)
        {
            return await DoEditAsync<DealLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/deal/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"0cMsOO5hEIR3", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DealLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/deal/validatecustomer/browse
        [HttpPost("validatecustomer/browse")]
        [FwControllerMethod(Id: "3lXummDAszQe", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validateofficelocation/browse
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "NqH5oU6KpY5V", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validatedepartment/browse
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "G6IywgsSeywP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validatedealtype/browse
        [HttpPost("validatedealtype/browse")]
        [FwControllerMethod(Id: "McNgvruzrfxE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validatedealclassification/browse
        [HttpPost("validatedealclassification/browse")]
        [FwControllerMethod(Id: "zUzmFkaepbXp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealClassificationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealClassificationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validateproductiontype/browse
        [HttpPost("validateproductiontype/browse")]
        [FwControllerMethod(Id: "Neemg89p41tk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProductionTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ProductionTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validatecsr/browse
        [HttpPost("validatecsr/browse")]
        [FwControllerMethod(Id: "gYq8EGFBJd8K", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCsrBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validateagent/browse
        [HttpPost("validateagent/browse")]
        [FwControllerMethod(Id: "Qgn4xsAKpzDq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAgentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validateprojectmanager/browse
        [HttpPost("validateprojectmanager/browse")]
        [FwControllerMethod(Id: "mjstfhKHCoeQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProjectManagerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validatecountry/browse
        [HttpPost("validatecountry/browse")]
        [FwControllerMethod(Id: "4bKSGwPJgEE9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validatedealstatus/browse
        [HttpPost("validatedealstatus/browse")]
        [FwControllerMethod(Id: "Uf1MAR6Rv9Wv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealStatusLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validatebillingcycle/browse
        [HttpPost("validatebillingcycle/browse")]
        [FwControllerMethod(Id: "omvBDBhDcG9v", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateBillingCycleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<BillingCycleLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validatepaymenttype/browse
        [HttpPost("validatepaymenttype/browse")]
        [FwControllerMethod(Id: "QRe2T4lROsIa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePaymentTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validatepaymentterms/browse
        [HttpPost("validatepaymentterms/browse")]
        [FwControllerMethod(Id: "r8gTrxv4ppEu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePaymentTermsBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTermsLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validateorderrate/browse
        [HttpPost("validateorderrate/browse")]
        [FwControllerMethod(Id: "bogPwa7ofF5S", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderRateBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RateTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validatesalesrepresentative/browse
        [HttpPost("validatesalesrepresentative/browse")]
        [FwControllerMethod(Id: "Ne8B0eQ6NvkE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSalesRepresentativeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContactLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validatecreditstatus/browse
        [HttpPost("validatecreditstatus/browse")]
        [FwControllerMethod(Id: "te1kHnk8sFA9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCreditStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CreditStatusLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validateinsurancecompany/browse
        [HttpPost("validateinsurancecompany/browse")]
        [FwControllerMethod(Id: "8KSRWLsn5xch", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInsuranceCompanyBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/validateshipcountry/browse
        [HttpPost("validateshipcountry/browse")]
        [FwControllerMethod(Id: "b27nOHP7YCK5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateShipCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/deal/A0000001/copycontactsfromcustomer
        [HttpPost("{id}/copycontactsfromcustomer")]
        [FwControllerMethod(Id: "Lk2dMECX4zoab", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<CopyContactsFromCustomerResponse>> CopyContactsFromCustomerAsync([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DealLogic d = new DealLogic();
            d.SetDependencies(AppConfig, UserSession);
            d.DealId = id;
            bool exists = await d.LoadAsync<DealLogic>();
            if (exists)
            {
                CopyContactsFromCustomerRequest request = new CopyContactsFromCustomerRequest();
                request.Deal = d;
                return await DealFunc.CopyContactsFromCustomerAsync(AppConfig, UserSession, request);
            }
            else
            {
                return NotFound();
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
