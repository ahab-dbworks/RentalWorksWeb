using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.MiscType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class MiscTypeController : AppDataController
    {
        public MiscTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MiscTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/misctype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(MiscTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misctype
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MiscTypeLogic>(pageno, pagesize, sort, typeof(MiscTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misctype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<MiscTypeLogic>(id, typeof(MiscTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/misctype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MiscTypeLogic l)
        {
            return await DoPostAsync<MiscTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/misctype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(MiscTypeLogic));
        }
        //------------------------------------------------------------------------------------
    }
}