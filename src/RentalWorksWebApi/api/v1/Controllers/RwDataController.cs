using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public abstract class RwDataController : RwController  
    {
        //------------------------------------------------------------------------------------
        public RwDataController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        protected FwBusinessLogic CreateBusinessLogic(Type type)
        {
            return (FwBusinessLogic)Activator.CreateInstance(type);
        }
        //------------------------------------------------------------------------------------
        protected virtual IActionResult doBrowse(BrowseRequestDto browseRequest, Type type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FwBusinessLogic l = CreateBusinessLogic(type);
                l.SetDbConfig(_appConfig.DatabaseSettings);
                FwJsonDataTable dt = l.Browse(browseRequest);
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual IActionResult doGet<T>(int pageno, int pagesize, Type type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BrowseRequestDto request = new BrowseRequestDto();
                request.pageno = pageno;
                request.pagesize = pagesize;
                FwBusinessLogic l = CreateBusinessLogic(type);
                l.SetDbConfig(_appConfig.DatabaseSettings);
                IEnumerable<T> records = l.Select<T>(request);
                return new OkObjectResult(records);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual IActionResult doGet<T>(string id, Type type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                FwBusinessLogic l = CreateBusinessLogic(type);
                l.SetDbConfig(_appConfig.DatabaseSettings);
                if (l.Load<T>(ids))
                {
                    return new OkObjectResult(l);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual IActionResult doPost<T>(FwBusinessLogic l)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                l.SetDbConfig(_appConfig.DatabaseSettings);
                l.Save();
                l.Load<T>();
                return new OkObjectResult(l);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual IActionResult doDelete(string id, Type type)
        {
            try
            {
                string[] ids = id.Split('~');
                FwBusinessLogic l = CreateBusinessLogic(type);
                l.SetDbConfig(_appConfig.DatabaseSettings);
                l.SetPrimaryKeys(ids);
                bool success = l.Delete();
                return new OkObjectResult(success);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual IActionResult doValidateDuplicate(ValidateDuplicateRequest request)
        {
            return new OkObjectResult(true);
        }
        //------------------------------------------------------------------------------------
    }
}