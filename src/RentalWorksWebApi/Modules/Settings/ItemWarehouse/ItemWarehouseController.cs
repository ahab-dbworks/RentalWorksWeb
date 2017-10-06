﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.ItemWarehouse
{
    [Route("api/v1/[controller]")]
    public class ItemWarehouseController : RwDataController
    {
        public ItemWarehouseController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/itemwarehouse/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ItemWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/itemwarehouse
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemWarehouseLogic>(pageno, pagesize, sort, typeof(ItemWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/itemwarehouse/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemWarehouseLogic>(id, typeof(ItemWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/itemwarehouse
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ItemWarehouseLogic l)
        {
            return await DoPostAsync<ItemWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/itemwarehouse/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ItemWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/itemwarehouse/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}