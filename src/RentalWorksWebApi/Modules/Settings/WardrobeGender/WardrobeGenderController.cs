﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.WardrobeGender
{
    [Route("api/v1/[controller]")]
    public class WardrobeGenderController : RwDataController
    {
        public WardrobeGenderController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobegender/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WardrobeGenderLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobegender
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeGenderLogic>(pageno, pagesize, sort, typeof(WardrobeGenderLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobegender/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeGenderLogic>(id, typeof(WardrobeGenderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobegender
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WardrobeGenderLogic l)
        {
            return await DoPostAsync<WardrobeGenderLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobegender/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WardrobeGenderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobegender/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}