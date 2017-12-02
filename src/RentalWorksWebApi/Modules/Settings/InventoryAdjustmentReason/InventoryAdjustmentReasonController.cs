﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.InventoryAdjustmentReason
{
    [Route("api/v1/[controller]")]
    public class InventoryAdjustmentReasonController : RwDataController
    {
        public InventoryAdjustmentReasonController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryadjustmentreason/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryAdjustmentReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventoryadjustmentreason
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryAdjustmentReasonLogic>(pageno, pagesize, sort, typeof(InventoryAdjustmentReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventoryadjustmentreason/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryAdjustmentReasonLogic>(id, typeof(InventoryAdjustmentReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryadjustmentreason
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventoryAdjustmentReasonLogic l)
        {
            return await DoPostAsync<InventoryAdjustmentReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventoryadjustmentreason/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryAdjustmentReasonLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryadjustmentreason/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}