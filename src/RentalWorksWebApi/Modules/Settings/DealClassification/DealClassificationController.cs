using FwStandard.AppManager;
﻿using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;

namespace WebApi.Modules.Settings.DealClassification
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"uRRVPMAFf61J")]
    public class DealClassificationController : AppDataController
    {
        public DealClassificationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealClassificationLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{16457FA2-FB52-4FA9-A94D-3DAB697D6B21}")]
        [FwControllerMethod(Id:"Bh9TCucDdD2j")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"Bh9TCucDdD2j")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus
        [HttpGet]
        [Authorize(Policy = "{EC95C419-BD71-46CB-8BF6-17CB1164552C}")]
        [FwControllerMethod(Id:"Piop2aqmqaFp")]
        public async Task<ActionResult<IEnumerable<DealClassificationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealClassificationLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{73EEDAAC-6133-476A-837B-FCDAED43BDF7}")]
        [FwControllerMethod(Id:"rume2d50A8jn")]
        public async Task<ActionResult<DealClassificationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealClassificationLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus
        [HttpPost]
        [Authorize(Policy = "{3BB08EBF-52CE-4F9C-980D-F162570018CC}")]
        [FwControllerMethod(Id:"UyaSmH7pDVXH")]
        public async Task<ActionResult<DealClassificationLogic>> PostAsync([FromBody]DealClassificationLogic l)
        {
            return await DoPostAsync<DealClassificationLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customerstatus/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{941CE445-FC04-44ED-A041-F9705334AE9A}")]
        [FwControllerMethod(Id:"K3nKF8nTFIEe")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
