using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.Address
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class AddressController : AppDataController
    {
        public AddressController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AddressLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/address/browse 
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
        // GET api/v1/address 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AddressLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/address/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AddressLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/address 
        [HttpPost]
        public async Task<ActionResult<AddressLogic>> PostAsync([FromBody]AddressLogic l)
        {
            return await DoPostAsync<AddressLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/address/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
