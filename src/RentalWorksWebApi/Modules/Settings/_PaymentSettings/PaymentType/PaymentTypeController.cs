using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PaymentSettings.PaymentType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"d8RdKxFfho4z")]
    public class PaymentTypeController : AppDataController
    {
        public PaymentTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PaymentTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymenttype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"GLIvBMjLCcKM", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"LCO64veg3hw4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymenttype
        [HttpGet]
        [FwControllerMethod(Id:"jwuaS4gCHxVR", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PaymentTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PaymentTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymenttype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"5a0oHVzRlCEd", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<PaymentTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PaymentTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymenttype
        [HttpPost]
        [FwControllerMethod(Id:"gubRbuYcJuvz", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PaymentTypeLogic>> NewAsync([FromBody]PaymentTypeLogic l)
        {
            return await DoNewAsync<PaymentTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/paymenttyp/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "4SmJEYQS7FSDb", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PaymentTypeLogic>> EditAsync([FromRoute] string id, [FromBody]PaymentTypeLogic l)
        {
            return await DoEditAsync<PaymentTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/paymenttype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"W4dBHcERzFAK", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PaymentTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
