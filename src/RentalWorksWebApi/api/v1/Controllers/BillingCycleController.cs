﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.api.v1.Models;
using RentalWorksWebLogic.Settings;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class BillingCycleController : RwDataController
    {
        public BillingCycleController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycle/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycle
        /// <summary></summary>
        /// <remarks></remarks>
        /// <response code="200">Successfully retrieves records.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpGet]
        [Produces(typeof(List<BillingCycleLogic>))]
        [SwaggerResponse(200, Type = typeof(List<BillingCycleLogic>))]
        [SwaggerResponse(500, Type = typeof(ApiException))]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BillingCycleLogic>(pageno, pagesize, sort, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycle/A0000001
        /// <summary></summary>
        /// <remarks></remarks>
        /// <response code="200">Successfully retrieved the record.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpGet("{id}")]
        [Produces(typeof(BillingCycleLogic))]
        [SwaggerResponse(200, Type = typeof(BillingCycleLogic))]
        [SwaggerResponse(500, Type = typeof(ApiException))]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<BillingCycleLogic>(id, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycle
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]BillingCycleLogic l)
        {
            return await DoPostAsync<BillingCycleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/billingcycle/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycle/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}