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

namespace WebApi.Modules.Agent.Customer
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"InSfo1f2lbFV")]
    public class CustomerController : AppDataController
    {
        public CustomerController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomerLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"7MoaoXPK0VIP")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Pl5KjtaY6zH0")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customer
        [HttpGet]
        [FwControllerMethod(Id:"a48vy0bxduhl")]
        public async Task<ActionResult<GetManyResponse<GetManyCustomerResponse>>> GetManyAsync([FromQuery]GetManyCustomerRequest request)
        {
            return await DoGetManyAsync<GetManyCustomerResponse>(request);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customer/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"yTB8kNFPBPSr")]
        public async Task<ActionResult<CustomerLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<CustomerLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer
        [HttpPost]
        [FwControllerMethod(Id:"toRko5a4O4n1")]
        public async Task<ActionResult<CustomerLogic>> PostAsync([FromBody]CustomerLogic l)
        {
            return await DoPostAsync<CustomerLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customer/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"6p5DKezU9DgN")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<CustomerLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customer/lookup/officelocations
        [HttpGet("lookup/officelocations")]
        [FwControllerMethod(Id:"ydamcE1E1G9P")]
        public async Task<ActionResult<GetManyResponse<GetManyOfficeLocationModel>>> GetOfficeLocationsAsync([FromQuery]GetManyOfficeLocationRequest request)
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
        // GET api/v1/customer/validations/managingdepartments
        //[HttpGet("validations/managingdepartments")]
        //[FwControllerMethod(Id:"EQB3smR5TUZ4")]
        //public async Task<ActionResult<GetManyResponse<GetManyDepartmentsValidationModel>>> GetManagingDepartmentsAsync([FromQuery]GetManyDepartmentsRequest request)
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
