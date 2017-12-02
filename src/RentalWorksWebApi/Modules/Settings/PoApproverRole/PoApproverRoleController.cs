﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.PoApproverRole
{
    [Route("api/v1/[controller]")]
    public class PoApproverRoleController : RwDataController
    {
        public PoApproverRoleController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/poapproverrole/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PoApproverRoleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poapproverrole
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoApproverRoleLogic>(pageno, pagesize, sort, typeof(PoApproverRoleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poapproverrole/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoApproverRoleLogic>(id, typeof(PoApproverRoleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/poapproverrole
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PoApproverRoleLogic l)
        {
            return await DoPostAsync<PoApproverRoleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/poapproverrole/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PoApproverRoleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/poapproverrole/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}