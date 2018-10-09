using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FacilityCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class FacilityCategoryController : AppDataController
    {
        public FacilityCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FacilityCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitycategory/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FacilityCategoryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitycategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacilityCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityCategoryLogic>(pageno, pagesize, sort, typeof(FacilityCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitycategory/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<FacilityCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityCategoryLogic>(id, typeof(FacilityCategoryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitycategory
        [HttpPost]
        public async Task<ActionResult<FacilityCategoryLogic>> PostAsync([FromBody]FacilityCategoryLogic l)
        {
            return await DoPostAsync<FacilityCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilitycategory/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FacilityCategoryLogic));
        }
        //------------------------------------------------------------------------------------
    }
}