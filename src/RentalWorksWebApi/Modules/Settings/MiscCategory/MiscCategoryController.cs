using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.MiscCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class MiscCategoryController : AppDataController
    {
        public MiscCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MiscCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/misccategory/browse
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
        // GET api/v1/misccategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MiscCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MiscCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misccategory/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<MiscCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<MiscCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/misccategory
        [HttpPost]
        public async Task<ActionResult<MiscCategoryLogic>> PostAsync([FromBody]MiscCategoryLogic l)
        {
            return await DoPostAsync<MiscCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/misccategory/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}