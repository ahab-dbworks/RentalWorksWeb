﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.AttributeValue
{
    [Route("api/v1/[controller]")]
    public class AttributeValueController : RwDataController
    {
        public AttributeValueController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/attributevalue/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(AttributeValueLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/attributevalue
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AttributeValueLogic>(pageno, pagesize, sort, typeof(AttributeValueLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/attributevalue/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<AttributeValueLogic>(id, typeof(AttributeValueLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/attributevalue
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]AttributeValueLogic l)
        {
            return await DoPostAsync<AttributeValueLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/attributevalue
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<AttributeValueLogic>(request, typeof(AttributeValueLogic));
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/attributevalue/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(AttributeValueLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/attributevalue/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}