using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.Manifest
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
        [FwControllerMethod(Id: "I4b2WEPOTVEOl")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/manifest/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "iwe21TwlAAcO")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/manifest 
        [HttpGet]
        [FwControllerMethod(Id: "V624gbGkbmoD")]
        public async Task<ActionResult<IEnumerable<ManifestLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ManifestLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/manifest/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "QxziTwrylmki")]
        public async Task<ActionResult<ManifestLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ManifestLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/manifest 
        [HttpPost]
        [FwControllerMethod(Id: "ATumtiYUA0LLA")]
        public async Task<ActionResult<ManifestLogic>> PostAsync([FromBody]ManifestLogic l)
        {
            return await DoPostAsync<ManifestLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/manifest/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "hfYcAsk67QgY1")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ManifestLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
