using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.OrderSettings.OrderType;
using WebApi.Modules.Settings.InventorySettings.InventoryType;
using WebApi.Modules.Settings.InventorySettings.RentalCategory;
using WebApi.Modules.Settings.SubCategory;
using WebApi.Modules.Inventory.RentalInventory;
using WebApi.Modules.Settings.LaborSettings.LaborType;
using WebApi.Modules.Settings.LaborSettings.LaborCategory;
using WebApi.Modules.Settings.LaborSettings.LaborRate;
using WebApi.Modules.Settings.MiscellaneousSettings.MiscType;
using WebApi.Modules.Settings.MiscellaneousSettings.MiscCategory;
using WebApi.Modules.Settings.MiscellaneousSettings.MiscRate;
using WebApi.Modules.Settings.InventorySettings.SalesCategory;
using WebApi.Modules.Inventory.SalesInventory;

namespace WebApi.Modules.Settings.DiscountItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"UMKuETy6vOLA")]
    public class DiscountItemController : AppDataController
    {
        public DiscountItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DiscountItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/discountitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"Y3jAcjLkbnIv", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"7VW0H7awipPK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discountitem 
        [HttpGet]
        [FwControllerMethod(Id:"fsU23r6XoB39", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DiscountItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DiscountItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discountitem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"trLqZAVvntAU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DiscountItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DiscountItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/discountitem 
        [HttpPost]
        [FwControllerMethod(Id:"pP0RhTPKpzHq", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DiscountItemLogic>> NewAsync([FromBody]DiscountItemLogic l)
        {
            return await DoNewAsync<DiscountItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/discountitem/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "vDxzf6nSwtiJA", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DiscountItemLogic>> EditAsync([FromRoute] string id, [FromBody]DiscountItemLogic l)
        {
            return await DoEditAsync<DiscountItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/discountitem/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"SPkaYZTVB6Tl", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DiscountItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // ----- Rental Discount Tab ---------------------------------------------------------
        // POST api/v1/discountitem/validateordertype/browse
        [HttpPost("validateordertype/browse")]
        [FwControllerMethod(Id: "NuvNcvNRVcf2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountitem/validateinventorytype/browse
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "e1JNjMoA9I0k", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountitem/validatecategory/browse
        [HttpPost("validatecategory/browse")]
        [FwControllerMethod(Id: "ncqc8ULkQ8zN", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountitem/validatesubcategory/browse
        [HttpPost("validatesubcategory/browse")]
        [FwControllerMethod(Id: "klMQwaEKm1gU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SubCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountitem/validateinventory/browse
        [HttpPost("validateinventory/browse")]
        [FwControllerMethod(Id: "O2HeEkBeDlAa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // ----- Labor Discount Tab ---------------------------------------------------------
        // POST api/v1/discountitem/validatelaborinventorytype/browse
        [HttpPost("validatelaborinventorytype/browse")]
        [FwControllerMethod(Id: "0kTVz2o7mPM5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLaborInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<LaborTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountitem/validatelaborcategory/browse
        [HttpPost("validatelaborcategory/browse")]
        [FwControllerMethod(Id: "j083RhAaqr8V", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLaborCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<LaborCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountitem/validatelaborinventory/browse
        [HttpPost("validatelaborinventory/browse")]
        [FwControllerMethod(Id: "odWXy8LaYAk3", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLaborInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<LaborRateLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // ----- Misc Discount Tab ---------------------------------------------------------
        // POST api/v1/discountitem/validatemiscinventorytype/browse
        [HttpPost("validatemiscinventorytype/browse")]
        [FwControllerMethod(Id: "upOLk0dSTTf4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateMiscInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<MiscTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountitem/validatemisccategory/browse
        [HttpPost("validatemisccategory/browse")]
        [FwControllerMethod(Id: "yXwF7bXJn8Hw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateMiscCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<MiscCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountitem/validatemiscinventory/browse
        [HttpPost("validatemiscinventory/browse")]
        [FwControllerMethod(Id: "fAIodLfSPeJD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateMiscInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<MiscRateLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // ----- Sales Discount Tab ---------------------------------------------------------
        //------------------------------------------------------------------------------------
        // POST api/v1/discountitem/validatesalescategory/browse
        [HttpPost("validatesalescategory/browse")]
        [FwControllerMethod(Id: "nyw76dyma8L8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSalesCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SalesCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/discountitem/validatesalesinventory/browse
        [HttpPost("validatesalesinventory/browse")]
        [FwControllerMethod(Id: "IsfexnWlsdjx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSalesInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SalesInventoryLogic>(browseRequest);
        }
    }
}
