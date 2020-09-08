using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Settings.TaxSettings.TaxOption;

namespace WebApi.Modules.Settings.FacilitySettings.Venue
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "dzfHYYraDfbPx")]
    public class VenueController : AppDataController
    {
        public VenueController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VenueLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/venue/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "jY8EJtduGb0bD", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/venue/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "ZYTpLq5HM5yDa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/venue 
        [HttpGet]
        [FwControllerMethod(Id: "EEivE4hPVj4Ex", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VenueLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VenueLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/venue/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "q2UZyHb73Zisg", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<VenueLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VenueLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/venue 
        [HttpPost]
        [FwControllerMethod(Id: "47QCIdvy7DuWr", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VenueLogic>> NewAsync([FromBody]VenueLogic l)
        {
            return await DoNewAsync<VenueLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/venue/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "t9hMz8KAgvmpW", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VenueLogic>> EditAsync([FromRoute] string id, [FromBody]VenueLogic l)
        {
            return await DoEditAsync<VenueLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/venue/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "jRpxYR7R6BygS", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VenueLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/venue/validateofficelocation/browse
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "Bqgn2dt7MsRkF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/venue/validatetaxoption/browse 
        [HttpPost("validatetaxoption/browse")]
        [FwControllerMethod(Id: "O55aALVZz9Ft7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTaxOptionBrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<TaxOptionLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
