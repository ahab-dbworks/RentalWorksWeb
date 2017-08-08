﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.UnretiredReason
{
    [Route("api/v1/[controller]")]
    public class UnretiredReasonController : RwDataController
    {
        public UnretiredReasonController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/unretiredreason/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(UnretiredReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/unretiredreason
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UnretiredReasonLogic>(pageno, pagesize, sort, typeof(UnretiredReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/unretiredreason/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<UnretiredReasonLogic>(id, typeof(UnretiredReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/unretiredreason
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]UnretiredReasonLogic l)
        {
            return await DoPostAsync<UnretiredReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/unretiredreason/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(UnretiredReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/unretiredreason/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}