using FwStandard.Models;
using System;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Threading.Tasks;
using WebApi.Logic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Linq;

namespace WebApi.Modules.Administrator.Update
{

    public class AvailableVersionsRequest
    {
        public string CurrentVersion { get; set; }
        public bool OnlyIncludeNewerVersions { get; set; }
    }

    public class AvailableVersionsResponse : TSpStatusResponse {
        public List<string> Versions { get; set; } = new List<string>();
    }


    public class ApplyUpdateRequest
    {
        public string ToVersion { get; set; } // if left blank, then just update to the latest available version
    }

    public class ApplyUpdateResponse : TSpStatusResponse { }

    public class UpdaterRequest
    {
        public string ToVersion { get; set; } // if left blank, then just update to the latest available version
        public string SQLServerName { get; set; }
        public string DatabaseName { get; set; }
        public string WebApplicationPool { get; set; }
        public string WebInstallPath { get; set; }
        public string ApiApplicationPool { get; set; }
        public string ApiInstallPath { get; set; }
    }

    public static class UpdateFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<AvailableVersionsResponse> GetAvailableVersions(FwApplicationConfig appConfig, FwUserSession userSession, AvailableVersionsRequest request)
        {
            AvailableVersionsResponse response = new AvailableVersionsResponse();

            if (string.IsNullOrEmpty(request.CurrentVersion))
            {
                response.msg = "Supply a value for CurrentVersion in the request.";
            }
            else if (request.CurrentVersion.Split(".").Length != 4)
            {
                response.msg = "Invalid format for CurrentVersion (" + request.CurrentVersion + ").  Format must be Major.Minor.Release.Build";
            }
            else
            {
                string systemName = "RentalWorksWeb";
                string currentMajorMinorRelease = request.CurrentVersion.Substring(0, request.CurrentVersion.LastIndexOf('.'));

                if (string.IsNullOrEmpty(currentMajorMinorRelease))
                {
                    response.msg = "Invalid format for CurrentVersion (" + request.CurrentVersion + ").  Cannot determine Major, Minor, and Release.";
                }
                else
                {
                    try
                    {
                        string ftpDirectory = "ftp://ftp.dbworks.com/" + systemName + "/" + currentMajorMinorRelease;
                        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpDirectory);
                        ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                        ftpRequest.Credentials = new NetworkCredential("update", "update");
                        FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                        Stream responseStream = ftpResponse.GetResponseStream();
                        StreamReader reader = new StreamReader(responseStream);
                        string names = reader.ReadToEnd();

                        reader.Close();
                        ftpResponse.Close();

                        foreach (string name in names.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList())
                        {
                            string fileName = name.ToLower();
                            if ((fileName.EndsWith("zip")) && (fileName.Contains("/rentalworksweb_")))
                            {
                                string version = fileName;
                                version = version.Replace(currentMajorMinorRelease + "/rentalworksweb_", ""); 
                                version = version.Replace(".zip", "");
                                version = version.Replace("_", ".");

                                bool includeVersion = ((!request.OnlyIncludeNewerVersions) || (version.CompareTo(request.CurrentVersion) > 0));

                                if (includeVersion)
                                {
                                    response.Versions.Add(version);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        response.msg = e.ToString();
                    }
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ApplyUpdateResponse> ApplyUpdate(FwApplicationConfig appConfig, FwUserSession userSession, ApplyUpdateRequest request)
        {
            ApplyUpdateResponse response = new ApplyUpdateResponse();

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(appConfig.DatabaseSettings.ConnectionString);

            UpdaterRequest updaterRequest = new UpdaterRequest();
            updaterRequest.ToVersion = request.ToVersion;
            updaterRequest.SQLServerName = connectionStringBuilder.DataSource;
            updaterRequest.DatabaseName = connectionStringBuilder.InitialCatalog;
            updaterRequest.ApiApplicationPool = appConfig.ApplicationPool;
            updaterRequest.ApiInstallPath = GetCurrentApiApplicationPath();
            updaterRequest.WebApplicationPool = GetCurrentWebApplicationPoolName(appConfig.ApplicationPool);
            updaterRequest.WebInstallPath = GetCurrentWebApplicationPath();

            if (string.IsNullOrEmpty(updaterRequest.ApiApplicationPool))
            {
                response.msg = "Could not determine API Application Pool from appsettings.json.";
            }
            else if (string.IsNullOrEmpty(updaterRequest.ApiInstallPath))
            {
                response.msg = "Could not determine API Installation path.";
            }
            else if (string.IsNullOrEmpty(updaterRequest.WebApplicationPool))
            {
                response.msg = "Could not determine Web Application Pool.";
            }
            else if (string.IsNullOrEmpty(updaterRequest.WebInstallPath))
            {
                response.msg = "Could not determine Web Installation path.";
            }
            else if (string.IsNullOrEmpty(updaterRequest.SQLServerName))
            {
                response.msg = "Could not determine SQL Server name from appsettings.json.";
            }
            else if (string.IsNullOrEmpty(updaterRequest.DatabaseName))
            {
                response.msg = "Could not determine Database name from appsettings.json.";
            }
            else
            {

                // need to first apply all hotfixes here

                try
                {
                    string server = "127.0.0.1";
                    int port = 18811;

                    TcpClient client = new TcpClient(server, port);
                    JsonSerializer serializer = new JsonSerializer();
                    string updaterRequestSt = JsonConvert.SerializeObject(updaterRequest);

                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(updaterRequestSt);

                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);
                    response.success = true;

                    // once this command is sent to the updater service, this API service will be bounced
                    // the following "Read" command is here temporarily to cause a delay

                    data = new Byte[256];
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    string updaterResponseStr = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                    Console.WriteLine("Response received: {0}", updaterResponseStr);

                    response = JsonConvert.DeserializeObject<ApplyUpdateResponse>(updaterResponseStr);

                    stream.Close();
                    client.Close();
                }
                catch (ArgumentNullException e)
                {
                    response.msg = "ArgumentNullException: " + e.ToString();
                    Console.WriteLine(response.msg);
                }
                catch (SocketException e)
                {
                    response.msg = "There is no RentalWorksWeb Updater Service available.";
                    Console.WriteLine(response.msg);
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        private static string GetCurrentApiApplicationPath()
        {
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            appPath = System.IO.Path.GetDirectoryName(appPath);
            return appPath;
        }
        //-------------------------------------------------------------------------------------------------------
        private static string GetCurrentWebApplicationPath()
        {
            string apiAppPath = GetCurrentApiApplicationPath();  //c:\inetpub\wwwroot\RentalWorksWebApi

            string webAppPath = "";
            if (apiAppPath.ToLower().EndsWith("api"))
            {
                webAppPath = apiAppPath.Substring(0, apiAppPath.Length - 3);
            }
            return webAppPath;
        }
        //-------------------------------------------------------------------------------------------------------
        private static string GetCurrentWebApplicationPoolName(string apiAppPoolName)
        {
            string webAppPoolName = "";
            if (apiAppPoolName.ToLower().EndsWith("api"))   //rentalworkswebapi
            {
                webAppPoolName = apiAppPoolName.Substring(0, apiAppPoolName.Length - 3);
            }
            return webAppPoolName;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
