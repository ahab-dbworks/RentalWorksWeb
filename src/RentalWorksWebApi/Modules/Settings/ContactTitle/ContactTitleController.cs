using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace WebApi.Modules.Settings.ContactTitle
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class ContactTitleController : AppDataController
    {
        public ContactTitleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContactTitleLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/contacttitle/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{ADCFFDE3-E33B-4BE8-9B9C-B040617A332E}")]
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
        // GET api/v1/contacttitle
        [HttpGet]
        [Authorize(Policy = "{9E71C0F1-70A3-494B-A871-FFE69100BBB3}")]
        public async Task<ActionResult<IEnumerable<ContactTitleLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<ContactTitleLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contacttitle/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{470B79CB-242D-4104-AF97-6416283CBCA8}")]
        public async Task<ActionResult<ContactTitleLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<ContactTitleLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contacttitle
        [HttpPost]
        [Authorize(Policy = "{194C54FA-A4AC-4CD9-9B23-16BB87B0B214}")]
        public async Task<ActionResult<ContactTitleLogic>> PostAsync([FromBody]ContactTitleLogic l)
        {
            return await DoPostAsync<ContactTitleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/contacttitle/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{16D7F840-B67F-497B-804B-F806B413F806}")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}