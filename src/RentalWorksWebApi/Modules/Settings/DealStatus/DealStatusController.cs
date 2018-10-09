using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.DealStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class DealStatusController : AppDataController
    {
        public DealStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealstatus/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{051A9D01-5B55-4805-931A-75937FA04F33}")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DealStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealstatus
        [HttpGet]
        [Authorize(Policy = "{A17E8E69-1427-472E-9F15-EA4E31590ABE}")]
        public async Task<ActionResult<IEnumerable<DealStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealStatusLogic>(pageno, pagesize, sort, typeof(DealStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealstatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{A8FFA28F-E260-4B28-BB88-B4A5C2F0745B}")]
        public async Task<ActionResult<DealStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealStatusLogic>(id, typeof(DealStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealstatus
        [HttpPost]
        [Authorize(Policy = "{C0DAAFEF-B169-4153-A6D4-C3B6D3C07F94}")]
        public async Task<ActionResult<DealStatusLogic>> PostAsync([FromBody]DealStatusLogic l)
        {
            return await DoPostAsync<DealStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/dealstatus/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{0C9DE590-155F-459E-B33E-90AEABFB5F50}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DealStatusLogic));
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}