using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Settings.ContactSettings.ContactTitle;
using WebApi.Modules.Settings.AddressSettings.Country;
//using WebApi.Modules.HomeControls.CompanyContact;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using WebApi;
using WebApi.Modules.HomeControls.CompanyContact;
using WebApi.Modules.Settings.DepartmentSettings.Department;

namespace WebApi.Modules.Agent.Contact
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"9ykTwUXTet46")]
    //[FwModuleDependency(Id: "U8Zlahz3ke9i", Name: "Agent>Order", Description: "Required to have Orders tab.")]
    //[FwModuleDependency(Id: "jFkSBEur1dluU", Name: "Agent>Quote", Description: "Required to have Quotes tab.")]
    public partial class ContactController : AppDataController
    {
        public ContactController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContactLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/contact/browse
        /// <summary>
        /// Retrieves a list of records
        /// </summary>
        /// <remarks>Use the pageno and pagesize query string parameters to limit the result set and improve performance.</remarks>
        /// <response code="200">Successfully retrieves records.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpPost("browse")]
        [Produces(typeof(FwJsonDataTable))]
        [SwaggerResponse(200, Type = typeof(FwJsonDataTable))]
        [SwaggerResponse(500, Type = typeof(FwApiException))]
        //[ApiExplorerSettings(IgnoreApi=true)]
        [FwControllerMethod(Id:"oVGkoWf7WoJo", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/contact/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "wfyh5bTW7cCuO", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Crew", RwGlobals.CONTACT_TYPE_CREW_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contact/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"nhBg3nFWSLBS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contact
        /// <summary>
        /// REST: Retrieves a list of records
        /// </summary>
        /// <remarks>Use the pageno and pagesize query string parameters to limit the result set and improve performance.</remarks>
        /// <response code="200">Successfully retrieves records.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpGet]
        [Produces(typeof(List<ContactLogic>))]
        [SwaggerResponse(200, Type = typeof(List<ContactLogic>))]
        [SwaggerResponse(500, Type = typeof(FwApiException))]
        [FwControllerMethod(Id:"tGV9key7gxD6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ContactLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ContactLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contact/A0000001
        [HttpGet("{id}")]
        [Produces(typeof(ContactLogic))]
        [SwaggerResponse(200, Type = typeof(ContactLogic))]
        [FwControllerMethod(Id:"6EMCklyR6Ltw", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ContactLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContactLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contact
        [HttpPost]
        [FwControllerMethod(Id:"3qX4qzUxUYlL", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ContactLogic>> NewAsync([FromBody]ContactLogic l)
        {
            return await DoNewAsync<ContactLogic>(l);
        }
        //--------------------------------------------------------------------------// 
        //Put api/v1/contact/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id:"W2eHNpadW6jj", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ContactLogic>> EditAsync([FromRoute] string id, [FromBody]ContactLogic l)
        {
            return await DoEditAsync<ContactLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/contact/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"8PPmwft0gjJw", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ContactLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contact/validatecontacttitle/browse
        [HttpPost("validatecontacttitle/browse")]
        [FwControllerMethod(Id:"orfp0WXyHV1P", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContactTitleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContactTitleLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contact/validatecountry/browse
        [HttpPost("validatecountry/browse")]
        [FwControllerMethod(Id:"OHcallxEmBIt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contact/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id:"4THbrHEHMvAO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CompanyContactLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contact/validatelocation/browse 
        [HttpPost("validatelocation/browse")]
        [FwControllerMethod(Id: "C7OS3V9Qo5R4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contact/validatewarehouse/browse 
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "OWQ8pj7IdPLr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/contact/companycontact/legend
        [HttpGet("companycontact/legend")]
        [FwControllerMethod(Id: "vLEnNEJc5EcY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<Dictionary<string, string>>> CompanyContact_GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Lead", RwGlobals.COMPANY_TYPE_LEAD_COLOR);
            legend.Add("Prospect", RwGlobals.COMPANY_TYPE_PROSPECT_COLOR);
            legend.Add("Customer", RwGlobals.COMPANY_TYPE_CUSTOMER_COLOR);
            legend.Add("Deal", RwGlobals.COMPANY_TYPE_DEAL_COLOR);
            legend.Add("Vendor", RwGlobals.COMPANY_TYPE_VENDOR_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contact/validatedepartment/browse
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id:"nkfAopV2ENNt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            browseRequest.searchfields.Add("Inactive");
            browseRequest.searchfieldvalues.Add("T");
            browseRequest.searchfieldoperators.Add("<>");
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
    }
}
