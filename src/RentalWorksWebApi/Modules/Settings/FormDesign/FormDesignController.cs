﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FormDesign
{
    [Route("api/v1/[controller]")]
    public class FormDesignController : AppDataController
    {
        public FormDesignController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/formdesign/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FormDesignLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/formdesign
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FormDesignLogic>(pageno, pagesize, sort, typeof(FormDesignLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/formdesign/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<FormDesignLogic>(id, typeof(FormDesignLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/formdesign
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]FormDesignLogic l)
        {
            return await DoPostAsync<FormDesignLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/formdesign/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FormDesignLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/formdesign/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}