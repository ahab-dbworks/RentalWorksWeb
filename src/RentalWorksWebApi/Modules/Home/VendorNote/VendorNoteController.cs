﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Home.VendorNote
{
    [Route("api/v1/[controller]")]
    public class VendorNoteController : RwDataController
    {
        public VendorNoteController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendornote/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VendorNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendornote
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorNoteLogic>(pageno, pagesize, sort, typeof(VendorNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendornote/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorNoteLogic>(id, typeof(VendorNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendornote
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VendorNoteLogic l)
        {
            return await DoPostAsync<VendorNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendornote/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VendorNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendornote/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}