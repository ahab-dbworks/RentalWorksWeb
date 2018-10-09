using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Home.Customer
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class CustomerController : AppDataController
    {
        public CustomerController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomerLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer/browse
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
        // GET api/v1/customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<CustomerLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customer/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<CustomerLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customer
        [HttpPost]
        public async Task<ActionResult<CustomerLogic>> PostAsync([FromBody]CustomerLogic l)
        {
            return await DoPostAsync<CustomerLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customer/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}