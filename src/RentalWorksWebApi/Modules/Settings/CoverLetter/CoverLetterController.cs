﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.CoverLetter
{
    [Route("api/v1/[controller]")]
    public class CoverLetterController : RwDataController
    {
        public CoverLetterController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/coverletter/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CoverLetterLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/coverletter
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CoverLetterLogic>(pageno, pagesize, sort, typeof(CoverLetterLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/coverletter/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CoverLetterLogic>(id, typeof(CoverLetterLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/coverletter
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CoverLetterLogic l)
        {
            return await DoPostAsync<CoverLetterLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/coverletter/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CoverLetterLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/coverletter/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}