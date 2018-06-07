using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using FwStandard.BusinessLogic;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.Pricing
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class PricingController : AppDataController
    {
        public PricingController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PricingLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/pricing/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PricingLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/pricing/D00BYU6Z/B0029AY5/A001TSXJ   //masterid/warehouseid/currencyid
        [HttpGet("{masterid}/{warehouseid}/{currencyid}")]
        public async Task<IActionResult> GetAsync([FromRoute]string masterid, [FromRoute]string warehouseid, [FromRoute]string currencyid)
        {
            return await DoSpecialGetAsync<PricingLogic>(masterid, warehouseid, currencyid, typeof(PricingLogic));

        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/pricing/D00BYU6Z/B0029AY5   //masterid/warehouseid
        [HttpGet("{masterid}/{warehouseid}")]
        public async Task<IActionResult> GetAsync([FromRoute]string masterid, [FromRoute]string warehouseid)
        {
            return await DoSpecialGetAsync<PricingLogic>(masterid, warehouseid, "", typeof(PricingLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/pricing/D00BYU6Z    //masterid
        [HttpGet("{masterid}")]
        public async Task<IActionResult> GetAsync([FromRoute]string masterid)
        {
            return await DoSpecialGetAsync<PricingLogic>(masterid, "", "", typeof(PricingLogic));
        }
        //------------------------------------------------------------------------------------ 
        protected async Task<IActionResult> DoSpecialGetAsync<T>(string masterId, string warehouseId, string currencyId, Type type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                IDictionary<string, object> uniqueIds = new Dictionary<string, object>();
                uniqueIds.Add("MasterId", masterId);
                uniqueIds.Add("WarehouseId", warehouseId);
                uniqueIds.Add("CurrencyId", currencyId);

                BrowseRequest request = new BrowseRequest();
                request.pageno = 0;
                request.pagesize = 0;
                request.orderby = string.Empty;
                request.uniqueids = uniqueIds;
                FwBusinessLogic l = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
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