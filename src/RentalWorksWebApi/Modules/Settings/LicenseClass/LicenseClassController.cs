using FwStandard.SqlServer;
using System.Collections.Generic;
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
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(LicenseClassLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/licenseclass
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LicenseClassLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<LicenseClassLogic>(pageno, pagesize, sort, typeof(LicenseClassLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/licenseclass/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<LicenseClassLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<LicenseClassLogic>(id, typeof(LicenseClassLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/licenseclass
        [HttpPost]
        public async Task<ActionResult<LicenseClassLogic>> PostAsync([FromBody]LicenseClassLogic l)
        {
            return await DoPostAsync<LicenseClassLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/licenseclass/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(LicenseClassLogic));
        }
        //------------------------------------------------------------------------------------
}
}