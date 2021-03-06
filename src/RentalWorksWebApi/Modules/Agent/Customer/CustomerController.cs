using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Settings.DepartmentSettings.Department;
using WebApi.Modules.Settings.CustomerSettings.CustomerType;
using WebApi.Modules.Settings.CustomerSettings.CustomerCategory;
using WebApi.Modules.Settings.AddressSettings.Country;
using WebApi.Modules.Settings.CustomerSettings.CustomerStatus;
using WebApi.Modules.Settings.PaymentSettings.PaymentTerms;
using WebApi.Modules.Settings.CustomerSettings.CreditStatus;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Settings.AddressSettings.State;

namespace WebApi.Modules.Agent.Customer
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"InSfo1f2lbFV")]
    public partial class CustomerController : AppDataController
    {
        public CustomerController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomerLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"7MoaoXPK0VIP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Pl5KjtaY6zH0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customer
        [HttpGet]
        [FwControllerMethod(Id:"a48vy0bxduhl", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<GetResponse<GetManyCustomerResponse>>> GetManyAsync([FromQuery]GetManyCustomerRequest request)
        {
            return await DoGetManyAsync<GetManyCustomerResponse>(request);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customer/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"yTB8kNFPBPSr", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CustomerLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<CustomerLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer
        [HttpPost]
        [FwControllerMethod(Id:"toRko5a4O4n1", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CustomerLogic>> NewAsync([FromBody]CustomerLogic l)
        {
            return await DoNewAsync<CustomerLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/custome/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "4AjxLis1DklOK", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CustomerLogic>> EditAsync([FromRoute] string id, [FromBody]CustomerLogic l)
        {
            return await DoEditAsync<CustomerLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customer/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"6p5DKezU9DgN", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<CustomerLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customer/lookup/officelocations
        [HttpGet("lookup/officelocations")]
        [FwControllerMethod(Id:"ydamcE1E1G9P", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<GetResponse<GetManyOfficeLocationModel>>> GetOfficeLocationsAsync([FromQuery]GetManyOfficeLocationRequest request)
        {
            //return await DoGetManyAsync<GetManyOfficeLocationModel>(request, typeof(OfficeLocationLogic));
            try
            {
                var customer = FwBusinessLogic.CreateBusinessLogic<CustomerLogic>(this.AppConfig, this.UserSession);
                var officeLocations = await customer.GetOfficeLocationsAsync(request);
                if (officeLocations == null)
                {
                    return NotFound();
                }
                return officeLocations;
            }
            catch(Exception ex)
            {
                return this.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/validatelocation/browse
        [HttpPost("validatelocation/browse")]
        [FwControllerMethod(Id: "yNlf9UabTUR2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/validatedepartment/browse
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "epyE1hEMELfV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/validatecustomertype/browse
        [HttpPost("validatecustomertype/browse")]
        [FwControllerMethod(Id: "wsSPageVvyup", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/validatecustomercategory/browse
        [HttpPost("validatecustomercategory/browse")]
        [FwControllerMethod(Id: "L4QC5Ql4eY49", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/validatecountry/browse
        [HttpPost("validatecountry/browse")]
        [FwControllerMethod(Id: "TCR6m9VWIySQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/validatecustomerstatus/browse
        [HttpPost("validatecustomerstatus/browse")]
        [FwControllerMethod(Id: "buaMP5GP5l37", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCustomerStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerStatusLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/validateparentcustomer/browse
        [HttpPost("validateparentcustomer/browse")]
        [FwControllerMethod(Id: "zhKwLHPom9AL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateParentCustomerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CustomerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/validatepaymentterms/browse
        [HttpPost("validatepaymentterms/browse")]
        [FwControllerMethod(Id: "PHpjtoeKJTFk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePaymentTermsBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTermsLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/validatecreditstatus/browse
        [HttpPost("validatecreditstatus/browse")]
        [FwControllerMethod(Id: "s8wyQktsyDnH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCreditStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CreditStatusLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/validateinsurancecompany/browse
        [HttpPost("validateinsurancecompany/browse")]
        [FwControllerMethod(Id: "vleyDPVAKeh3", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInsuranceCompanyBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/validatetaxstateofincorporation/browse
        [HttpPost("validatetaxstateofincorporation/browse")]
        [FwControllerMethod(Id: "6IplOv8OMzdM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTaxStateOfIncorporationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<StateLogic>(browseRequest);
        }
        // GET api/v1/customer/validations/managingdepartments
        //[HttpGet("validations/managingdepartments")]
        //[FwControllerMethod(Id:"EQB3smR5TUZ4")]
        //public async Task<ActionResult<GetResponse<GetManyDepartmentsValidationModel>>> GetManagingDepartmentsAsync([FromQuery]GetManyDepartmentsRequest request)
        //{
        //    try
        //    {
        //        var customer = FwBusinessLogic.CreateBusinessLogic<CustomerLogic>(this.AppConfig, this.UserSession);
        //        var officeLocations = await customer.GetOfficeLocationsAsync(request);
        //        if (officeLocations == null)
        //        {
        //            return NotFound();
        //        }
        //        return officeLocations;
        //    }
        //    catch(Exception ex)
        //    {
        //        return this.GetApiExceptionResult(ex);
        //    }
        //}
        //------------------------------------------------------------------------------------

        /*
        {
            "caption": "Office Location",
            "validationname": "OfficeLocationValidation",
            "datafield": "OfficeLocationId",
            "displayfield": "OfficeLocation"
        },
        {
            "caption": "Managing Department",
            "validationname": "DepartmentValidation",
            "datafield": "DepartmentId",
            "displayfield": "Department"
        },
        {
            "caption": "Type",
            "validationname": "CustomerTypeValidation",
            "datafield": "CustomerTypeId",
            "displayfield": "CustomerType"
        },
        {
            "caption": "Customer Category",
            "validationname": "CustomerCategoryValidation",
            "datafield": "CustomerCategoryId",
            "displayfield": "CustomerCategory"
        },
        {
            "caption": "Parent Customer",
            "validationname": "CustomerValidation",
            "datafield": "ParentCustomerId",
            "displayfield": "ParentCustomer"
        },
        {
            "caption": "Status",
            "validationname": "CustomerStatusValidation",
            "datafield": "CustomerStatusId",
            "displayfield": "CustomerStatus"
        },
        {
            "caption": "Country",
            "validationname": "CountryValidation",
            "datafield": "CountryId",
            "displayfield": "Country"
        },
        {
            "caption": "Payment Terms",
            "validationname": "PaymentTermsValidation",
            "datafield": "PaymentTermsId",
            "displayfield": "PaymentTerms"
        },
        {
            "caption": "Template",
            "validationname": "DiscountTemplateValidation",
            "datafield": "DiscountTemplateId",
            "displayfield": "DiscountTemplate"
        },
        {
            "caption": "Country",
            "validationname": "CountryValidation",
            "datafield": "BillToCountryId",
            "displayfield": "BillToCountry"
        },
        {
            "caption": "Status",
            "validationname": "CreditStatusValidation",
            "datafield": "CreditStatusId",
            "displayfield": "CreditStatus"
        },
        {
            "caption": "Name",
            "validationname": "VendorValidation",
            "datafield": "InsuranceCompanyId",
            "displayfield": "InsuranceCompany"
        },
        {
            "caption": "Country",
            "validationname": "CountryValidation",
            "datafield": "InsuranceCompanyCountryId",
            "displayfield": "InsuranceCompanyCountry"
        },
        {
            "caption": "State of Incorporation",
            "validationname": "StateValidation",
            "datafield": "TaxStateOfIncorporationId",
            "displayfield": "TaxStateOfIncorporation"
        },
        {
            "caption": "Tax Option",
            "validationname": "CustomerValidation",
            "datafield": "",
            "displayfield": "Customer"
        },
        {
            "caption": "Country",
            "validationname": "CountryValidation",
            "datafield": "ShipCountryId",
            "displayfield": "ShipCountry"
        }
        */
    }
}
