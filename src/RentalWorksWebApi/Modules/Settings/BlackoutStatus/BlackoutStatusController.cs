﻿using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.BlackoutStatus
{
    [Route("api/v1/[controller]")]
    public class BlackoutStatusController : RwDataController
    {
        public BlackoutStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/blackoutstatus/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{3EC1D66D-A977-4A7F-8D24-5930A002E63E}")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(BlackoutStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/blackoutstatus
        [HttpGet]
        [Authorize(Policy = "{C30C427F-F9DF-41D8-8569-14AD17680624}")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BlackoutStatusLogic>(pageno, pagesize, sort, typeof(BlackoutStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/blackoutstatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{5B6C7CAF-E5E5-45CC-88DF-0AA132F61CE0}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<BlackoutStatusLogic>(id, typeof(BlackoutStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/blackoutstatus
        [HttpPost]
        [Authorize(Policy = "{9B53148A-781B-4CAE-B4F3-AFAAA749A65A}")]
        public async Task<IActionResult> PostAsync([FromBody]BlackoutStatusLogic l)
        {
            return await DoPostAsync<BlackoutStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/blackoutstatus/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{1BA52854-4796-41B5-973D-9A9731BC4AFE}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(BlackoutStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/blackoutstatus/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "{278A4455-2772-4E7A-A207-F0CD4283FDA2}")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}