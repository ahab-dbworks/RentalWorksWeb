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
using FwStandard.SqlServer;
using System.Data;
using WebApi.Modules.Administrator.SystemUpdateHistory;
using FwCore.Controllers;

namespace WebApi.Modules.Administrator.SystemUpdate
{

    public class AvailableVersionsRequest
    {
        public string CurrentVersion { get; set; }
        public bool OnlyIncludeNewerVersions { get; set; }
    }

    public class AvailableVersion
    {
        public string value { get; set; }
        public string text { get; set; }

    }

    public class AvailableVersionsResponse : TSpStatusResponse
    {
        public List<string> VersionsList { get; set; } = new List<string>();
        public List<AvailableVersion> Versions { get; set; } = new List<AvailableVersion>();
    }

    public class BuildDocumentsRequest
    {
        public string CurrentVersion { get; set; }
        public bool OnlyIncludeNewerVersions { get; set; }
    }

    public class BuildDocumentsResponse : TSpStatusResponse
    {
        public List<string> DocumentsList { get; set; } = new List<string>();
    }


    public class ApplyUpdateRequest
    {
        public string CurrentVersion { get; set; }
        public string ToVersion { get; set; }
    }

    public class ApplyUpdateResponse : TSpStatusResponse { }

    public class UpdaterRequest
    {
        public string ToVersion { get; set; }
        public string SQLServerName { get; set; }
        public string DatabaseName { get; set; }
        public string WebApplicationPool { get; set; }
        public string WebInstallPath { get; set; }
        public string ApiApplicationPool { get; set; }
        public string ApiInstallPath { get; set; }
    }

    public class DownloadBuildDocumentRequest
    {
        public string Version { get; set; }
    }

    public class DownloadBuildDocumentResponse : TSpStatusResponse
    {
        public string downloadUrl { get; set; }
    }



    public static class SystemUpdateFunc
    {
        //-------------------------------------------------------------------------------------------------------
        //public static async Task<AvailableVersionsResponse> GetAvailableVersions(FwApplicationConfig appConfig, FwUserSession userSession, AvailableVersionsRequest request)
        public static AvailableVersionsResponse GetAvailableVersions(FwApplicationConfig appConfig, FwUserSession userSession, AvailableVersionsRequest request)
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
                string[] versionPieces = request.CurrentVersion.Split(".");

