using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FacilityType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class FacilityTypeController : AppDataController
    {
        public FacilityTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FacilityTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitytype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FacilityTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitytype
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityTypeLogic>(pageno, pagesize, sort, typeof(FacilityTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitytype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityTypeLogic>(id, typeof(FacilityTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitytype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]FacilityTypeLogic l)
        {
            return await DoPostAsync<FacilityTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilitytype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FacilityTypeLogic));
        }
        //------------------------------------------------------------------------------------
    }
}