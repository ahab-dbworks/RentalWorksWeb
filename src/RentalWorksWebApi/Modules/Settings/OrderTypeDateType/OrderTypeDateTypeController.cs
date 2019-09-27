using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Logic;
using System;

namespace WebApi.Modules.Settings.OrderTypeDateType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"oMijD9WAL6Bl")]
    public class OrderTypeDateTypeController : AppDataController
    {
        public OrderTypeDateTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderTypeDateTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypedatetype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"oQU3jzFAOEJ7")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"SFdEn4RSWwfz")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypedatetype 
        [HttpGet]
        [FwControllerMethod(Id:"HhVQpCNtKIRD")]
        public async Task<ActionResult<IEnumerable<OrderTypeDateTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeDateTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypedatetype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"RYewhcaLuwnS")]
        public async Task<ActionResult<OrderTypeDateTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeDateTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypedatetype 
        [HttpPost]
        [FwControllerMethod(Id:"L8bm7Hv94svZ")]
        public async Task<ActionResult<OrderTypeDateTypeLogic>> PostAsync([FromBody]OrderTypeDateTypeLogic l)
        {
            return await DoPostAsync<OrderTypeDateTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertypedatetype/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"XNnIjGRAojxz")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OrderTypeDateTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypedatetype/sort
        [HttpPost("sort")]
        [FwControllerMethod(Id: "gapf9m1VDrVRm")]
        public async Task<ActionResult<SortItemsResponse>> SortOrderTypeDateTypesAsync([FromBody]SortOrderTypeDateTypesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await OrderTypeDateTypeFunc.SortOrderTypeDateTypes(AppConfig, UserSession, request);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
