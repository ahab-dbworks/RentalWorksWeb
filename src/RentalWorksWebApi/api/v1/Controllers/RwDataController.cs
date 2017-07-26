using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.api.v1.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        protected virtual async Task<IActionResult> DoBrowseAsync(BrowseRequestDto browseRequest, Type type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FwBusinessLogic l = CreateBusinessLogic(type);
                l.SetDbConfig(_appConfig.DatabaseSettings);
                FwJsonDataTable dt = await l.BrowseAsync(browseRequest);
                dt.TotalPages = 1;
                dt.TotalRows = dt.Rows.Count;
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                ApiException jsonException = new ApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoGetAsync<T>(int pageno, int pagesize, Type type)
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
                IEnumerable<T> records = await l.SelectAsync<T>(request);
                return new OkObjectResult(records);
            }
            catch (Exception ex)
            {
                ApiException jsonException = new ApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoGetAsync<T>(string id, Type type)
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
                if (await l.LoadAsync<T>(ids))
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
                ApiException jsonException = new ApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoPostAsync<T>(FwBusinessLogic l)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                l.SetDbConfig(_appConfig.DatabaseSettings);
                await l.SaveAsync();
                await l.LoadAsync<T>();
                return new OkObjectResult(l);
            }
            catch (Exception ex)
            {
                ApiException jsonException = new ApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoDeleteAsync(string id, Type type)
        {
            try
            {
                string[] ids = id.Split('~');
                FwBusinessLogic l = CreateBusinessLogic(type);
                l.SetDbConfig(_appConfig.DatabaseSettings);
                l.SetPrimaryKeys(ids);
                bool success = await l.DeleteAsync();
                return new OkObjectResult(success);
            }
            catch (Exception ex)
            {
                ApiException jsonException = new ApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            IActionResult result = new OkObjectResult(true);
            // mv 7/15/2017 this gets rid of the no async warning, should be replaced by an await against a db call
            await Task.Run(() =>
            {
               
            });
            return result;
        }
        //------------------------------------------------------------------------------------
    }
}