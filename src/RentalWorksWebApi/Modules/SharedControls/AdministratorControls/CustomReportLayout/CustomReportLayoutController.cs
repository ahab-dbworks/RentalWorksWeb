using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.AdministratorControls.CustomReportLayout;

namespace WebApi.Modules.Administrator.CustomReportLayout
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id:"EtrF5NHJ7dRg6")]
    public class CustomReportLayoutController : AppDataController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public CustomReportLayoutController(IOptions<FwApplicationConfig> appConfig, IHostingEnvironment hostingEnvironment) : base(appConfig)
        { 
            logicType = typeof(CustomReportLayoutLogic);
            _hostingEnvironment = hostingEnvironment;
        }
        public class CustomReportLayoutResponse
        {
            public string ReportTemplate { get; set; } = "";
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayout/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"M0zfkwRXj1Sg3")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayout/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Xl07paHHu5Bpe")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customreportlayout 
        [HttpGet]
        [FwControllerMethod(Id:"DyfOgFqvM8e13")]
        public async Task<ActionResult<IEnumerable<CustomReportLayoutLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomReportLayoutLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customreportlayout/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"CmnEngR5cTrk9")]
        public async Task<ActionResult<CustomReportLayoutLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomReportLayoutLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayout 
        [HttpPost]
        [FwControllerMethod(Id:"zK6wj3GwcC74U")]
        public async Task<ActionResult<CustomReportLayoutLogic>> PostAsync([FromBody]CustomReportLayoutLogic l)
        {
            return await DoPostAsync<CustomReportLayoutLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/customreportlayout/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"pXTAMmdzOMLHi")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomReportLayoutLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customreportlayout/template/report
        [HttpGet("template/{report}")]
        [FwControllerMethod(Id: "OjYnsJSoA0p")]
        public ActionResult<CustomReportLayoutResponse> GetTemplate([FromRoute]string report)
        {
            try
            {
                CustomReportLayoutResponse response = new CustomReportLayoutResponse();
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "Reports", report, "hbReport.hbs");
                response.ReportTemplate = System.IO.File.ReadAllText(path);
                return response;
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
    }
}
