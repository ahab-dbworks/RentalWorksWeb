using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VendorClass
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class VendorClassController : AppDataController
    {
        public VendorClassController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorClassLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorclass/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorclass
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendorClassLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorClassLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorclass/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<VendorClassLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorClassLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorclass
        [HttpPost]
        public async Task<ActionResult<VendorClassLogic>> PostAsync([FromBody]VendorClassLogic l)
        {
            return await DoPostAsync<VendorClassLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendorclass/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
}
}