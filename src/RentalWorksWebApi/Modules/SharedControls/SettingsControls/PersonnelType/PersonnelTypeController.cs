using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.PersonnelType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"Dd4V9E1c9Kz8")]
    public class PersonnelTypeController : AppDataController
    {
        public PersonnelTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PersonnelTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/personneltype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"aabq0oir80vP", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"vtI1gVZ84UHs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/personneltype
        [HttpGet]
        [FwControllerMethod(Id:"1GuqRyjBlO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PersonnelTypeLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<PersonnelTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/personneltype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"MdNPy8DZZuqk")]
        public async Task<ActionResult<PersonnelTypeLogic>> GetAsync(string id)
        {
            return await DoGetAsync<PersonnelTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/personneltype
        [HttpPost]
        [FwControllerMethod(Id:"CFenljECKJAl", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PersonnelTypeLogic>> NewAsync([FromBody]PersonnelTypeLogic l)
        {
            return await DoNewAsync<PersonnelTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/personneltyp/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "TFiYPhFxij4wU", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PersonnelTypeLogic>> EditAsync([FromRoute] string id, [FromBody]PersonnelTypeLogic l)
        {
            return await DoEditAsync<PersonnelTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/personneltype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"UmmOMN9feSrd", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<PersonnelTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
