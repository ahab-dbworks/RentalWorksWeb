﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.MiscType
{
    [Route("api/v1/[controller]")]
    public class MiscTypeController : RwDataController
    {
        public MiscTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/misctype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(MiscTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misctype
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MiscTypeLogic>(pageno, pagesize, sort, typeof(MiscTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misctype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<MiscTypeLogic>(id, typeof(MiscTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/misctype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MiscTypeLogic l)
        {
            return await DoPostAsync<MiscTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/misctype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(MiscTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/misctype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}