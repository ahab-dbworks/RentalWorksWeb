using FwStandard.AppManager;
using FwStandard.SqlServer;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using FwStandard.BusinessLogic;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.HomeControls.Pricing
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"NygLXvkCVElXL")]
    public class PricingController : AppDataController
    {
        public PricingController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PricingLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/pricing/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"lFLNXAovlTsff", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"f3UcfaNtMXPgv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/pricing/D00BYU6Z/B0029AY5/A001TSXJ   //inventoryid/warehouseid/currencyid
        [HttpGet("{inventoryid}/{warehouseid}/{currencyid}")]
        [FwControllerMethod(Id:"jQK0qxwBnK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PricingLogic>>> GetManyByMasterIdAndWarehouseIdAndCurrencyIdAsync([FromRoute]string inventoryid, [FromRoute]string warehouseid, [FromRoute]string currencyid)
        {
            return await DoSpecialGetAsync<PricingLogic>(inventoryid, warehouseid, currencyid);

        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/pricing/D00BYU6Z/B0029AY5   //inventoryid/warehouseid
        [HttpGet("{inventoryid}/{warehouseid}")]
        [FwControllerMethod(Id:"07TSm1lM7e", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PricingLogic>>> GetManyByMasterIdAndWarehouseIdAsync([FromRoute]string inventoryid, [FromRoute]string warehouseid)
        {
            return await DoSpecialGetAsync<PricingLogic>(inventoryid, warehouseid, "");
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/pricing/D00BYU6Z    //inventoryid
        [HttpGet("{inventoryid}")]
        [FwControllerMethod(Id:"qyjbDa9I9W", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PricingLogic>>> GetManyByMasterIdAsync([FromRoute]string inventoryId)
        {
            return await DoSpecialGetAsync<PricingLogic>(inventoryId, "", "");
        }
        //------------------------------------------------------------------------------------ 
        protected async Task<ActionResult<IEnumerable<T>>> DoSpecialGetAsync<T>(string masterId, string warehouseId, string currencyId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                IDictionary<string, object> uniqueIds = new Dictionary<string, object>();
                uniqueIds.Add("InventoryId", masterId);
                uniqueIds.Add("WarehouseId", warehouseId);
                uniqueIds.Add("CurrencyId", currencyId);

                BrowseRequest request = new BrowseRequest();
                request.pageno = 0;
                request.pagesize = 0;
                request.orderby = string.Empty;
                request.uniqueids = uniqueIds;
                FwBusinessLogic l = FwBusinessLogic.CreateBusinessLogic(logicType, this.AppConfig, this.UserSession);
                IEnumerable<T> records = await l.SelectAsync<T>(request);

                return new OkObjectResult(records);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        //// PUT api/v1/pricing/A0000001 
        //[HttpPut("{id}")]
        //[FwControllerMethod(Id: "vHn1dP6sZ8rgB", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<PricingLogic>> EditAsync([FromRoute] string id, [FromBody]PricingLogic l)
        //{
        //    return await DoEditAsync<PricingLogic>(l);
        //}
        //------------------------------------------------------------------------------------ 
        // POST api/v1/pricing/many
        [HttpPost("many")]
        [FwControllerMethod(Id: "3EIIA7or0scGl")]
        public async Task<List<ActionResult<PricingLogic>>> PostAsync([FromBody]List<PricingLogic> l)
        {
            FwBusinessLogicList l2 = new FwBusinessLogicList();
            l2.AddRange(l);
            return await DoPostAsync<PricingLogic>(l2);
        }
        //------------------------------------------------------------------------------------ 
    }
}
