using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using Microsoft.AspNetCore.Http;
using System;

namespace WebApi.Modules.Home.Container
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "bSQsBVDvo86X1")]
    public class ContainerController : AppDataController
    {
        public ContainerController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContainerLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/container/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "56QKEdqPBEc68")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/container/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "J4Ikgxw4b5gsl")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------     
        // GET api/v1/container 
        [HttpGet]
        [FwControllerMethod(Id: "QjmyRCx5ZFM8w")]
        public async Task<ActionResult<IEnumerable<ContainerLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ContainerLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/container/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "eCUUnp7FR3x0c")]
        public async Task<ActionResult<ContainerLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContainerLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/container 
        //[HttpPost]
        //[FwControllerMethod(Id: "xxxxxxx")]
        //public async Task<ActionResult<ContainerLogic>> PostAsync([FromBody]ContainerLogic l)
        //{
        //    return await DoPostAsync<ContainerLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/container/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "xxxxxxxxxxx")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <ContainerLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
