using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Home.VendorNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class VendorNoteController : AppDataController
    {
        public VendorNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorNoteLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendornote/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VendorNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendornote
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendorNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorNoteLogic>(pageno, pagesize, sort, typeof(VendorNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendornote/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<VendorNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorNoteLogic>(id, typeof(VendorNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendornote
        [HttpPost]
        public async Task<ActionResult<VendorNoteLogic>> PostAsync([FromBody]VendorNoteLogic l)
        {
            return await DoPostAsync<VendorNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendornote/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VendorNoteLogic));
        }
        //------------------------------------------------------------------------------------
    }
}