﻿using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers
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
        protected virtual async Task<IActionResult> DoGetAsync<T>(int pageno, int pagesize, string sort, Type type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BrowseRequestDto request = new BrowseRequestDto();
                request.pageno = 0;
                request.pagesize = 0;
                request.orderby = string.Empty;
                //request.pageno = pageno;
                //request.pagesize = pagesize;
                //request.orderby = sort;
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
                string validateMsg = string.Empty;
                if (l.ValidateBusinessLogic(ref validateMsg))
                {
                    if (l.AllPrimaryKeysHaveValues)
                    {
                        //update
                        await l.SaveAsync();
                        //await l.LoadAsync<T>();
                        return new OkObjectResult(l);
                    }
                    else
                    {
                        //insert
                        await l.SaveAsync();
                        //await l.LoadAsync<T>();
                        //return new CreatedAtRouteResult("api/v1/customerstatus/" + l.GetPrimaryKeys()[0], new { id = l.GetPrimaryKeys()[0] }, l);
                        return new OkObjectResult(l);
                    }
                }
                else
                {
                    throw new Exception(validateMsg);
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

        protected virtual async Task<IActionResult> DoSaveFormAsync<T>(SaveFormRequest request, Type type)
        {
            FwBusinessLogic logic = (FwBusinessLogic)Activator.CreateInstance(type);

            //populate the fields from the request
            IDictionary<string, dynamic> fields = request.fields;
            foreach(var field in fields)
            {
                var propertyInfo = logic.GetType().GetTypeInfo().GetProperties().First(p => p.Name == field.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(logic, field.Value.value);
                }
            }

            //populate the uniqueids from the request
            IDictionary<string, dynamic> ids = request.ids;
            foreach(var id in ids)
            {
                var propertyInfo = logic.GetType().GetTypeInfo().GetProperties().First(p => p.Name == id.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(logic, id.Value.value);
                }
            }
            
            //populate the parent formfields from the request
            IDictionary<string, dynamic> miscfields = request.miscfields;
            foreach(var miscfield in miscfields)
            {
                var propertyInfo = logic.GetType().GetTypeInfo().GetProperties().First(p => p.Name == miscfield.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(logic, miscfield.Value.value);
                }
            }

            return await DoPostAsync<T>(logic);
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