using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Settings.ContactSettings.ContactTitle;
using WebLibrary;

namespace WebApi.Modules.HomeControls.CompanyContact
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"gQHuhVDA5Do2")]
    public class CompanyContactController : AppDataController
    {
        public CompanyContactController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CompanyContactLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/companycontact/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"Yux8wA5C7NXE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/companycontact/legend
        [HttpGet("legend")]
        [FwControllerMethod(Id: "2h1Al5QkdiMuk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
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
        // POST api/v1/modulename/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"oEuuwLalyvow", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/companycontact 
        [HttpGet]
        [FwControllerMethod(Id:"owToh3dDUn1R", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CompanyContactLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CompanyContactLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/companycontact/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"1U64Otc5kr1T", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<CompanyContactLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CompanyContactLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/companycontact 
        [HttpPost]
        [FwControllerMethod(Id:"v6r4EgZmO4Qf", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CompanyContactLogic>> NewAsync([FromBody]CompanyContactLogic l)
        {
            return await DoNewAsync<CompanyContactLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/companycontact/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "7YmpN2f65tigL", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CompanyContactLogic>> EditAsync([FromRoute] string id, [FromBody]CompanyContactLogic l)
        {
            return await DoEditAsync<CompanyContactLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/companycontact/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"859kQDas1ois", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CompanyContactLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/companycontact/validatecontacttitle/browse
        [HttpPost("validatecontacttitle/browse")]
        [FwControllerMethod(Id:"IYtoTwLT0GXB", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContactTitleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContactTitleLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
    }
}
