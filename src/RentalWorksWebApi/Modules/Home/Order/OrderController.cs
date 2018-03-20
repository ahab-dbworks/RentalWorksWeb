using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.Order
{
    [Route("api/v1/[controller]")]
    public class OrderController : AppDataController
    {
        public OrderController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/browse
        [HttpPost("browse")]
        public async Task<IActionResult> Browse([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/copy
        [HttpPost("copy/{id}")]
        public async Task<IActionResult> Copy([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                OrderLogic l = new OrderLogic();
                l.AppConfig = AppConfig;
                l.UserSession = UserSession;
                if (await l.LoadAsync<OrderLogic>(ids))
                {
                    OrderLogic lCopy = await l.CopyAsync<OrderLogic>();
                    return new OkObjectResult(lCopy);
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
    // GET api/v1/order
    [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderLogic>(pageno, pagesize, sort, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderLogic>(id, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderLogic l)
        {
            return await DoPostAsync<OrderLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/order/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}