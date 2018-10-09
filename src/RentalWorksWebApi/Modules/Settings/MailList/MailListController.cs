using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.MailList
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class MailListController : AppDataController
    {
        public MailListController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MailListLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/maillist/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(MailListLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/maillist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MailListLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<MailListLogic>(pageno, pagesize, sort, typeof(MailListLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/maillist/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<MailListLogic>> GetAsync(string id)
        {
            return await DoGetAsync<MailListLogic>(id, typeof(MailListLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/maillist
        [HttpPost]
        public async Task<ActionResult<MailListLogic>> PostAsync([FromBody]MailListLogic l)
        {
            return await DoPostAsync<MailListLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/maillist/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(MailListLogic));
        }
        //------------------------------------------------------------------------------------
    }
}