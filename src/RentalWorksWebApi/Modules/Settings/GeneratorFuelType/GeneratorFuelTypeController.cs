using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.GeneratorFuelType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class GeneratorFuelTypeController : AppDataController
    {
        public GeneratorFuelTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneratorFuelTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorfueltype/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{A159EF71-9F4D-40F6-8027-0AB1FC7A7CE0}")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(GeneratorFuelTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorfueltype
        [HttpGet]
        [Authorize(Policy = "{9B922EC1-D8C9-492E-91F4-E234E6AFAC64}")]
        public async Task<ActionResult<IEnumerable<GeneratorFuelTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorFuelTypeLogic>(pageno, pagesize, sort, typeof(GeneratorFuelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorfueltype/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{BC15A1F5-FAB4-4A2B-AD04-F9B7E3462560}")]
        public async Task<ActionResult<GeneratorFuelTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorFuelTypeLogic>(id, typeof(GeneratorFuelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorfueltype
        [HttpPost]
        [Authorize(Policy = "{90835994-22C3-4742-8634-44FEDC574D8B}")]
        public async Task<ActionResult<GeneratorFuelTypeLogic>> PostAsync([FromBody]GeneratorFuelTypeLogic l)
        {
            return await DoPostAsync<GeneratorFuelTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatorfueltype/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{9AFAD4EB-3629-4524-9688-7C7E46974AA8}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(GeneratorFuelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}