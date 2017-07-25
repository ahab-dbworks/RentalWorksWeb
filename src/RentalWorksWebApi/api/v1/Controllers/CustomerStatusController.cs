﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class CustomerStatusController : RwDataController
    {
        public CustomerStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/browse
        [HttpPost("browse")]
        [ApiExplorerSettings(IgnoreApi=true)]
        public async Task<IActionResult> Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus
        [HttpGet]
        [Produces(typeof(List<CustomerStatusLogic>))]
        [SwaggerResponse(200, Type = typeof(List<CustomerStatusLogic>))]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize)
        {
            return await DoGetAsync<CustomerStatusLogic>(pageno, pagesize, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus/A0000001
        [HttpGet("{id}")]
        [Produces(typeof(CustomerStatusLogic))]
        [SwaggerResponse(200, Type = typeof(CustomerStatusLogic))]
        public async Task<IActionResult> GetAsync([FromQuery]string id)
        {
            return await DoGetAsync<CustomerStatusLogic>(id, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CustomerStatusLogic l)
        {
            return await DoPostAsync<CustomerStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customerstatus/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery]string id)
        {
            return await DoDeleteAsync(id, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/validateduplicate
        [HttpPost("validateduplicate")]
        [ApiExplorerSettings(IgnoreApi=true)]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}