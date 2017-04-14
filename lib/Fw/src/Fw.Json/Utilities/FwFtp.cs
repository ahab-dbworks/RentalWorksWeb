using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Fw.Json.SqlServer.Entities;

namespace Fw.Json.Utilities
{
    public class FwFtp
    {
        public string Server   = string.Empty;
        public string UserName = string.Empty;
        public string Password = string.Empty;
        public NetworkCredential Credentials = null;
        public bool UseSSL;
        //---------------------------------------------------------------------------------------------
        public FwFtp(string server, int port, string userName, string password, bool useSSL)
        {
            this.Server      = server + ":" + port.ToString();
            this.UserName    = userName; 
            this.Password    = password;
            this.Credentials = new NetworkCredential(this.UserName, this.Password);
            this.UseSSL      = useSSL;
        }
        //---------------------------------------------------------------------------------------------
        public FtpWebResponse UploadFile(string sourcePath, string destinationPath) 
        {            
            FtpWebRequest ftpRequest = null;
            FtpWebResponse ftpResponse = null;
            byte[] fileContents = null;
            
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + this.Server + destinationPath)); 
            ftpRequest.EnableSsl = this.UseSSL;
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
            ftpRequest.Proxy = null;
            ftpRequest.UseBinary = true;
            ftpRequest.Credentials = this.Credentials;
            ftpRequest.UsePassive = true;
            fileContents = File.ReadAllBytes(sourcePath);                
            using (Stream outputStream = ftpRequest.GetRequestStream())
            {
                outputStream.Write(fileContents, 0, fileContents.Length);                
            }                
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

            return ftpResponse;
        }
        //---------------------------------------------------------------------------------------------
        public static void FtpFile(string description, string filePath)
        {
            FwSource source;
            FwFtp ftpclient;
            string ftppath;

            source = FwSource.Loads(description, "");
            if (!source.Disabled)
            {
                ftpclient = new FwFtp(source.FtpHost, source.FtpPort, source.FtpUserName, source.FtpPassword, source.FtpSsl);
                ftppath   = source.FtpPath + Path.GetFileName(filePath);
                ftpclient.UploadFile(filePath, ftppath);
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
