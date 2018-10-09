using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Attribute
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class AttributeController : AppDataController
    {
        public AttributeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AttributeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/attribute/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(AttributeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/attribute
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AttributeLogic>(pageno, pagesize, sort, typeof(AttributeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/attribute/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AttributeLogic>(id, typeof(AttributeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/attribute
        [HttpPost]
        public async Task<ActionResult<AttributeLogic>> PostAsync([FromBody]AttributeLogic l)
        {
            return await DoPostAsync<AttributeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/attribute/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(AttributeLogic));
        }
        //------------------------------------------------------------------------------------
    }
}