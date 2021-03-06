using AutoMapper;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Data;
using FwStandard.Models;
using FwStandard.Modules.Administrator.WebAuditJson;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
        protected bool return404IfGetNotFound = true;
        protected bool return404IfDeleteNotFound = true;
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
        protected virtual async Task<ActionResult<FwJsonDataTable>> DoBrowseAsync(BrowseRequest browseRequest, Type type = null)
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
                if (type.IsSubclassOf(typeof(FwBusinessLogic)))
                {
                    FwBusinessLogic logic = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                    FwJsonDataTable dt = await logic.BrowseAsync(browseRequest);
                    return new OkObjectResult(dt);
                }
                else if (type.IsSubclassOf(typeof(FwDataReadWriteRecord)))
                {

                    FwDataReadWriteRecord loader = (FwDataReadWriteRecord)Activator.CreateInstance(type);
                    loader.SetDependencies(this.AppConfig, this.UserSession);
                    FwJsonDataTable dt = await loader.BrowseAsync(browseRequest);
                    return new OkObjectResult(dt);
                }
                else
                {
                    throw new ArgumentException($"Invalid type: {type.FullName}.");
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<FwJsonDataTable>> DoBrowseAsync<T>(BrowseRequest browseRequest)
        {
            return await this.DoBrowseAsync(browseRequest, typeof(T));
        }
        //------------------------------------------------------------------------------------
        public class DoExportExcelXlsxExportFileAsyncResult
        {
            public string downloadUrl = "";
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DoExportExcelXlsxFileAsync(BrowseRequest browseRequest, Type type = null, string worksheetName = "")
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


                browseRequest.forexcel = true;
                browseRequest.pageno = 1; // Required for successful download excel from any page other than 1
                FwJsonDataTable dt;

                if (type.IsSubclassOf(typeof(FwBusinessLogic)))
                {
                    if (string.IsNullOrEmpty(worksheetName))
                    {
                        string moduleName = type.Name.Replace("Logic", "");
                        worksheetName = moduleName;
                    }
                    FwBusinessLogic logic = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                    dt = await logic.BrowseAsync(browseRequest);
                }
                else if (type.IsSubclassOf(typeof(FwDataReadWriteRecord)))
                {
                    if (string.IsNullOrEmpty(worksheetName))
                    {
                        string moduleName = type.Name.Replace("Loader", "");
                        worksheetName = moduleName;
                    }
                    FwDataReadWriteRecord loader = (FwDataReadWriteRecord)Activator.CreateInstance(type);
                    loader.SetDependencies(this.AppConfig, this.UserSession);
                    dt = await loader.BrowseAsync(browseRequest);
                }
                else
                {
                    throw new ArgumentException($"Invalid type: {type.FullName}.");
                }

                string strippedWorksheetName = new string(worksheetName.Where(c => char.IsLetterOrDigit(c)).ToArray());
                string filename = $"{this.UserSession.WebUsersId}_{strippedWorksheetName}_{Guid.NewGuid().ToString().Replace("-", string.Empty)}_xlsx";
                string downloadFileName = $"{strippedWorksheetName}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}";
                string directory = FwDownloadController.GetDownloadsDirectory();
                string path = Path.Combine(directory, filename);

                // Delete any existing excel files belonginng to this user
                FwDownloadController.DeleteCurrentWebUserDownloads(this.UserSession.WebUsersId);

                //bool includeColorColumns = browseRequest.includecolorcolumns;
                //bool includeIdColumns = browseRequest.includeidcolumns;
                //
                //if (!includeIdColumns || !includeColorColumns)
                //{
                //    foreach (FwJsonDataTableColumn col in dt.Columns)
                //    {
                //        string dataField = col.DataField.ToUpper();
                //        if ((!includeIdColumns) && (dataField.EndsWith("ID") || dataField.EndsWith("KEY")))
                //        {
                //            col.IsVisible = false;
                //        }
                //
                //        if ((!includeColorColumns) && (col.DataType.Equals(FwDataTypes.OleToHtmlColor)))
                //        {
                //            col.IsVisible = false;
                //        }
                //    }
                //}

                dt.ToExcelXlsxFile(worksheetName, path);
                DoExportExcelXlsxExportFileAsyncResult result = new DoExportExcelXlsxExportFileAsyncResult();
                result.downloadUrl = $"api/v1/download/{filename}?downloadasfilename={downloadFileName}.xlsx";
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DoExportExcelXlsxFileAsync<T>(BrowseRequest browseRequest, string worksheetName = "")
        {
            return await this.DoExportExcelXlsxFileAsync(browseRequest, typeof(T), worksheetName);
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<IEnumerable<T>>> DoGetAsync<T>(int pageno, int pagesize, string sort, Type type = null)
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

        protected virtual async Task<ActionResult<T>> DoGetAsync<T>(string id, Type type = null)
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
                string[] ids = id.Split('~');
                bool found = await l.LoadAsync<T>(ids);
                if ((!found) && return404IfGetNotFound)
                {
                    return NotFound();
                }
                else
                {
                    return new OkObjectResult(l);
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoGetEmptyObjectAsync(Type type = null)
        {
            try
            {
                if (type == null)
                {
                    type = logicType;
                }
                FwBusinessLogic l = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                l.IsEmptyObject = true;

                if ((l._Custom.CustomFields != null) && (l._Custom.CustomFields.Count > 0))
                {
                    FwCustomValues customValues = new FwCustomValues();
                    foreach (FwCustomField customField in l._Custom.CustomFields)
                    {
                        customValues.AddCustomValue(customField.FieldName, null, customField.FieldType);
                    }
                    l._Custom = customValues;
                }
                await Task.CompletedTask;
                return new OkObjectResult(l);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/{module}/emptyobject
        /// <summary>
        /// Get an empty object
        /// </summary>
        /// <returns></returns>
        [HttpGet("emptyobject")]
        [FwControllerMethod("", FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public virtual async Task<IActionResult> GetEmptyObjectAsync()
        {
            return await DoGetEmptyObjectAsync();
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoGetEmptyBrowseObjectAsync(Type type = null)
        {
            try
            {
                if (type == null)
                {
                    type = logicType;
                }
                FwBusinessLogic l = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                l.IsEmptyObject = true;

                await Task.CompletedTask;

                if (l.HasBrowseLoader)
                {
                    if ((l._Custom.CustomFields != null) && (l._Custom.CustomFields.Count > 0))
                    {
                        FwCustomValues customValues = new FwCustomValues();
                        foreach (FwCustomField customField in l._Custom.CustomFields)
                        {
                            customValues.AddCustomValue(customField.FieldName, null, customField.FieldType);
                        }
                        l._Custom = customValues;
                    }

                    FwDataRecord emptyLoadRecord = l.GetEmptyBrowseLoaderRecord();
                    emptyLoadRecord._Custom = l._Custom;

                    return new OkObjectResult(emptyLoadRecord);
                }
                else
                {
                    return await DoGetEmptyObjectAsync();
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/{module}/emptybrowseobject
        /// <summary>
        /// Get an empty browse object
        /// </summary>
        /// <returns></returns>
        [HttpGet("emptybrowseobject")]
        [FwControllerMethod("", FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public virtual async Task<IActionResult> GetEmptyBrowseObjectAsync()
        {
            return await DoGetEmptyBrowseObjectAsync();
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoGetKeyFieldNamesAsync(Type type = null)
        {
            try
            {
                if (type == null)
                {
                    type = logicType;
                }
                FwBusinessLogic l = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                List<PropertyInfo> pkProperties = l.GetPrimaryKeyProperties();
                List<string> fieldNames = new List<string>();
                foreach (PropertyInfo property in pkProperties)
                {
                    fieldNames.Add(property.Name);
                }
                await Task.CompletedTask;
                return new OkObjectResult(fieldNames);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/{module}/keyfieldnames
        /// <summary>
        /// Get an array of primary key field names
        /// </summary>
        /// <returns></returns>
        [HttpGet("keyfieldnames")]
        [FwControllerMethod("", FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public virtual async Task<IActionResult> GetKeyFieldNamesAsync()
        {
            return await DoGetKeyFieldNamesAsync();
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Gets a list of objects using the type argument as the BusinessLogicType and maps the results onto the generic type T.  If type is null, the logicType of the controller is used for the BusinessLogicType.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="type"></param>
        /// <param name="customFields"></param>
        /// <param name="beforeExecuteQuery"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        protected virtual async Task<ActionResult<GetResponse<T>>> DoGetManyAsync<T>(GetRequest request, Type type = null, FwCustomFields customFields = null, Func<FwSqlSelect, Task> beforeExecuteQuery = null, FwSqlConnection conn = null)
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
                if (type.IsSubclassOf(typeof(FwBusinessLogic)))
                {
                    FwBusinessLogic l = FwBusinessLogic.CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                    GetResponse<T> response = await l.GetManyAsync<T>(request, beforeExecuteQuery, conn);
                    return new OkObjectResult(response);
                }
                else if (type.IsSubclassOf(typeof(FwDataReadWriteRecord)))
                {

                    FwDataReadWriteRecord loader = (FwDataReadWriteRecord)Activator.CreateInstance(type);
                    loader.SetDependencies(this.AppConfig, this.UserSession);
                    var getManyAsyncMethodInfo = loader.GetType().GetMethod("GetManyAsync");
                    var getManyAsyncGenericMethocInfo = getManyAsyncMethodInfo.MakeGenericMethod(type);
                    dynamic getManyAsyncTask = getManyAsyncGenericMethocInfo.Invoke(loader, new object[] { request, customFields, beforeExecuteQuery, conn });
                    dynamic unmappedResponse = await getManyAsyncTask;
                    GetResponse<T> mappedResponse = AutoMapper.Mapper.Map<GetResponse<T>>(unmappedResponse);
                    return new OkObjectResult(mappedResponse);
                }
                else
                {
                    throw new ArgumentException($"Invalid type: {type.FullName}.");
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<T>> DoPostAsync<T>(FwBusinessLogic l)
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

                var result = new FwValidateResult();
                if (l.AllPrimaryKeysHaveValues)
                {
                    //updating
                    saveMode = TDataRecordSaveMode.smUpdate;

                    if (l.LoadOriginalBeforeSaving)
                    {
                        //load the original record from the database
                        original = FwBusinessLogic.CreateBusinessLogic(logicType, this.AppConfig, this.UserSession);
                        original.SetPrimaryKeys(l.GetPrimaryKeys());
                        bool exists = await original.LoadAsync<T>();
                        //if (!exists)
                        if ((!exists) && return404IfGetNotFound)
                        {
                            return NotFound();
                        }
                    }
                }
                else
                {
                    //inserting
                }
                await l.ValidateBusinessLogicAsync(saveMode, original, result);

                if (result.IsValid)
                {
                    int rowsAffected = await l.SaveAsync(original);

                    if (l.ReloadOnSave)
                    {
                        await l.LoadAsync<T>();
                    }
                    return new OkObjectResult(l);
                }
                else
                {
                    return BadRequest(result.ValidateMsg);
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<T>> DoNewAsync<T>(object request, Type logicType = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FwBusinessLogic logic;
                if (request.GetType().IsSubclassOf(typeof(FwBusinessLogic)))
                {
                    logic = (FwBusinessLogic)request;
                }
                else
                {
                    logic = (FwBusinessLogic)Mapper.Map(request, request.GetType(), logicType);
                }

                FwBusinessLogic original = null;
                TDataRecordSaveMode saveMode = TDataRecordSaveMode.smInsert;
                logic.AppConfig = this.AppConfig;
                logic.UserSession = this.UserSession;

                var result = new FwValidateResult();
                if (logic.AllPrimaryKeysHaveValues)
                {
                    return BadRequest();
                }
                await logic.ValidateBusinessLogicAsync(saveMode, original, result);

                if (result.IsValid)
                {
                    int rowsAffected = await logic.SaveAsync(original, saveMode: TDataRecordSaveMode.smInsert);

                    if (logic.ReloadOnSave)
                    {
                        var logic_LoadAsyncMethodInfo = logic.GetType().GetMethod("LoadAsync", new Type[] { typeof(FwSqlConnection) });
                        var logic_LoadAsyncGenericMethocInfo = logic_LoadAsyncMethodInfo.MakeGenericMethod(logic.GetType());
                        dynamic logic_LoadAsyncTask = logic_LoadAsyncGenericMethocInfo.Invoke(logic, new object[] { null });
                        dynamic logic_LoadAsyncResponse = await logic_LoadAsyncTask;
                    }
                    if (typeof(T) != logic.GetType())
                    {
                        var mappedLogic = Mapper.Map<T>(logic);
                        return new OkObjectResult(mappedLogic);
                    }
                    return new OkObjectResult(logic);
                }
                else
                {
                    return BadRequest(result.ValidateMsg);
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<T>> DoEditAsync<T>(object request, Type logicType = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FwBusinessLogic logic;
                if (request.GetType().IsSubclassOf(typeof(FwBusinessLogic)))
                {
                    logic = (FwBusinessLogic)request;
                } else
                {
                    logic = (FwBusinessLogic)Mapper.Map(request, request.GetType(), logicType);
                }

                FwBusinessLogic original = null;
                TDataRecordSaveMode saveMode = TDataRecordSaveMode.smInsert;
                logic.AppConfig = this.AppConfig;
                logic.UserSession = this.UserSession;

                var result = new FwValidateResult();
                if (!logic.AllPrimaryKeysHaveValues)
                {
                    return BadRequest();
                }
                //updating
                saveMode = TDataRecordSaveMode.smUpdate;

                if (logic.LoadOriginalBeforeSaving)
                {
                    //load the original record from the database
                    original = FwBusinessLogic.CreateBusinessLogic(logic.GetType(), this.AppConfig, this.UserSession);
                    original.SetPrimaryKeys(logic.GetPrimaryKeys());

                    var original_LoadAsyncMethodInfo = original.GetType().GetMethod("LoadAsync", new Type[] { typeof(FwSqlConnection) });
                    var original_LoadAsyncGenericMethocInfo = original_LoadAsyncMethodInfo.MakeGenericMethod(original.GetType());
                    dynamic original_LoadAsyncTask = original_LoadAsyncGenericMethocInfo.Invoke(original, new object[] { null });
                    dynamic original_LoadAsyncResponse = await original_LoadAsyncTask;
                    bool exists = (bool)original_LoadAsyncResponse;
                    if ((!exists) && return404IfGetNotFound)
                    {
                        return NotFound();
                    }
                }

                await logic.ValidateBusinessLogicAsync(saveMode, original, result);

                if (result.IsValid)
                {
                    int rowsAffected = await logic.SaveAsync(original, saveMode: TDataRecordSaveMode.smUpdate);

                    if (logic.ReloadOnSave)
                    {
                        var logic_LoadAsyncMethodInfo = logic.GetType().GetMethod("LoadAsync", new Type[] { typeof(FwSqlConnection) });
                        var logic_LoadAsyncGenericMethocInfo = logic_LoadAsyncMethodInfo.MakeGenericMethod(logic.GetType());
                        dynamic logic_LoadAsyncTask = logic_LoadAsyncGenericMethocInfo.Invoke(logic, new object[] { null });
                        dynamic logic_LoadAsyncResponse = await logic_LoadAsyncTask;
                    }
                    if (typeof(T) != logic.GetType())
                    {
                        var mappedLogic = Mapper.Map<T>(logic);
                        return new OkObjectResult(mappedLogic);
                    }
                    return new OkObjectResult(logic);
                }
                else
                {
                    return BadRequest(result.ValidateMsg);
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------

        //justin 03/04/2019 experimental
        protected virtual async Task<List<ActionResult<T>>> DoPostAsync<T>(FwBusinessLogicList lList)
        {
            List<ActionResult<T>> results = new List<ActionResult<T>>();
            if (!ModelState.IsValid)
            {
                results.Add(BadRequest(ModelState));
            }
            try
            {
                foreach (FwBusinessLogic l in lList)
                {
                    ActionResult<T> ar = await DoPostAsync<T>(l);
                    results.Add(ar);
                }
                if (lList.Count > 0)
                {
                    AfterSaveManyEventArgs e = new AfterSaveManyEventArgs();
                    await lList[0].AfterSaveManyAsync(e);
                }
            }
            catch (Exception ex)
            {
                results.Add(GetApiExceptionResult(ex));
            }
            return results;
        }
        //------------------------------------------------------------------------------------
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

                FwBusinessLogic logic = FwBusinessLogic.CreateBusinessLogic(type, this.AppConfig, this.UserSession);

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
                var result = new FwValidateResult();

                if (logic.AllPrimaryKeysHaveValues)
                {
                    //update
                    await logic.ValidateBusinessLogicAsync(TDataRecordSaveMode.smUpdate, null, result);
                }
                else
                {
                    //insert
                    await logic.ValidateBusinessLogicAsync(TDataRecordSaveMode.smInsert, null, result);
                }
                if (result.IsValid)
                {
                    await logic.SaveAsync(null);
                    return new OkObjectResult(logic);
                }
                else
                {
                    throw new Exception(result.ValidateMsg);
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        //BEGIN LEGACY CODE
        [Obsolete("DoDeleteAsync without a <T> type is deprecated.  Please add the logical type to this method call.")]
        protected virtual async Task<ActionResult<bool>> DoDeleteAsync(string id, Type type = null)
        {
            try
            {

                if (type == null)
                {
                    type = logicType;
                }

                string[] ids = id.Split('~');
                FwBusinessLogic l = FwBusinessLogic.CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                l.SetPrimaryKeys(ids);
                bool success = await l.DeleteAsync();
                return new OkObjectResult(success);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //END LEGACY CODE
        //------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<bool>> DoDeleteAsync<T>(string id, Type type = null)
        {
            try
            {
                string[] ids = id.Split('~');
                FwBusinessLogic l = null;
                if (type == null)
                {
                    l = FwBusinessLogic.CreateBusinessLogic(typeof(T), this.AppConfig, this.UserSession);
                }
                else
                {
                    l = FwBusinessLogic.CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                }
                l.SetPrimaryKeys(ids);

                if (l.LoadOriginalBeforeDeleting)
                {
                    //load the original record from the database
                    bool exists = await l.LoadAsync<T>();
                    if ((!exists) && return404IfDeleteNotFound)
                    {
                        return NotFound();
                    }
                }

                bool success = await l.DeleteAsync();
                return new OkObjectResult(success);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
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
        protected ObjectResult GetApiExceptionResult(Exception ex)
        {
            FwApiException jsonException = new FwApiException();
            jsonException.StatusCode = StatusCodes.Status500InternalServerError;
            //jsonException.Message = ex.Message;
            jsonException.Message = ex.Message.Replace("The transaction ended in the trigger. The batch has been aborted.", "");

            if (ex.InnerException != null)
            {
                jsonException.Message += $"\n\nInnerException: \n{ex.InnerException.Message}";
            }
            jsonException.StackTrace = ex.StackTrace;
            return StatusCode(jsonException.StatusCode, jsonException);
        }
        //------------------------------------------------------------------------------------
    }
}