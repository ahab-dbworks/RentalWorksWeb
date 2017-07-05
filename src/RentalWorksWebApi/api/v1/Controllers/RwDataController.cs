using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public abstract class RwDataController : RwController  
    {
        //------------------------------------------------------------------------------------
        public RwDataController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/businesslogic/browse
        [HttpPost("browse")]
        public FwJsonDataTable Browse([FromBody]BrowseRequestDto request)
        {
            FwJsonDataTable dt = doBrowse(request);
            return dt;
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/businesslogic/id
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            string[] ids = id.Split('~');
            doDelete(ids);
        }
        //------------------------------------------------------------------------------------
        protected virtual void doDelete(string[] ids) { }
        //------------------------------------------------------------------------------------
        protected abstract FwJsonDataTable doBrowse(BrowseRequestDto request);
        //------------------------------------------------------------------------------------
    }
}