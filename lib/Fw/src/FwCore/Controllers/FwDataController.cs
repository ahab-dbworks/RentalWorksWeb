﻿using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FwCore.Controllers
{
    [Route("api/v1/[controller]")]
    public abstract class FwDataController : FwController
    {
        protected Type logicType = null;
        //------------------------------------------------------------------------------------
        public FwDataController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        protected FwBusinessLogic CreateBusinessLogic(Type type, FwApplicationConfig appConfig, FwUserSession userSession)
        {
            FwBusinessLogic bl = (FwBusinessLogic)Activator.CreateInstance(type);
            bl.AppConfig = appConfig;
            bl.UserSession = userSession;
            bl.SetDependencies(appConfig, userSession);
            return bl;
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoBrowseAsync(BrowseRequest browseRequest, Type type = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (type == null)
                {
                    type = logicType;
                }

                FwBusinessLogic l = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.BrowseAsync(browseRequest);
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
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
        }
        //------------------------------------------------------------------------------------
        public class DoExportExcelXlsxExportFileAsyncResult
        {
            public string downloadUrl = "";
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoExportExcelXlsxFileAsync(BrowseRequest browseRequest, Type type = null, string worksheetName = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (type == null)
                {
                    type = logicType;
                }

                if (string.IsNullOrEmpty(worksheetName))
                {
                    string moduleName = type.Name.Replace("Logic", "");
                    worksheetName = moduleName;
                }

                FwBusinessLogic l = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.BrowseAsync(browseRequest);
                string strippedWorksheetName = new string(worksheetName.Where(c => char.IsLetterOrDigit(c)).ToArray());
                string downloadFileName = strippedWorksheetName + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string filename = this.UserSession.WebUsersId + "_" + strippedWorksheetName + "_" + Guid.NewGuid().ToString().Replace("-", string.Empty) + "_xlsx";
                string directory = FwDownloadController.GetDownloadsDirectory();
                string path = Path.Combine(directory, filename);

                // Delete any existing excel files belonginng to this user
                FwDownloadController.DeleteCurrentWebUserDownloads(this.UserSession.WebUsersId);

                dt.ToExcelXlsxFile(worksheetName, path);
                DoExportExcelXlsxExportFileAsyncResult result = new DoExportExcelXlsxExportFileAsyncResult();
                result.downloadUrl = "api/v1/download/" + filename + "?downloadasfilename=" + downloadFileName + ".xlsx";
                return new OkObjectResult(result);
            }
            catch (Exception ex)
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
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoGetAsync<T>(int pageno, int pagesize, string sort, Type type = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (type == null)
                {
                    type = logicType;
                }

                BrowseRequest request = new BrowseRequest();
                request.pageno = 0;
                request.pagesize = 0;
                request.orderby = string.Empty;
                //request.pageno = pageno;
                //request.pagesize = pagesize;
                //request.orderby = sort;
                FwBusinessLogic l = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                IEnumerable<T> records = await l.SelectAsync<T>(request);
                return new OkObjectResult(records);
            }
            catch (Exception ex)
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
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoGetAsync<T>(string id, Type type = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (type == null)
                {
                    type = logicType;
                }

                string[] ids = id.Split('~');
                FwBusinessLogic l = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
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
                FwBusinessLogic original = null;
                TDataRecordSaveMode saveMode = TDataRecordSaveMode.smInsert;
                l.AppConfig = this.AppConfig;
                l.UserSession = this.UserSession;

                bool isValid = true;
                string validateMsg = string.Empty;
                if (l.AllPrimaryKeysHaveValues)
                {
                    //updating
                    saveMode = TDataRecordSaveMode.smUpdate;

                    //load the original record from the database
                    original = CreateBusinessLogic(logicType, this.AppConfig, this.UserSession);
                    original.SetPrimaryKeys(l.GetPrimaryKeys());
                    bool exists = await original.LoadAsync<T>();
                    if (!exists)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    //inserting
                }
                isValid = l.ValidateBusinessLogic(saveMode, original, ref validateMsg);

                if (isValid)
                {
                    await l.SaveAsync(original);  
                    if (l.ReloadOnSave)
                    {
                        await l.LoadAsync<T>();
                    }
                    return new OkObjectResult(l);
                }
                else
                {
                    throw new Exception(validateMsg);
                }
            }
            catch (Exception ex)
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
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoSaveFormAsync<T>(SaveFormRequest request, Type type = null)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (type == null)
                {
                    type = logicType;
                }

                FwBusinessLogic logic = CreateBusinessLogic(type, this.AppConfig, this.UserSession);

                //populate the parent formfields from the request
                IDictionary<string, dynamic> miscfields = request.miscfields;
                foreach (var miscfield in miscfields)
                {
                    var propertyInfo = logic.GetType().GetTypeInfo().GetProperties().DefaultIfEmpty(null).FirstOrDefault(p => p.Name == miscfield.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(logic, miscfield.Value.value);
                    }
                }

                //populate the uniqueids from the request
                //this section may not be needed mv 2017-08-25
                IDictionary<string, dynamic> ids = request.ids;
                foreach (var id in ids)
                {
                    var propertyInfo = logic.GetType().GetTypeInfo().GetProperties().DefaultIfEmpty(null).FirstOrDefault(p => p.Name == id.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(logic, id.Value.value);
                    }
                }

                //populate the fields from the request
                IDictionary<string, dynamic> fields = request.fields;
                foreach (var field in fields)
                {
                    var propertyInfo = logic.GetType().GetTypeInfo().GetProperties().DefaultIfEmpty(null).FirstOrDefault(p => p.Name == field.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(logic, field.Value.value);
                    }
                }

                logic.AppConfig = AppConfig;
                bool isValid = true;
                string validateMsg = string.Empty;

                if (logic.AllPrimaryKeysHaveValues)
                {
                    //update
                    isValid = logic.ValidateBusinessLogic(TDataRecordSaveMode.smUpdate, null, ref validateMsg);
                }
                else
                {
                    //insert
                    isValid = logic.ValidateBusinessLogic(TDataRecordSaveMode.smInsert, null, ref validateMsg);
                }
                if (isValid)
                {
                    await logic.SaveAsync(null);
                    return new OkObjectResult(logic);
                }
                else
                {
                    throw new Exception(validateMsg);
                }
            }
            catch (Exception ex)
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
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoDeleteAsync(string id, Type type = null)
        {
            try
            {

                if (type == null)
                {
                    type = logicType;
                }

                string[] ids = id.Split('~');
                FwBusinessLogic l = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                l.SetPrimaryKeys(ids);
                bool success = await l.DeleteAsync();
                return new OkObjectResult(success);
            }
            catch (Exception ex)
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
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoValidateDuplicateAsync(ValidateDuplicateRequest request)  //05/25/2018 justin - I think this can be removed now
        {
            IActionResult result = new OkObjectResult(true);
            await Task.CompletedTask; // get rid of the no async call warning
            return result;
        }
        //------------------------------------------------------------------------------------
    }
}