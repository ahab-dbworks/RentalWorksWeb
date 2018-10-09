using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.ContactNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class ContactNoteController : AppDataController
    {
        public ContactNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContactNoteLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contactnote/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ContactNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/contactnote 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ContactNoteLogic>(pageno, pagesize, sort, typeof(ContactNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/contactnote/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContactNoteLogic>(id, typeof(ContactNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contactnote 
        [HttpPost]
        public async Task<ActionResult<ContactNoteLogic>> PostAsync([FromBody]ContactNoteLogic l)
        {
            return await DoPostAsync<ContactNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/contactnote/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ContactNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}
