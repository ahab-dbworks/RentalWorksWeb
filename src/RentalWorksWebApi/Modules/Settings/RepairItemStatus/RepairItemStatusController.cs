﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.RepairItemStatus
{
    [Route("api/v1/[controller]")]
    public class RepairItemStatusController : AppDataController
    {
        public RepairItemStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/repairitemstatus/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RepairItemStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/repairitemstatus
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairItemStatusLogic>(pageno, pagesize, sort, typeof(RepairItemStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/repairitemstatus/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairItemStatusLogic>(id, typeof(RepairItemStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repairitemstatus
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RepairItemStatusLogic l)
        {
            return await DoPostAsync<RepairItemStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/repairitemstatus/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RepairItemStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repairitemstatus/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}