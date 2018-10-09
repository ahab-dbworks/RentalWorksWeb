using FwStandard.SqlServer;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Home.Taxable
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class TaxableController : AppDataController
    {
        public TaxableController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(TaxableLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/taxable/browse 
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
        // GET api/v1/taxable/B003MZ45/F01BQV9J   //masterid/locationid
        [HttpGet("{masterid}/{locationid}")]
        public async Task<ActionResult<IEnumerable<TaxableLogic>>> GetOneAsync([FromRoute]string masterid, [FromRoute]string locationid)
        {
            return await DoSpecialGetAsync<TaxableLogic>(masterid, locationid);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/taxable/B003MZ45   //masterid
        [HttpGet("{masterid}")]
        public async Task<ActionResult<IEnumerable<TaxableLogic>>> GetOneAsync([FromRoute]string masterid)
        {
            return await DoSpecialGetAsync<TaxableLogic>(masterid, "");
        }
        //------------------------------------------------------------------------------------ 
        protected async Task<ActionResult<IEnumerable<T>>> DoSpecialGetAsync<T>(string masterId, string locationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                IDictionary<string, object> uniqueIds = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(masterId))
                {
                    uniqueIds.Add("MasterId", masterId);
                }
                if (!string.IsNullOrEmpty(locationId))
                {
                    uniqueIds.Add("LocationId", locationId);
                }

                BrowseRequest request = new BrowseRequest();
                request.pageno = 0;
                request.pagesize = 0;
                request.orderby = string.Empty;
                request.uniqueids = uniqueIds;
                FwBusinessLogic l = CreateBusinessLogic(logicType, this.AppConfig, this.UserSession);
                IEnumerable<T> records = await l.SelectAsync<T>(request);
                return new OkObjectResult(records);
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------



    }
}