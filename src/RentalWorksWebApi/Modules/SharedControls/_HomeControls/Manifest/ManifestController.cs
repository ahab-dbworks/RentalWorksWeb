using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using WebLibrary;

namespace WebApi.Modules.HomeControls.Manifest
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "yMwoSvKvwAbbZ")]
    public class ManifestController : AppDataController
    {
        public ManifestController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ManifestLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/manifest/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "I4b2WEPOTVEOl", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/manifest/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "G1cIx9nCM8KJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Voided Items", RwGlobals.CONTRACT_ITEM_VOIDED_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/manifest/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "iwe21TwlAAcO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/manifest 
        [HttpGet]
        [FwControllerMethod(Id: "V624gbGkbmoD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ManifestLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ManifestLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/manifest/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "QxziTwrylmki", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ManifestLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ManifestLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/manifest 
        [HttpPost]
        [FwControllerMethod(Id: "ATumtiYUA0LLA", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ManifestLogic>> NewAsync([FromBody]ManifestLogic l)
        {
            return await DoNewAsync<ManifestLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/manifest/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "isGV3D6GTlUWB", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ManifestLogic>> EditAsync([FromRoute] string id, [FromBody]ManifestLogic l)
        {
            return await DoEditAsync<ManifestLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/manifest/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "hfYcAsk67QgY1", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ManifestLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
