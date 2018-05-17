using FwStandard.BusinessLogic;
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
        protected virtual async Task<IActionResult> DoBrowseAsync(BrowseRequest browseRequest, Type type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FwBusinessLogic l = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.BrowseAsync(browseRequest);
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------
        public class DoExportExcelXlsxExportFileAsyncResult
        {
            public string downloadUrl = "";
        }
        protected virtual async Task<IActionResult> DoExportExcelXlsxFileAsync(BrowseRequest browseRequest, Type type, string worksheetName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FwBusinessLogic l = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.BrowseAsync(browseRequest);
                string downloadasfilename = new string(worksheetName.Where(c =>char.IsLetterOrDigit(c)).ToArray()) + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string filename = this.UserSession.WebUsersId + "-" + downloadasfilename + "-" + Guid.NewGuid().ToString().Replace("-", string.Empty) + ".xlsx";
                string directory = FwDownloadController.GetDownloadsDirectory();
                string path = Path.Combine(directory, filename);

                // Delete any existing excel files belonginng to this user
                FwDownloadController.DeleteCurrentWebUserDownloads(this.UserSession.WebUsersId);

                dt.ToExcelXlsxFile(worksheetName, path);
                DoExportExcelXlsxExportFileAsyncResult result = new DoExportExcelXlsxExportFileAsyncResult();
                result.downloadUrl = "api/v1/download/" + filename + "?downloadasfilename=" + downloadasfilename + ".xlsx";
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
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
                l.AppConfig = this.AppConfig;
                l.UserSession = this.UserSession;

                bool isValid = true;
                string validateMsg = string.Empty;
                if (l.AllPrimaryKeysHaveValues)
                {
                    //update
                    isValid = l.ValidateBusinessLogic(TDataRecordSaveMode.smUpdate, ref validateMsg);
                }
                else
                {
                    //insert
                    isValid = l.ValidateBusinessLogic(TDataRecordSaveMode.smInsert, ref validateMsg);
                }

                if (isValid)
                {
                    await l.SaveAsync();
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
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoSaveFormAsync<T>(SaveFormRequest request, Type type)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
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
                    isValid = logic.ValidateBusinessLogic(TDataRecordSaveMode.smUpdate, ref validateMsg);
                }
                else
                {
                    //insert
                    isValid = logic.ValidateBusinessLogic(TDataRecordSaveMode.smInsert, ref validateMsg);
                }
                if (isValid)
                {
                    await logic.SaveAsync();
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
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            IActionResult result = new OkObjectResult(true);
            await Task.CompletedTask; // get rid of the no async call warning
            return result;
        }
        //------------------------------------------------------------------------------------
    }
}