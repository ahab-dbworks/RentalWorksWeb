﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class ScheduleTypeController : RwDataController
    {
        public ScheduleTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/scheduletype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ScheduleTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/scheduletype
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<ScheduleTypeLogic>(pageno, pagesize, sort, typeof(ScheduleTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/scheduletype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<ScheduleTypeLogic>(id, typeof(ScheduleTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/scheduletype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ScheduleTypeLogic l)
        {
            return await DoPostAsync<ScheduleTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/scheduletype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(ScheduleTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/scheduletype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}