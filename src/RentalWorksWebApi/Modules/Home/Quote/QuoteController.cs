using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using WebLibrary;
using WebApi.Modules.Home.Order;

namespace WebApi.Modules.Home.Quote
{

    [Route("api/v1/[controller]")]
    public class QuoteController : AppDataController
    {
        public QuoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/browse
        [HttpPost("browse")]
        public async Task<IActionResult> Browse([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(QuoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/copy/A0000001
        [HttpPost("copy/{id}")]
        public async Task<IActionResult> Copy([FromRoute]string id, [FromBody] QuoteOrderCopyRequest copyRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                QuoteLogic l = new QuoteLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<QuoteLogic>(ids))
                {
                    if (copyRequest.CopyToType.Equals(RwConstants.ORDER_TYPE_QUOTE))
                    {
                        QuoteLogic lCopy = (QuoteLogic)await l.CopyAsync<OrderBaseLogic>(copyRequest);
                        return new OkObjectResult(lCopy);
                    }
                    else
                    {
                        OrderLogic lCopy = (OrderLogic)await l.CopyAsync<OrderBaseLogic>(copyRequest);
                        return new OkObjectResult(lCopy);
                    }
                }
                else
                {
                    return NotFound();
                }
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
        // GET api/v1/quote
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<QuoteLogic>(pageno, pagesize, sort, typeof(QuoteLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/quote/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<QuoteLogic>(id, typeof(QuoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]QuoteLogic l)
        {
            return await DoPostAsync<QuoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/quote/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(QuoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}