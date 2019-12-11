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

namespace WebApi.Modules.Settings.InventorySettings.SalesCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"XS6vdtV5jQTyF")]
    public class SalesCategoryController : AppDataController
    {
        public SalesCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SalesCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"iGvCDBc6iCTOs", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"7TQBqR8a0d6nU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/salescategory
        [HttpGet]
        [FwControllerMethod(Id:"nKzzlH2X6e461", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<SalesCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SalesCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/salescategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"CtaBWOgfnFdio", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<SalesCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SalesCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory
        [HttpPost]
        [FwControllerMethod(Id:"eQE7mrt1VxdOK", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<SalesCategoryLogic>> NewAsync([FromBody]SalesCategoryLogic l)
        {
            return await DoNewAsync<SalesCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/salescategor/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "objnsebcEDwKK", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<SalesCategoryLogic>> EditAsync([FromRoute] string id, [FromBody]SalesCategoryLogic l)
        {
            return await DoEditAsync<SalesCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/salescategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"iXj9iGvriNmmc", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SalesCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory/validateinventorytype/browse
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "yQAP040G9SWy", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory/validateinventorybarcodedesigner/browse
        [HttpPost("validateinventorybarcodedesigner/browse")]
        [FwControllerMethod(Id: "AkFVlIWDahke", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBarcodeDesignerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<AppReportDesignerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory/validatebarcodedesigner/browse
        [HttpPost("validatebarcodedesigner/browse")]
        [FwControllerMethod(Id: "PCETzwYhKJsk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateBarcodeDesignerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<AppReportDesignerLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory/validateprofitandlosscategoryid/browse
        [HttpPost("validateprofitandlosscategoryid/browse")]
        [FwControllerMethod(Id: "ql0tHgwOGbbw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProfitAndLossCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SalesCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory/validateassetaccount/browse
        [HttpPost("validateassetaccount/browse")]
        [FwControllerMethod(Id: "zggN3OW03vFa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAssetAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory/validateincomeaccount/browse
        [HttpPost("validateincomeaccount/browse")]
        [FwControllerMethod(Id: "SD54UFINAhM2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory/validatesubincomeaccount/browse
        [HttpPost("validatesubincomeaccount/browse")]
        [FwControllerMethod(Id: "F3gSObO5lC3B", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory/validatecostofgoodssoldexpenseaccount/browse
        [HttpPost("validatecostofgoodssoldexpenseaccount/browse")]
        [FwControllerMethod(Id: "9SV1pmDVc0oH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCostOfGoodsSoldExpenseAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
    }
}
