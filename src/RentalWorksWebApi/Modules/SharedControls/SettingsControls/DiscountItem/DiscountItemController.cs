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
    }
}
