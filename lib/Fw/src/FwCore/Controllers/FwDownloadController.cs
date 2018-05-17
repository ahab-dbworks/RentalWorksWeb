using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FwCore.Controllers
{
    public class FwDownloadController : FwController
    {
        FwApplicationConfig appConfig;
        //------------------------------------------------------------------------------------
        public FwDownloadController(IOptions<FwApplicationConfig> appConfig) : base(appConfig)
        {
            this.appConfig = appConfig.Value;
        }
        //------------------------------------------------------------------------------------
        public static string GetDownloadsDirectory()
        {
            string directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + Path.DirectorySeparatorChar + "temp" + Path.DirectorySeparatorChar + "downloads");
            return directory;
        }
        //------------------------------------------------------------------------------------
        public static void DeleteCurrentWebUserDownloads(string webusersid)
        {
            // Delete any existing excel files belonginng to this user
            string directory = FwDownloadController.GetDownloadsDirectory();
            string[] oldFilePaths = Directory.GetFiles(directory, webusersid + "*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < oldFilePaths.Length; i++)
            {
                System.IO.File.Delete(oldFilePaths[i]);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoGetAsync(string fileName, string downloadAsFileName)
        {
            if (string.IsNullOrEmpty(fileName)) return BadRequest();
            if (string.IsNullOrEmpty(downloadAsFileName)) return BadRequest();
            string whitelistFileName = new string(fileName.Where(c => char.IsLetterOrDigit(c) || (c == '_')).ToArray());
            if (fileName != whitelistFileName) return BadRequest();
            string whitelistDownloadAsFileName = new string(downloadAsFileName.Where(c => char.IsLetterOrDigit(c) || c == '_'  || c == '-' || c == '.').ToArray());
            if (downloadAsFileName != whitelistDownloadAsFileName) return BadRequest();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/temp/downloads", whitelistFileName);
            if (!System.IO.File.Exists(path)) return Content("The requested file does not exist on the server.");
            var ext = Path.GetExtension(path).ToLowerInvariant();
            var contentType = "";
            if (whitelistFileName.EndsWith("_xlsx"))
            {
                contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            else
            {
                return BadRequest();
            }
            if (!System.IO.File.Exists(path)) return BadRequest();
            // mv 2018-05-16 don't put a using block on FileStream, MVC will dispose of the stream when the result is returned
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Delete, 4096, FileOptions.DeleteOnClose);
            var result = new FileStreamResult(stream, contentType);
            result.FileDownloadName = (!string.IsNullOrEmpty(whitelistDownloadAsFileName)) ? whitelistDownloadAsFileName : whitelistFileName;
            await Task.CompletedTask;
            return result;
        }
        //------------------------------------------------------------------------------------
    }
}