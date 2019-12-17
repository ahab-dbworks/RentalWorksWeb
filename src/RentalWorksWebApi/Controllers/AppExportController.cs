using FwCore.Controllers;
using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using static FwCore.Controllers.FwDataController;

namespace WebApi.Controllers
{
    public abstract class AppExportController : FwController
    {
        //------------------------------------------------------------------------------------
        public AppExportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        protected Type loaderType = null;
        //------------------------------------------------------------------------------------
        protected ObjectResult GetApiExceptionResult(Exception ex)
        {
            FwApiException jsonException = new FwApiException();
            jsonException.StatusCode = StatusCodes.Status500InternalServerError;
            jsonException.Message = ex.Message;
            if (ex.InnerException != null)
            {
                jsonException.Message += $"\n\nInnerException: \n{ex.InnerException.Message}";
            }
            jsonException.StackTrace = ex.StackTrace;
            return StatusCode(jsonException.StatusCode, jsonException);
        }
        //------------------------------------------------------------------------------------
        [HttpGet("emptyobject")]
        [FwControllerMethod("", FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public ActionResult<FwJsonDataTable> GetEmptyObject()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Type type = loaderType;
                AppExportLoader l = (AppExportLoader)Activator.CreateInstance(type);
                l.SetDependencies(AppConfig, UserSession);
                return new OkObjectResult(l);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
