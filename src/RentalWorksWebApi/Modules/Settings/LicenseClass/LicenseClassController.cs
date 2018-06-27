using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.LicenseClass
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class LicenseClassController : AppDataController
    {
        public LicenseClassController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(LicenseClassLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/licenseclass/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(LicenseClassLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/licenseclass
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<LicenseClassLogic>(pageno, pagesize, sort, typeof(LicenseClassLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/licenseclass/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<LicenseClassLogic>(id, typeof(LicenseClassLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/licenseclass
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]LicenseClassLogic l)
        {
            return await DoPostAsync<LicenseClassLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/licenseclass/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(LicenseClassLogic));
        }
        //------------------------------------------------------------------------------------
}
}