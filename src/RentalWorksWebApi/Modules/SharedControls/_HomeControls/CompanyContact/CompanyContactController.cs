using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebLibrary;

namespace WebApi.Modules.Home.CompanyContact
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
        [FwControllerMethod(Id:"Yux8wA5C7NXE")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/companycontact/legend
        [HttpGet("legend")]
        [FwControllerMethod(Id: "2h1Al5QkdiMuk")]
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
        [FwControllerMethod(Id:"oEuuwLalyvow")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/companycontact 
        [HttpGet]
        [FwControllerMethod(Id:"owToh3dDUn1R")]
        public async Task<ActionResult<IEnumerable<CompanyContactLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CompanyContactLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/companycontact/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"1U64Otc5kr1T")]
        public async Task<ActionResult<CompanyContactLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CompanyContactLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/companycontact 
        [HttpPost]
        [FwControllerMethod(Id:"v6r4EgZmO4Qf")]
        public async Task<ActionResult<CompanyContactLogic>> PostAsync([FromBody]CompanyContactLogic l)
        {
            return await DoPostAsync<CompanyContactLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/companycontact/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"859kQDas1ois")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CompanyContactLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
