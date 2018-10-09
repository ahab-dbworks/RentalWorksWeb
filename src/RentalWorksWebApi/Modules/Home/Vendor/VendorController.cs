using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Home.Vendor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class VendorController : AppDataController
    {
        public VendorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VendorLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendorLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<VendorLogic>(pageno, pagesize, sort, typeof(VendorLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendor/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<VendorLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<VendorLogic>(id, typeof(VendorLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendor
        [HttpPost]
        public async Task<ActionResult<VendorLogic>> PostAsync([FromBody]VendorLogic l)
        {
            return await DoPostAsync<VendorLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendor/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(VendorLogic));
        }
        //------------------------------------------------------------------------------------
    }
}