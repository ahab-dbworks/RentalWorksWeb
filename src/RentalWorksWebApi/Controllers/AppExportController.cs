﻿using FwCore.Controllers;
using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Text;
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
        protected AppExportLoader loader = null;
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
        public Task<bool> Export<T>(AppExportLoader loader, string exportString)
        {
            bool exported = false;

            StringBuilder sb = new StringBuilder();
            var template = Handlebars.Compile(exportString);
            var result = template(loader);

            exported = true;
            return exported;
        }
    }
}
