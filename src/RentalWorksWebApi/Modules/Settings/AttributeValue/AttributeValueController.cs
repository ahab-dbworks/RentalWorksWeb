using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.AttributeValue
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class AttributeValueController : AppDataController
    {
        public AttributeValueController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AttributeValueLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/attributevalue/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(AttributeValueLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/attributevalue
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AttributeValueLogic>(pageno, pagesize, sort, typeof(AttributeValueLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/attributevalue/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AttributeValueLogic>(id, typeof(AttributeValueLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/attributevalue
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]AttributeValueLogic l)
        {
            return await DoPostAsync<AttributeValueLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/attributevalue/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(AttributeValueLogic));
        }
        //------------------------------------------------------------------------------------
    }
}