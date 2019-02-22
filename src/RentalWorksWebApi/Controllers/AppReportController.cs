﻿using FwCore.Controllers;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static FwCore.Controllers.FwDataController;

namespace WebApi.Controllers
{
    public class AppReportRequest
    {
        public bool IncludeSubHeadingsAndSubTotals { get; set; } = true;
        public bool IncludeIdColumns { get; set; } = true;
        public bool IncludeColorColumns { get; set; } = true;
    }

    public abstract class AppReportController : FwReportController
    {
        //------------------------------------------------------------------------------------
        public AppReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
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
        protected virtual async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DoExportExcelXlsxFileAsync(FwJsonDataTable dt, string worksheetName = "", bool includeIdColumns  = true, bool includeColorColumns = true)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (string.IsNullOrEmpty(worksheetName))
                {
                    worksheetName = GetReportFileName();
                }

                string strippedWorksheetName = new string(worksheetName.Where(c => char.IsLetterOrDigit(c)).ToArray());
                string downloadFileName = $"{strippedWorksheetName}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}";
                string filename = $"{this.UserSession.WebUsersId}_{strippedWorksheetName}_{Guid.NewGuid().ToString().Replace("-", string.Empty)}_xlsx";
                string directory = FwDownloadController.GetDownloadsDirectory();
                string path = Path.Combine(directory, filename);

                // Delete any existing excel files belonginng to this user
                FwDownloadController.DeleteCurrentWebUserDownloads(this.UserSession.WebUsersId);

                if (!includeIdColumns || !includeColorColumns)
                {
                    foreach (FwJsonDataTableColumn col in dt.Columns)
                    {
                        if ((!includeIdColumns) && (col.DataField.EndsWith("Id") || col.DataField.EndsWith("Key")))
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
    }
}
