using FwCore.Controllers;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Threading.Tasks;
using static FwCore.Controllers.FwDataController;

namespace FwCore.Grids.AppDocument
{
    public static class FwAppDocumentController
    {
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<FwJsonDataTable>> BrowseAsync(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, 
            Type logicType, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, BrowseRequest browseRequest)
        {
            return await FwGridController.BrowseAsync(appConfig, userSession, modelState, logicType, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<GetResponse<T>>> GetManyAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            Type logicType, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, GetRequest request)
        {
            if (!string.IsNullOrEmpty(uniqueId1FieldName))
            {
                request.filters[uniqueId1FieldName] = new GetManyRequestFilter(uniqueId1FieldName, "eq", uniqueId1FieldValue, false);
            }
            if (!string.IsNullOrEmpty(uniqueId2FieldName))
            {
                request.filters[uniqueId2FieldName] = new GetManyRequestFilter(uniqueId2FieldName, "eq", uniqueId2FieldValue, false);
            }
            return await FwGridController.GetManyAsync<T>(appConfig, userSession, modelState, logicType, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, request);
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync(FwApplicationConfig appConfig, FwUserSession userSession, 
            ModelStateDictionary modelState, Type logicType, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, 
            BrowseRequest browseRequest)
        {
            return await FwGridController.ExportExcelXlsxFileAsync(appConfig, userSession, modelState, logicType, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<T>> GetOneAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, Type logicType, 
            string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, string appDocumentId)
        {
            return await FwGridController.GetOneAsync<T>(appConfig, userSession, modelState, logicType, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, appDocumentId);
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<T>> NewAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, 
            string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, T model) where T : AppDocumentLogic
        {
            try
            {
                model.AppConfig = appConfig;
                model.UserSession = userSession;
                var date = DateTime.UtcNow;
                model.AttachDate = date.ToString("yyyy-MM-dd");
                model.AttachTime = date.ToString("hh:mm:ss");
                model.DateStamp = date.ToString("yyyy-MM-ddThh:mm:ssz");
                model.SetUniqueId1(uniqueId1FieldValue);
                model.SetUniqueId2(uniqueId2FieldValue);
                await model.SaveAppDocumentImageAsync(FwStandard.Grids.AppDocument.AppDocumentLogic.SaveModes.Insert);
                return model;
            }
            catch(Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<T>> EditAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, 
            string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, string documentId, T model) where T : AppDocumentLogic
        {
            try
            {
                if (model.DocumentId != documentId)
                {
                    modelState.AddModelError("AppDocumentId", "AppDocumentId in the url does not match the AppDocumentId in the model.");
                    return new BadRequestObjectResult(modelState);
                }
                model.AppConfig = appConfig;
                model.UserSession = userSession;
                var date = DateTime.UtcNow;
                model.AttachDate = date.ToString("yyyy-MM-dd");
                model.AttachTime = date.ToString("hh:mm:ss");
                model.DateStamp = date.ToString("yyyy-MM-ddThh:mm:ssz");
                await model.SaveAppDocumentImageAsync(FwStandard.Grids.AppDocument.AppDocumentLogic.SaveModes.Update);
                return model;
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<bool>> DeleteAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, 
            string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, string appDocumentId)
        {
            return await FwGridController.DoDeleteAsync<T>(appConfig, userSession, modelState, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, appDocumentId);
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<GetDocumentThumbnailsResponse>> GetThumbnailsAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, int pageNo, int pageSize)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                GetDocumentThumbnailsResponse response = await bl.GetThumbnailsAsync(validateAginstTable, validateAgainstField, pageNo, pageSize);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<GetDocumentImageResponse>> GetImageAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, string appImageId)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                GetDocumentImageResponse response = await bl.GetImageAsync(validateAginstTable, validateAgainstField, appImageId);
                if (response == null)
                {
                    return new NotFoundObjectResult("Image not found.");
                }
                else
                {
                    return new OkObjectResult(response);
                }
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<T>> AttachImageFromDataUrlAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, PutDocumentImageRequest request)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                await bl.LoadAsync<T>();
                var response = await bl.AttachImageAsync(validateAginstTable, validateAgainstField, request.DataUrl);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<T>> AttachImageFromUploadAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, IFormFile file)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                await bl.LoadAsync<T>();
                byte[] image = null;
                using (Stream stream = file.OpenReadStream())
                {
                    image = new byte[stream.Length];
                    stream.Read(image, 0, image.Length);
                }
                var response = await bl.AttachImageAsync(validateAginstTable, validateAgainstField, image);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<bool>> DeleteImageAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, string appImageId)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                bool response = await bl.DeleteImageAsync(validateAginstTable, validateAgainstField, appImageId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<FileContentResult>> GetFileAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId) where T: AppDocumentLogic
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                await bl.LoadAsync<T>();
                var documentFile = await bl.GetFileAsync(validateAginstTable, validateAgainstField);
                if (documentFile == null)
                {
                    return new NotFoundObjectResult("File not found.");
                }
                else
                {
                    return new FileContentResult(documentFile.File.Data, documentFile.File.ContentType)
                    {
                        FileDownloadName = bl.Description + "." + documentFile.File.Extension
                    };
                }
            }
            catch(Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<T>> AttachFileFromDataUrlAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, PutDocumentFileRequest request)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                await bl.LoadAsync<T>();
                var response = await bl.AttachFileAsync(validateAginstTable, validateAgainstField, request.DataUrl, request.FileExtension);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<T>> AttachFileFromUploadAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, IFormFile file)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                await bl.LoadAsync<T>();
                byte[] fileData = null;
                using (Stream stream = file.OpenReadStream())
                {
                    fileData = new byte[stream.Length];
                    stream.Read(fileData, 0, fileData.Length);
                }
                string fileExtension = Path.GetExtension(file.FileName).ToUpper();
                if (fileExtension.StartsWith("."))
                {
                    fileExtension = fileExtension.Substring(1);
                }
                var response = await bl.AttachFileAsync(validateAginstTable, validateAgainstField, fileData, fileExtension);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<bool>> DeleteFileAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                await bl.LoadAsync<T>();
                var response = await bl.DeleteFileAsync(validateAginstTable, validateAgainstField);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
