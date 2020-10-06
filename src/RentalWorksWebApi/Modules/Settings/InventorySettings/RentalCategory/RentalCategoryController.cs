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
using FwStandard.BusinessLogic;

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
        // POST api/v1/rentalcategory/many
        [HttpPost("many")]
        [FwControllerMethod(Id: "Q0o4UX3YPLBR")]
        public async Task<List<ActionResult<RentalCategoryLogic>>> PostAsync([FromBody] List<RentalCategoryLogic> l)
        {
            FwBusinessLogicList l2 = new FwBusinessLogicList();
            l2.AddRange(l);
            return await DoPostAsync<RentalCategoryLogic>(l2);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/rentalcategory/A0000001
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
        // POST api/v1/rentalcategory/validateinventorytype/browse
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "YGDbJBI9rzRA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/validateinventorybarcodedesigner/browse
        [HttpPost("validateinventorybarcodedesigner/browse")]
        [FwControllerMethod(Id: "eapSQqhCJCdV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBarCodeDesignerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<AppReportDesignerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/validatebarcodedesigner/browse
        [HttpPost("validatebarcodedesigner/browse")]
        [FwControllerMethod(Id: "vA8p2oYcjhyH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateBarCodeDesignerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<AppReportDesignerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/validateassetaccount/browse
        [HttpPost("validateassetaccount/browse")]
        [FwControllerMethod(Id: "xmmeQxJe3dhG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAssetAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/validateincomeaccount/browse
        [HttpPost("validateincomeaccount/browse")]
        [FwControllerMethod(Id: "dd2xbVxmldem", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/validatesubincomeaccount/browse
        [HttpPost("validatesubincomeaccount/browse")]
        [FwControllerMethod(Id: "BvdbT4Dg0p4T", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/validateequipmentsaleincomeaccount/browse
        [HttpPost("validateequipmentsaleincomeaccount/browse")]
        [FwControllerMethod(Id: "22p0aOOy7MwH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateEquipmentSaleIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/validateldincomeaccount/browse
        [HttpPost("validateldincomeaccount/browse")]
        [FwControllerMethod(Id: "S5D21zs4VctP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLdIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/validatecostofgoodssoldexpenseaccount/browse
        [HttpPost("validatecostofgoodssoldexpenseaccount/browse")]
        [FwControllerMethod(Id: "4ssxQ6VXjhFj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCostOfGoodsSoldExpenseAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/rentalcategory/validatecostofgoodsrentedexpenseaccount/browse
        [HttpPost("validatecostofgoodsrentedexpenseaccount/browse")]
        [FwControllerMethod(Id: "GHkFzPoX8FsX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCostOfGoodsRentedExpenseAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
    }
}
