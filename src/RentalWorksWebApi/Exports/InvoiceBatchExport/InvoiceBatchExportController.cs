using FwCore.Controllers;
using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Data;
using WebApi.Logic;

namespace WebApi.Modules.Exports.InvoiceBatchExport
{
    [Route("api/v1/[controller]")]
    //[ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "GI2FxKtrjja1")]
    public class InvoiceBatchExportController : AppExportController
    {
        public InvoiceBatchExportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(InvoiceBatchExportLoader); }

        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicebatchexport/export
        [HttpPost("export")]
        [FwControllerMethod(Id: "gfekPjE6qLe0", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<InvoiceBatchExportResponse>> Export([FromBody]InvoiceBatchExportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InvoiceBatchExportLoader l = new InvoiceBatchExportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                await l.DoLoad<InvoiceBatchExportLoader>(request);
                
                string exportString = await AppFunc.GetStringDataAsync(AppConfig, "webdataexportformat", "dataexportformatid", request.DataExportFormatId, "exportstring");
                //request must contain a DataExportFormatId (provided from the field on the requesting Page)
                //we need a generic method here that will take an instance of a AppExportLoader ("l" in this scope) and an Export Format String (pulled from the provided DataExportFormatId)
                //   the method should use handlebars to produce a giant string from the "l" data object and the desired export format.
                //   the method should then produce a text file with that giant string and download it back to the page.
                await Export<InvoiceBatchExportLoader>(l, exportString);

                //for (int i = 0; i <= l.Invoices.Count - 1; i++)
                //{
                //    var invoice = l.Invoices[i];
                //    if (!string.IsNullOrEmpty(invoice.InvoiceId))
                //    {
                //        string lineText = exportString;
                //        string[] fields = lineText.Split(new string[] { "{{", "}}" }, StringSplitOptions.RemoveEmptyEntries);

                //        foreach (string s in fields)
                //        {
                //            if (!s.StartsWith(@"\"))
                //            {
                //                int childSeparatorIndex = s.IndexOf(".", StringComparison.Ordinal);
                //                if (childSeparatorIndex.Equals(-1))
                //                {
                //                    var value = l.GetType().GetProperty(s).GetValue(l);
                //                    if (!value.Equals(null)) 
                //                    {
                //                        lineText = lineText.Replace("{{" + s + "}}", value.ToString());
                //                    }
                                 
                //                } else
                //                {
                //                    string parentField = s.Substring(0, childSeparatorIndex);
                //                    string childField = s.Substring(childSeparatorIndex + 1);

                //                    var children = l.GetType().GetProperty(parentField).GetValue(l);
                //                    string childVal = children.GetType().GetProperty(childField).GetValue(children).ToString();
                //                    lineText = lineText.Replace("{{" + parentField +"." + childField + "}}", childVal);
                //                }
                //            }
                //        }
                //        sb.AppendLine(lineText);

                //    }
                //}
               // *
               // *COPIED FROM InvoiceProcessBatchFunc.cs
               //*
              // here we are creating a downloadable file that will live in the API "downloads" directory
              //string downloadFileName = l.BatchNumber + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".csv";
              //  string filename = UserSession.WebUsersId + "_" + l.BatchNumber + "_" + Guid.NewGuid().ToString().Replace("-", string.Empty) + "_csv";
              //  string directory = FwDownloadController.GetDownloadsDirectory();
              //  string path = Path.Combine(directory, filename);

              //  using (var tw = new StreamWriter(path, false)) // false here will initialize the file fresh, no appending
              //  {
              //      tw.Write(sb);
              //      tw.Flush();
              //      tw.Close();
              //  }

                InvoiceBatchExportResponse response = new InvoiceBatchExportResponse();
                response.downloadUrl = "";
                //response.downloadUrl = $"api/v1/download/{filename}?downloadasfilename={downloadFileName}"; 
                                            // populate this field with the download file path and name
                                            //for now, the actual filename of the download file is not critical. But going forward we may need to add a field somewhere for user to define how they want the file named

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
