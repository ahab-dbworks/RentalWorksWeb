using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Data;
using FwStandard.Models;
using FwStandard.Modules.Administrator.WebAuditJson;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static FwCore.Controllers.FwDataController;

namespace FwCore.Controllers
{
    public static class FwGridController
    {
        //------------------------------------------------------------------------------------
        public static FwBusinessLogic CreateBusinessLogic(Type type, FwApplicationConfig appConfig, FwUserSession userSession)
        {
            FwBusinessLogic bl = (FwBusinessLogic)Activator.CreateInstance(type);
            bl.AppConfig = appConfig;
            bl.UserSession = userSession;
            bl.SetDependencies(appConfig, userSession);
            return bl;
        }
        //------------------------------------------------------------------------------------
        public static async Task<ActionResult<FwJsonDataTable>> BrowseAsync(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, Type logicType, 
            string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, BrowseRequest browseRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(uniqueId1FieldName))
                {
                    modelState.AddModelError(uniqueId1FieldName, $"BrowseRequest.filterfields must define a field: {uniqueId1FieldName}.");
                }
                if (string.IsNullOrEmpty(uniqueId1FieldValue))
                {
                    modelState.AddModelError(uniqueId1FieldName, $"BrowseRequest.filterfields must have a value for field: {uniqueId1FieldName}.");
                }
                if (!string.IsNullOrEmpty(uniqueId2FieldName) && string.IsNullOrEmpty(uniqueId2FieldValue))
                {
                    modelState.AddModelError(uniqueId2FieldName, $"BrowseRequest.filterfields must have a value for field: {uniqueId2FieldName}.");
                }
                if (!modelState.IsValid)
                {
                    return new BadRequestObjectResult(modelState);
                }
                if (!string.IsNullOrEmpty(uniqueId1FieldName)) browseRequest.filterfields[uniqueId1FieldName] = uniqueId1FieldValue;
                if (!string.IsNullOrEmpty(uniqueId2FieldName)) browseRequest.filterfields[uniqueId2FieldName] = uniqueId2FieldValue;
                FwBusinessLogic l = CreateBusinessLogic(logicType, appConfig, userSession);
                FwJsonDataTable dt = await l.BrowseAsync(browseRequest);
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        public static async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync(FwApplicationConfig appConfig, FwUserSession userSession, 
            ModelStateDictionary modelState, Type logicType, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, 
            BrowseRequest browseRequest, string worksheetName = "")
        {
            try
            {
                if (!modelState.IsValid)
                {
                    return new BadRequestObjectResult(modelState);
                }
                if (!string.IsNullOrEmpty(uniqueId1FieldName)) browseRequest.filterfields[uniqueId1FieldName] = uniqueId1FieldValue;
                if (!string.IsNullOrEmpty(uniqueId2FieldName)) browseRequest.filterfields[uniqueId2FieldName] = uniqueId2FieldValue;
                if (string.IsNullOrEmpty(worksheetName))
                {
                    string moduleName = logicType.Name.Replace("Logic", "");
                    worksheetName = moduleName;
                }

                FwBusinessLogic l = CreateBusinessLogic(logicType, appConfig, userSession);
                browseRequest.forexcel = true;
                FwJsonDataTable dt = await l.BrowseAsync(browseRequest);
                string strippedWorksheetName = new string(worksheetName.Where(c => char.IsLetterOrDigit(c)).ToArray());
                string filename = $"{userSession.WebUsersId}_{strippedWorksheetName}_{Guid.NewGuid().ToString().Replace("-", string.Empty)}_xlsx";
                string downloadFileName = $"{strippedWorksheetName}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}";
                string directory = FwDownloadController.GetDownloadsDirectory();
                string path = Path.Combine(directory, filename);

                // Delete any existing excel files belonginng to this user
                FwDownloadController.DeleteCurrentWebUserDownloads(userSession.WebUsersId);

                bool includeColorColumns = browseRequest.includecolorcolumns;
                bool includeIdColumns = browseRequest.includeidcolumns;

                if (!includeIdColumns || !includeColorColumns)
                {
                    foreach (FwJsonDataTableColumn col in dt.Columns)
                    {
                        string dataField = col.DataField.ToUpper();
                        if ((!includeIdColumns) && (dataField.EndsWith("ID") || dataField.EndsWith("KEY")))
                        {
                            col.IsVisible = false;
                        }

                        if ((!includeColorColumns) && (col.DataType.Equals(FwDataTypes.OleToHtmlColor)))
                        {
                            col.IsVisible = false;
                        }
                    }
                }

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
        public static async Task<ActionResult<GetResponse<T>>> DoGetManyAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, Type logicType,
            string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, GetRequest request)
        {
            if (!modelState.IsValid)
            {
                return new BadRequestObjectResult(modelState);
            }
            try
            {
                if (string.IsNullOrEmpty(uniqueId1FieldName))
                {
                    modelState.AddModelError(uniqueId1FieldName, $"BrowseRequest.filterfields must define a field: {uniqueId1FieldName}.");
                }
                if (string.IsNullOrEmpty(uniqueId1FieldValue))
                {
                    modelState.AddModelError(uniqueId1FieldName, $"BrowseRequest.filterfields must have a value for field: {uniqueId1FieldName}.");
                }
                if (!string.IsNullOrEmpty(uniqueId2FieldName) && string.IsNullOrEmpty(uniqueId2FieldValue))
                {
                    modelState.AddModelError(uniqueId2FieldName, $"BrowseRequest.filterfields must have a value for field: {uniqueId2FieldName}.");
                }
                if (!modelState.IsValid)
                {
                    return new BadRequestObjectResult(modelState);
                }
                var requestType = request.GetType();
                var propInfoUniqueId1Field = requestType.GetProperty(uniqueId1FieldName);
                var propInfoUniqueId2Field = requestType.GetProperty(uniqueId2FieldName);
                propInfoUniqueId1Field.SetValue(request, uniqueId1FieldValue);
                propInfoUniqueId2Field.SetValue(request, uniqueId2FieldValue);
                if (!string.IsNullOrEmpty(uniqueId1FieldName) && propInfoUniqueId1Field == null) propInfoUniqueId1Field.SetValue(request, uniqueId1FieldValue);
                if (!string.IsNullOrEmpty(uniqueId2FieldName) && propInfoUniqueId2Field == null) propInfoUniqueId2Field.SetValue(request, uniqueId2FieldValue);
                FwBusinessLogic l = FwBusinessLogic.CreateBusinessLogic(logicType, appConfig, userSession);
                GetResponse<T> response = await l.GetManyAsync<T>(request);
                return new OkObjectResult(response);
            }
            catch (ArgumentException ex)
            {
                modelState.AddModelError(ex.ParamName, ex.Message);
                return new BadRequestObjectResult(modelState);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------

        public static async Task<ActionResult<T>> GetOneAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, Type logicType, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, string id)
        {
            try
            {
                if (!modelState.IsValid)
                {
                    return new BadRequestObjectResult(modelState);
                }
                FwBusinessLogic l = CreateBusinessLogic(logicType, appConfig, userSession);
                if (id.Equals("emptyobject")) //justin 10/23/2018 temporary solution for listing Fields on Custom Forms and Duplicate Rules.  Will be replaced with a front-end solution to traverse the Security Tree (once ready)
                {
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

                    return new OkObjectResult(l);
                }
                else
                {
                    string[] ids = id.Split('~');
                    bool found = await l.LoadAsync<T>(ids);
                    if (!found)
                    {
                        return new NotFoundObjectResult($"404 - {logicType.Name} not found");
                    }
                    else
                    {
                        return new OkObjectResult(l);
                    }
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        public static async Task<IActionResult> GetEmptyObjectAsync(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, Type logicType, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue)
        {
            try
            {
                FwBusinessLogic l = CreateBusinessLogic(logicType, appConfig, userSession);
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
        public static async Task<ActionResult<GetResponse<T>>> GetManyAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, Type logicType, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, GetRequest request)
        {
            try
            {
                if (!modelState.IsValid)
                {
                    return new BadRequestObjectResult(modelState);
                }
                FwBusinessLogic l = FwBusinessLogic.CreateBusinessLogic(logicType, appConfig, userSession);
                GetResponse<T> response = await l.GetManyAsync<T>(request);
                if (!string.IsNullOrEmpty(uniqueId1FieldName)) request.filters[uniqueId1FieldName] = new GetManyRequestFilter(uniqueId1FieldName, "eq", uniqueId1FieldValue, false);
                if (!string.IsNullOrEmpty(uniqueId2FieldName)) request.filters[uniqueId2FieldName] = new GetManyRequestFilter(uniqueId2FieldName, "eq", uniqueId2FieldValue, false);
                return new OkObjectResult(response);
            }
            catch(ArgumentException ex)
            {
                modelState.AddModelError(ex.ParamName, ex.Message);
                return new BadRequestObjectResult(modelState);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        public static async Task<ActionResult<T>> DoNewAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, FwBusinessLogic model)
        {
            try
            {
                //if (!string.IsNullOrEmpty(uniqueId1FieldName))
                //{
                //    var propinfoUniqueId1Field = model.GetType().GetProperty(uniqueId1FieldName);
                //    if (propinfoUniqueId1Field.GetValue(model).ToString() != uniqueId1FieldValue)
                //    {
                //        modelState.AddModelError(uniqueId1FieldName, $"{uniqueId1FieldName} in url is not related to the object being created.");
                //    }
                //}
                //if (!string.IsNullOrEmpty(uniqueId2FieldName))
                //{
                //    var propinfoUniqueId2Field = model.GetType().GetProperty(uniqueId2FieldName);
                //    if (propinfoUniqueId2Field.GetValue(model).ToString() != uniqueId2FieldValue)
                //    {
                //        modelState.AddModelError(uniqueId2FieldName, $"{uniqueId2FieldName} in url is not related to the object being created.");
                //    }
                //}
                if (!modelState.IsValid)
                {
                    return new BadRequestObjectResult(modelState);
                }
                FwBusinessLogic original = null;
                TDataRecordSaveMode saveMode = TDataRecordSaveMode.smInsert;
                model.AppConfig = appConfig;
                model.UserSession = userSession;

                var result = new FwValidateResult();
                if (model.AllPrimaryKeysHaveValues)
                {
                    
                    return new BadRequestResult();
                }
                await model.ValidateBusinessLogicAsync(saveMode, original, result);

                if (result.IsValid)
                {
                    int rowsAffected = await model.SaveAsync(original, saveMode: TDataRecordSaveMode.smInsert);

                    if (model.ReloadOnSave)
                    {
                        await model.LoadAsync<T>();
                    }
                    return new OkObjectResult(model);
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
        public static async Task<ActionResult<T>> DoEditAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, FwBusinessLogic model)
        {
            try
            {
                //if (!string.IsNullOrEmpty(uniqueId1FieldName))
                //{
                //    var propinfoUniqueId1Field = model.GetType().GetProperty(uniqueId1FieldName);
                //    if (propinfoUniqueId1Field.GetValue(model).ToString() != uniqueId1FieldValue)
                //    {
                //        modelState.AddModelError(uniqueId1FieldName, $"{uniqueId1FieldName} in url is not related to the object being updated.");
                //    }
                //}
                //if (!string.IsNullOrEmpty(uniqueId2FieldName))
                //{
                //    var propinfoUniqueId2Field = model.GetType().GetProperty(uniqueId2FieldName);
                //    if (propinfoUniqueId2Field.GetValue(model).ToString() != uniqueId2FieldValue)
                //    {
                //        modelState.AddModelError(uniqueId2FieldName, $"{uniqueId2FieldName} in url is not related to the object being updated.");
                //    }
                //}
                if (!modelState.IsValid)
                {
                    return new BadRequestObjectResult(modelState);
                }
                FwBusinessLogic original = null;
                TDataRecordSaveMode saveMode = TDataRecordSaveMode.smInsert;
                model.AppConfig = appConfig;
                model.UserSession = userSession;

                var result = new FwValidateResult();
                if (!model.AllPrimaryKeysHaveValues)
                {
                    
                    return new BadRequestObjectResult(modelState);
                }
                //updating
                saveMode = TDataRecordSaveMode.smUpdate;

                if (model.LoadOriginalBeforeSaving)
                {
                    //load the original record from the database
                    original = FwBusinessLogic.CreateBusinessLogic(typeof(T), appConfig, userSession);
                    original.SetPrimaryKeys(model.GetPrimaryKeys());
                    bool exists = await original.LoadAsync<T>();
                    //if (!exists)
                    if (!exists)
                    {
                        return new NotFoundObjectResult($"404 - {typeof(T).Name} not found");
                    }
                }
 
                await model.ValidateBusinessLogicAsync(saveMode, original, result);

                if (result.IsValid)
                {
                    int rowsAffected = await model.SaveAsync(original, saveMode: TDataRecordSaveMode.smUpdate);

                    if (model.ReloadOnSave)
                    {
                        await model.LoadAsync<T>();
                    }
                    return new OkObjectResult(model);
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
        //public static async Task<IActionResult> DoSaveFormAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, SaveFormRequest request, Type type)
        //{
        //    try
        //    {
        //        if (!modelState.IsValid)
        //        {
        //            return new BadRequestObjectResult(modelState);
        //        }

        //        FwBusinessLogic logic = FwBusinessLogic.CreateBusinessLogic(type, appConfig, userSession);

        //        //populate the parent formfields from the request
        //        IDictionary<string, dynamic> miscfields = request.miscfields;
        //        foreach (var miscfield in miscfields)
        //        {
        //            var propertyInfo = logic.GetType().GetTypeInfo().GetProperties().DefaultIfEmpty(null).FirstOrDefault(p => p.Name == miscfield.Key);
        //            if (propertyInfo != null)
        //            {
        //                propertyInfo.SetValue(logic, miscfield.Value.value);
        //            }
        //        }

        //        //populate the uniqueids from the request
        //        //this section may not be needed mv 2017-08-25
        //        IDictionary<string, dynamic> ids = request.ids;
        //        foreach (var id in ids)
        //        {
        //            var propertyInfo = logic.GetType().GetTypeInfo().GetProperties().DefaultIfEmpty(null).FirstOrDefault(p => p.Name == id.Key);
        //            if (propertyInfo != null)
        //            {
        //                propertyInfo.SetValue(logic, id.Value.value);
        //            }
        //        }

        //        //populate the fields from the request
        //        IDictionary<string, dynamic> fields = request.fields;
        //        foreach (var field in fields)
        //        {
        //            var propertyInfo = logic.GetType().GetTypeInfo().GetProperties().DefaultIfEmpty(null).FirstOrDefault(p => p.Name == field.Key);
        //            if (propertyInfo != null)
        //            {
        //                propertyInfo.SetValue(logic, field.Value.value);
        //            }
        //        }

        //        logic.AppConfig = appConfig;
        //        var result = new FwValidateResult();

        //        if (logic.AllPrimaryKeysHaveValues)
        //        {
        //            //update
        //            await logic.ValidateBusinessLogicAsync(TDataRecordSaveMode.smUpdate, null, result);
        //        }
        //        else
        //        {
        //            //insert
        //            await logic.ValidateBusinessLogicAsync(TDataRecordSaveMode.smInsert, null, result);
        //        }
        //        if (result.IsValid)
        //        {
        //            await logic.SaveAsync(null);
        //            return new OkObjectResult(logic);
        //        }
        //        else
        //        {
        //            throw new Exception(result.ValidateMsg);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetApiExceptionResult(ex);
        //    }
        //}
        //------------------------------------------------------------------------------------
        public static async Task<ActionResult<bool>> DoDeleteAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, string id)
        {
            try
            {
                if (!modelState.IsValid)
                {
                    return new BadRequestObjectResult(modelState);
                }
                string[] ids = id.Split('~');
                FwBusinessLogic model = FwBusinessLogic.CreateBusinessLogic(typeof(T), appConfig, userSession);
                model.SetPrimaryKeys(ids);
                if (model.LoadOriginalBeforeDeleting)
                {
                    //load the original record from the database
                    bool exists = await model.LoadAsync<T>();
                    if (!string.IsNullOrEmpty(uniqueId1FieldName))
                    {
                        var propinfoUniqueId1Field = model.GetType().GetProperty(uniqueId1FieldName);
                        if (propinfoUniqueId1Field.GetValue(model).ToString() != uniqueId1FieldValue)
                        {
                            modelState.AddModelError(uniqueId1FieldName, $"{uniqueId1FieldName} in url is not related to the object being deleted.");
                        }
                    }
                    if (!string.IsNullOrEmpty(uniqueId2FieldName))
                    {
                        var propinfoUniqueId2Field = model.GetType().GetProperty(uniqueId2FieldName);
                        if (propinfoUniqueId2Field.GetValue(model).ToString() != uniqueId2FieldValue)
                        {
                            modelState.AddModelError(uniqueId2FieldName, $"{uniqueId2FieldName} in url is not related to the object being deleted.");
                        }
                    }
                    if (!exists)
                    {
                        return new NotFoundObjectResult($"404 - {typeof(T).Name} not found");
                    }
                }
                if (!modelState.IsValid)
                {
                    return new BadRequestObjectResult(modelState);
                }
                bool success = await model.DeleteAsync();
                return new OkObjectResult(success);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        public static ObjectResult GetApiExceptionResult(Exception ex)
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
            var result = new ObjectResult(jsonException);
            result.StatusCode = jsonException.StatusCode;
            return result;
        }
        //------------------------------------------------------------------------------------
    }
}