using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SetOpening
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class SetOpeningController : AppDataController
    {
        public SetOpeningController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SetOpeningLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/setopening/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SetOpeningLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setopening
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SetOpeningLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SetOpeningLogic>(pageno, pagesize, sort, typeof(SetOpeningLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setopening/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<SetOpeningLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SetOpeningLogic>(id, typeof(SetOpeningLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/setopening
        [HttpPost]
        public async Task<ActionResult<SetOpeningLogic>> PostAsync([FromBody]SetOpeningLogic l)
        {
            return await DoPostAsync<SetOpeningLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/setopening/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SetOpeningLogic));
        }
        //------------------------------------------------------------------------------------
    }
}