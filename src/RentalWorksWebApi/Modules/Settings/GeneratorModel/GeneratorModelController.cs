﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.GeneratorModel
{
    [Route("api/v1/[controller]")]
    public class GeneratorModelController : RwDataController
    {
        public GeneratorModelController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatormodel/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(GeneratorModelLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatormodel
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorModelLogic>(pageno, pagesize, sort, typeof(GeneratorModelLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatormodel/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorModelLogic>(id, typeof(GeneratorModelLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatormodel
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]GeneratorModelLogic l)
        {
            return await DoPostAsync<GeneratorModelLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatormodel/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(GeneratorModelLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatormodel/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
    //------------------------------------------------------------------------------------
}
}