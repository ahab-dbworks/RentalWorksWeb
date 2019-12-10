using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.InventorySettings.InventoryType;
using WebApi.Modules.Settings.AppReportDesigner;
using WebApi.Modules.Settings.AccountingSettings.GlAccount;

namespace WebApi.Modules.Settings.InventorySettings.RentalCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"whxFImy6IZG2p")]
    public class RentalCategoryController : AppDataController
    {
        public RentalCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RentalCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"f8RNwGI6wXDyf", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"SFOKmae3MqpMJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/rentalcategory
        [HttpGet]
        [FwControllerMethod(Id:"r8i1dPqDT8ox9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<RentalCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RentalCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/rentalcategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"anPAUbeBwi8f0", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<RentalCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RentalCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory
        [HttpPost]
        [FwControllerMethod(Id:"eKNlff3tW3swN", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<RentalCategoryLogic>> NewAsync([FromBody]RentalCategoryLogic l)
        {
            return await DoNewAsync<RentalCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/rentalcategor/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "VB80rXwpHgOXn", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<RentalCategoryLogic>> EditAsync([FromRoute] string id, [FromBody]RentalCategoryLogic l)
        {
            return await DoEditAsync<RentalCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/rentalcategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"FDQGC9RXy6eKi", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<RentalCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateinventorytype/browse
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "YGDbJBI9rzRA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateinventorybarcodedesigner/browse
        [HttpPost("validateinventorybarcodedesigner/browse")]
        [FwControllerMethod(Id: "eapSQqhCJCdV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBarCodeDesignerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<AppReportDesignerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validatebarcodedesigner/browse
        [HttpPost("validatebarcodedesigner/browse")]
        [FwControllerMethod(Id: "vA8p2oYcjhyH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateBarCodeDesignerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<AppReportDesignerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateassetaccount/browse
        [HttpPost("validateassetaccount/browse")]
        [FwControllerMethod(Id: "xmmeQxJe3dhG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAssetAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateincomeaccount/browse
        [HttpPost("validateincomeaccount/browse")]
        [FwControllerMethod(Id: "xmmeQxJe3dhG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validatesubincomeaccount/browse
        [HttpPost("validatesubincomeaccount/browse")]
        [FwControllerMethod(Id: "xmmeQxJe3dhG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateequipmentsaleincomeaccount/browse
        [HttpPost("validateequipmentsaleincomeaccount/browse")]
        [FwControllerMethod(Id: "xmmeQxJe3dhG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateEquipmentSaleIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateldincomeaccount/browse
        [HttpPost("validateldincomeaccount/browse")]
        [FwControllerMethod(Id: "xmmeQxJe3dhG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLdIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validatecostofgoodssoldexpenseaccount/browse
        [HttpPost("validatecostofgoodssoldexpenseaccount/browse")]
        [FwControllerMethod(Id: "xmmeQxJe3dhG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCostOfGoodsSoldExpenseAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validatecostofgoodsrentedexpenseaccount/browse
        [HttpPost("validatecostofgoodsrentedexpenseaccount/browse")]
        [FwControllerMethod(Id: "xmmeQxJe3dhG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCostOfGoodsRentedExpenseAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
    }
}
