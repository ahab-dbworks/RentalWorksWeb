using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.InventorySettings.InventoryType;
using WebApi.Modules.Settings.InventorySettings.Unit;
using WebApi.Modules.Settings.AccountingSettings.GlAccount;

namespace WebApi.Modules.Settings.GeneratorSettings.GeneratorType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"mUQp7GqmQlaR")]
    public class GeneratorTypeController : AppDataController
    {
        public GeneratorTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneratorTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"OOOyCPaiBCjj", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Wsb1kgC6QO2S", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatortype
        [HttpGet]
        [FwControllerMethod(Id:"7LadFZQWdmNP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<GeneratorTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatortype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"DiLpR82Vl7Jz", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<GeneratorTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortype
        [HttpPost]
        [FwControllerMethod(Id:"eX4udmt8J3hF", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<GeneratorTypeLogic>> NewAsync([FromBody]GeneratorTypeLogic l)
        {
            return await DoNewAsync<GeneratorTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/generatortyp/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "sFt4r5sppLkZp", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<GeneratorTypeLogic>> EditAsync([FromRoute] string id, [FromBody]GeneratorTypeLogic l)
        {
            return await DoEditAsync<GeneratorTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatortype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"3KwJCwQCGOPx", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<GeneratorTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateinventorytype/browse
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "nUEBbzC95mxx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateunit/browse
        [HttpPost("validateunit/browse")]
        [FwControllerMethod(Id: "mmVqKHVBDGQy", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUnitBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UnitLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateassetaccount/browse
        [HttpPost("validateassetaccount/browse")]
        [FwControllerMethod(Id: "TMwp7ywLAE5H", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAssetAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateincomeaccount/browse
        [HttpPost("validateincomeaccount/browse")]
        [FwControllerMethod(Id: "5GcSChGp9V28", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validatesubincomeaccount/browse
        [HttpPost("validatesubincomeaccount/browse")]
        [FwControllerMethod(Id: "EolsAu9kcklS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateequipmentsaleincomeaccount/browse
        [HttpPost("validateequipmentsaleincomeaccount/browse")]
        [FwControllerMethod(Id: "Kyf4jnlzrSlR", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateEquipmentSaleIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateldincomeaccount/browse
        [HttpPost("validateldincomeaccount/browse")]
        [FwControllerMethod(Id: "IGn7cy72byLG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLdIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validatecostofgoodssoldexpenseaccount/browse
        [HttpPost("validatecostofgoodssoldexpenseaccount/browse")]
        [FwControllerMethod(Id: "0kjqnOWTxuje", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCostOfGoodsSoldExpenseAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validatecostofgoodsrentedexpenseaccount/browse
        [HttpPost("validatecostofgoodsrentedexpenseaccount/browse")]
        [FwControllerMethod(Id: "bLfWMDRUeBT4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCostOfGoodsRentedExpenseAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
    }
}
