using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FormDesign
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class FormDesignController : AppDataController
    {
        public FormDesignController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FormDesignLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/formdesign/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FormDesignLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/formdesign
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormDesignLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FormDesignLogic>(pageno, pagesize, sort, typeof(FormDesignLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/formdesign/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<FormDesignLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FormDesignLogic>(id, typeof(FormDesignLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/formdesign
        [HttpPost]
        public async Task<ActionResult<FormDesignLogic>> PostAsync([FromBody]FormDesignLogic l)
        {
            return await DoPostAsync<FormDesignLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/formdesign/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FormDesignLogic));
        }
        //------------------------------------------------------------------------------------
    }
}