                if ((string.IsNullOrEmpty(versionPieces[0])) || (string.IsNullOrEmpty(versionPieces[1])) || (string.IsNullOrEmpty(versionPieces[2])))
                {
                    response.msg = "Invalid format for CurrentVersion (" + request.CurrentVersion + ").  Cannot determine Major, Minor, and Release.";
                }
                else
                {
                    try
                    {
                        string systemName = "RentalWorksWeb";
                        string currentMajorMinorRelease = versionPieces[0] + "." + versionPieces[1] + "." + versionPieces[2];
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
                                    AvailableVersion v = new AvailableVersion();
                                    v.text = version;
                                    v.value = version;
                                    response.Versions.Add(v);
                                    response.VersionsList.Add(version);
                                }
                            }
                        }
                        response.success = true;
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
        public static BuildDocumentsResponse GetBuildDocuments(FwApplicationConfig appConfig, FwUserSession userSession, BuildDocumentsRequest request)
        {
            BuildDocumentsResponse response = new BuildDocumentsResponse();

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
                string[] documentPieces = request.CurrentVersion.Split(".");

                if ((string.IsNullOrEmpty(documentPieces[0])) || (string.IsNullOrEmpty(documentPieces[1])) || (string.IsNullOrEmpty(documentPieces[2])))
                {
                    response.msg = "Invalid format for CurrentVersion (" + request.CurrentVersion + ").  Cannot determine Major, Minor, and Release.";
                }
                else
                {
                    try
                    {
                        string systemName = "RentalWorksWeb";
                        string currentMajorMinorRelease = documentPieces[0] + "." + documentPieces[1] + "." + documentPieces[2];
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
                            if ((fileName.EndsWith("pdf")) && (fileName.Contains("/v" + currentMajorMinorRelease)))
                            {
                                string version = fileName;
                                version = version.Replace(currentMajorMinorRelease + "/v", "");
                                version = version.Replace(".pdf", "");

                                bool includeDocument = ((!request.OnlyIncludeNewerVersions) || (version.CompareTo(request.CurrentVersion) > 0));

                                if (includeDocument)
                                {
                                    response.DocumentsList.Add(version);
                                }
                            }
                        }
                        response.success = true;
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
        public static DownloadBuildDocumentResponse DownloadBuildDocument(FwApplicationConfig appConfig, FwUserSession userSession, DownloadBuildDocumentRequest request) {
            DownloadBuildDocumentResponse response = new DownloadBuildDocumentResponse();

            if (string.IsNullOrEmpty(request.Version))
            {
                response.msg = "Supply a value for Version in the request.";
            }
            else if (request.Version.Split(".").Length != 4)
            {
                response.msg = "Invalid format for Version (" + request.Version + ").  Format must be Major.Minor.Release.Build";
            }
            else
            {
                string[] documentPieces = request.Version.Split(".");

                if ((string.IsNullOrEmpty(documentPieces[0])) || (string.IsNullOrEmpty(documentPieces[1])) || (string.IsNullOrEmpty(documentPieces[2])))
                {
                    response.msg = "Invalid format for Version (" + request.Version + ").  Cannot determine Major, Minor, and Release.";
                }
                else
                {
                    try
                    {
                        string systemName = "RentalWorksWeb";
                        string majorMinorRelease = documentPieces[0] + "." + documentPieces[1] + "." + documentPieces[2];

                        string remotePdfFileName = request.Version + ".pdf";
                        string remotePath = "ftp://ftp.dbworks.com/" + systemName + "/" + majorMinorRelease + "/v" + remotePdfFileName;

                        string localPdfFile = request.Version.Replace('.', '_') + "_pdf";
                        string localPath = "wwwroot/temp/downloads/" + localPdfFile;

                        WebClient client = new WebClient();
                        client.Credentials = new NetworkCredential("update", "update");
                        client.DownloadFile(remotePath, localPath);

                        response.downloadUrl = $"api/v1/download/{localPdfFile}?downloadasfilename={request.Version}.pdf";
                        response.success = true;
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

            SystemUpdateHistoryLogic h = new SystemUpdateHistoryLogic();
            h.SetDependencies(appConfig, userSession);
            h.UsersId = userSession.UsersId;
            h.FromVersion = request.CurrentVersion;
            h.ToVersion = request.ToVersion;
            h.UpdateDateTime = DateTime.Now;

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(appConfig.DatabaseSettings.ConnectionString);

            UpdaterRequest updaterRequest = new UpdaterRequest();
            updaterRequest.ToVersion = request.ToVersion;
            updaterRequest.SQLServerName = connectionStringBuilder.DataSource;
            updaterRequest.DatabaseName = connectionStringBuilder.InitialCatalog;
            updaterRequest.ApiApplicationPool = appConfig.ApplicationPool;
            updaterRequest.ApiInstallPath = GetCurrentApiApplicationPath();
            updaterRequest.WebApplicationPool = GetCurrentWebApplicationPoolName(appConfig.ApplicationPool);
            updaterRequest.WebInstallPath = GetCurrentWebApplicationPath();

            string hotfixInstallerConnectionString = "Server=" + connectionStringBuilder.DataSource + ";Database=" + connectionStringBuilder.InitialCatalog + ";User Id=dbworks;Password=db2424;";  // user/pass hard-coded for now
            string updaterServer = "127.0.0.1";
            int updaterPort = 18811;

            bool doInstallHotfixes = (request.ToVersion.CompareTo(request.CurrentVersion) >= 0);  // only apply hotfixes if upgrading or refreshing current version, not downgrading

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
            else if (string.IsNullOrEmpty(request.CurrentVersion))
            {
                response.msg = "Current Version cannot be blank.";
            }
            else if (string.IsNullOrEmpty(request.ToVersion))
            {
                response.msg = "Update To Version cannot be blank.";
            }
            else
            {
                response.success = true;

                // attempt to connect to the database with dbworks account to confirm credentials
                if (response.success)
                {
                    if (doInstallHotfixes)
                    {
                        try
                        {
                            // using native SqlConnection object here to bypass block on the "dbworks" login
                            using (SqlConnection conn = new SqlConnection(hotfixInstallerConnectionString))
                            {
                                conn.Open();
                                conn.Close();
                            }
                        }
                        catch (Exception)
                        {
                            response.success = false;
                            response.msg = "Cannot connect to the database to install hotfixes.";
                        }
                    }
                }


                // attempt to connect to the updater service to confirm send/receive connectivity
                if (response.success)
                {
                    try
                    {
                        TcpClient client = new TcpClient(updaterServer, updaterPort);
                        JsonSerializer serializer = new JsonSerializer();
                        string testRequestStr = "TESTCHECK";
                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(testRequestStr);
                        NetworkStream stream = client.GetStream();
                        stream.Write(data, 0, data.Length);
                        data = new Byte[256];
                        Int32 bytes = stream.Read(data, 0, data.Length);
                        string testResponseStr = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        stream.Close();
                        client.Close();
                    }
                    catch (Exception)
                    {
                        response.msg = "There is no RentalWorksWeb Updater Service available.";
                        response.success = false;
                    }
                }

                // apply all hotfixes here
                if (response.success)
                {
                    if (doInstallHotfixes)
                    {
                        try
                        {
                            // using native SqlConnection object here to bypass block on the "dbworks" login
                            using (SqlConnection conn = new SqlConnection(hotfixInstallerConnectionString))
                            {
                                SqlCommand qry = new SqlCommand("fw_installhotfixes", conn);
                                qry.CommandTimeout = 1800;  // time-out at 1,800 seconds (30 minutes)  // default is 30 (30 seconds).  0 indicates never timeout
                                qry.CommandType = CommandType.StoredProcedure;
                                qry.Parameters.Add("@includepreview", SqlDbType.VarChar).Value = "O";
                                conn.Open();
                                await qry.ExecuteNonQueryAsync();
                                conn.Close();
                            }
                        }
                        catch (Exception)
                        {
                            response.success = false;
                            response.msg = "Failed to install Hotfixes.";
                        }
                    }
                }

                // if hotfixess were installed successfully, then request the API/Web update
                if (response.success)
                {
                    try
                    {
                        TcpClient client = new TcpClient(updaterServer, updaterPort);
                        JsonSerializer serializer = new JsonSerializer();
                        string updaterRequestSt = JsonConvert.SerializeObject(updaterRequest);
                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(updaterRequestSt);
                        NetworkStream stream = client.GetStream();
                        stream.Write(data, 0, data.Length);

                        await h.SaveAsync();

                        // once the above command is sent to the updater service, this API service will be bounced
                        // the following "Read" command is here only to cause a delay to the page continues to show the Please Wait pop-up until the API service is bounced
                        // the page should quit waiting at that point with a "No Response" status from this API 
                        // the page will then log the user out and refresh the application

                        data = new Byte[256];
                        Int32 bytes = stream.Read(data, 0, data.Length);
                        string updaterResponseStr = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        response = JsonConvert.DeserializeObject<ApplyUpdateResponse>(updaterResponseStr);
                        stream.Close();
                        client.Close();
                    }
                    catch (ArgumentNullException e)
                    {
                        response.msg = "ArgumentNullException: " + e.ToString();
                        response.success = false;
                    }
                    catch (SocketException)
                    {
                        response.msg = "There is no RentalWorksWeb Updater Service available.";
                        response.success = false;
                    }
                }
            }

            h.ErrorMessage = response.msg;
            await h.SaveAsync();

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
