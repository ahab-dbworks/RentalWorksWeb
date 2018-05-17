using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
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
            await Task.CompletedTask;
            if (fileName == null) return Content("The filename was not specified.");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/temp/downloads", fileName);
            if (!System.IO.File.Exists(path)) return Content("The requested file does not exist on the server.");
            var ext = Path.GetExtension(path).ToLowerInvariant();
            string contentType = string.Empty;
            switch (ext)
            {
                case ".txt": contentType = "text/plain"; break;
                case ".pdf": contentType = "application/pdf"; break;
                case ".doc": contentType = "application/vnd.ms-word"; break;
                case ".docx": contentType = "application/vnd.ms-word"; break;
                case ".xls": contentType = "application/vnd.ms-excel"; break;
                case ".xlsx": contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; break;
                case ".png": contentType = "image/png"; break;
                case ".jpg": contentType = "image/jpeg"; break;
                case ".jpeg": contentType = "image/jpeg"; break;
                case ".gif": contentType = "image/gif"; break;
                case ".csv": contentType = "text/csv"; break;
            }
            // mv 2018-05-16 don't put a using block on FileStream, MVC will dispose of the stream when the result is returned
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Delete, 4096, FileOptions.DeleteOnClose);
            var result = new FileStreamResult(stream, contentType);
            result.FileDownloadName = (!string.IsNullOrEmpty(downloadAsFileName)) ? downloadAsFileName : fileName;
            return result;
        }
        //------------------------------------------------------------------------------------
    }
}