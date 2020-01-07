using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Modules.Settings.ContactSettings.ContactTitle;
using WebApi.Modules.Settings.AddressSettings.Country;

namespace WebApi.Modules.Settings.LaborSettings.Crew
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"7myCbtZNx85m")]
    public class CrewController : AppDataController
    {
        public CrewController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CrewLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crew/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"jE78nFz0vv8x", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"9SAiqaoxW6Cb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crew 
        [HttpGet]
        [FwControllerMethod(Id:"Sr3ijgq1cDQi", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CrewLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crew/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"ta2ZqYaHLnKV", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CrewLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crew 
        [HttpPost]
        [FwControllerMethod(Id:"tTbheB1sPVfV", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CrewLogic>> NewAsync([FromBody]CrewLogic l)
        {
            return await DoNewAsync<CrewLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/crew/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "51gsxVmbS4JMx", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CrewLogic>> EditAsync([FromRoute] string id, [FromBody]CrewLogic l)
        {
            return await DoEditAsync<CrewLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/crew/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"VycXmuNzEsp1", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CrewLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/settings/validatecontacttitle/browse
        [HttpPost("validatecontacttitle/browse")]
        [FwControllerMethod(Id: "GuPSLlnwDRQl", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContactTitleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContactTitleLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validatecountry/browse
        [HttpPost("validatecountry/browse")]
        [FwControllerMethod(Id: "CtgcJOr7MRwF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
    }
}
