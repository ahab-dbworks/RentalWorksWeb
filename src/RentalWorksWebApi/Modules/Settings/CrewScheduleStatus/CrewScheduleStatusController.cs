﻿using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.CrewScheduleStatus
{
    [Route("api/v1/[controller]")]
    public class CrewScheduleStatusController : RwDataController
    {
        public CrewScheduleStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewschedulestatus/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CrewScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/crewschedulestatus
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewScheduleStatusLogic>(pageno, pagesize, sort, typeof(CrewScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/crewschedulestatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewScheduleStatusLogic>(id, typeof(CrewScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewschedulestatus
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<IActionResult> PostAsync([FromBody]CrewScheduleStatusLogic l)
        {
            return await DoPostAsync<CrewScheduleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/crewschedulestatus/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CrewScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewschedulestatus/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}