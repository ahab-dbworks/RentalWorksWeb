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
    public class TaxableController : AppDataController
    {
        public TaxableController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/taxable/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(TaxableLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/taxable/B003MZ45/F01BQV9J   //masterid/locationid
        [HttpGet("{masterid}/{locationid}")]
        public async Task<IActionResult> GetAsync([FromRoute]string masterid, [FromRoute]string locationid)
        {
            return await DoSpecialGetAsync<TaxableLogic>(masterid, locationid, typeof(TaxableLogic));

        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/taxable/B003MZ45   //masterid
        [HttpGet("{masterid}")]
        public async Task<IActionResult> GetAsync([FromRoute]string masterid)
        {
            return await DoSpecialGetAsync<TaxableLogic>(masterid, "", typeof(TaxableLogic));
        }
        //------------------------------------------------------------------------------------ 
        protected async Task<IActionResult> DoSpecialGetAsync<T>(string masterId, string locationId, Type type)
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