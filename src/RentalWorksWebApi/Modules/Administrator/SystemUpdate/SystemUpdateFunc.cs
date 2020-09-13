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
using WebApi.Modules.Administrator.SystemUpdateHistoryLog;
using WebApi.Modules.Settings.SystemSettings.SystemSettings;

namespace WebApi.Modules.Administrator.SystemUpdate
{
    public class AvailableVersionsRequest
    {
        public string CurrentVersion { get; set; }
        public bool OnlyIncludeNewerVersions { get; set; }
    }

    public class BuildDocument
    {
        public string BuildNumber { get; set; }
        public DateTime? BuildDate { get; set; }
    }

    public class BuildDocumentDateComparer : Comparer<BuildDocument>
    {
        public override int Compare(BuildDocument x, BuildDocument y)
        {
            if (x.BuildDate > y.BuildDate)
            {
                return -1;
            }
            else if (x.BuildDate < y.BuildDate)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public class AvailableVersion
    {
        public string value { get; set; }
        public string text { get; set; }
        public string Version { get; set; }
        public DateTime? VersionDate { get; set; }
    }

    public class AvailableVersionDateComparer : Comparer<AvailableVersion>
    {
        public override int Compare(AvailableVersion x, AvailableVersion y)
        {
            if (x.VersionDate > y.VersionDate)
            {
                return -1;
            }
            else if (x.VersionDate < y.VersionDate)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public class AvailableVersionsResponse : TSpStatusResponse
    {
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
        public List<BuildDocument> Documents { get; set; } = new List<BuildDocument>();
    }

    public class GetVersionHotfixRequest
    {
        public string Version { get; set; }
    }

    public class GetVersionHotfixResponse : TSpStatusResponse
    {
        public string Hotfix { get; set; }
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
        const string LOOPBACK_IP = "127.0.0.1";
        public const string BETA_DIRECTORY = "Beta";
        public const string QA_DIRECTORY = "QA";
        public const string FTP_LOGIN = "update";
        public const string FTP_PASSWORD = "update";
        public const string FTP_SERVER = "ftp://ftp.dbworks.com";
        public const string SYSTEM_NAME = "RentalWorksWeb";
        //-------------------------------------------------------------------------------------------------------
        private static DateTime GetFileDateTimeFromFtp(string fullFtpFileName) 
        {
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(fullFtpFileName);
            ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            ftpRequest.Credentials = new NetworkCredential(FTP_LOGIN, FTP_PASSWORD);
            FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            DateTime fileDateTime = ftpResponse.LastModified.AddHours(-7);  // probably not right
            ftpResponse.Close();
            return fileDateTime;
        }
        //-------------------------------------------------------------------------------------------------------
        private static List<string> GetFileNamesFromFtpDirectory(string ftpDirectory)
        {
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpDirectory);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            ftpRequest.Credentials = new NetworkCredential(FTP_LOGIN, FTP_PASSWORD);
            FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            Stream responseStream = ftpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string names = reader.ReadToEnd();
            reader.Close();
            ftpResponse.Close();

            return names.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        //-------------------------------------------------------------------------------------------------------
        private static List<AvailableVersion> GetAvailableVersionsFromDirectory(string currentMajor, string currentMinor, string currentRelease, bool onlyIncludeNewerVersions, string subDirectoryName = "")  // Beta, QA
        {
            List<AvailableVersion> versions = new List<AvailableVersion>();

            string systemName = SYSTEM_NAME;
            string currentMajorMinorRelease = currentMajor + "." + currentMinor + "." + currentRelease;
            string ftpDirectory = FTP_SERVER + "/" + systemName + "/" + currentMajorMinorRelease;

            if (!string.IsNullOrEmpty(subDirectoryName))  // Beta or QA
            {
                ftpDirectory += "/" + subDirectoryName;
            }
            string currentDirectory = ftpDirectory.Substring(ftpDirectory.LastIndexOf('/') + 1).ToLower();

            foreach (string name in GetFileNamesFromFtpDirectory(ftpDirectory))
            {
                string fileName = name.ToLower();
                if ((fileName.EndsWith("zip")) && (fileName.Contains("/" + systemName.ToLower() + "_")))
                {
                    string version = fileName;
                    version = version.Replace(currentDirectory + "/" + systemName.ToLower() + "_", "");
                    version = version.Replace(".zip", "");
                    version = version.Replace("_", ".");

                    bool includeVersion = true;

                    if (onlyIncludeNewerVersions)
                    {
                        string[] thisVersionPieces = version.Split(".");

                        if (FwConvert.ToInt32(thisVersionPieces[3]) <= FwConvert.ToInt32(currentRelease))
                        {
                            includeVersion = false;
                        }
                    }


                    if (includeVersion)
                    {
                        // get the date of the file
                        string fullFtpFileName = ftpDirectory + fileName.Replace(currentDirectory, "");
                        DateTime lastModifiedDateTime = GetFileDateTimeFromFtp(fullFtpFileName);

                        AvailableVersion v = new AvailableVersion();
                        v.text = $"{version} &nbsp;&nbsp;&nbsp; (Released: {lastModifiedDateTime.Date.ToString("MM/dd/yyyy")})" + (string.IsNullOrWhiteSpace(subDirectoryName) ? "" : " [" + subDirectoryName + "]");
                        v.value = version;
                        v.Version = version;
                        v.VersionDate = lastModifiedDateTime.Date;

                        versions.Add(v);
                    }
                }

            }

            versions.Sort(new AvailableVersionDateComparer());

            return versions;
        }
        //-------------------------------------------------------------------------------------------------------
        private static string GetVersionHotfixFromDirectory(string major, string minor, string release, string build, string subDirectoryName = "")  // Beta, QA
        {
            string hotfixNumber = string.Empty;

            string systemName = SYSTEM_NAME;
            string majorMinorRelease = major + "." + minor + "." + release;
            string ftpDirectory = FTP_SERVER + "/" + systemName + "/" + majorMinorRelease;

            if (!string.IsNullOrEmpty(subDirectoryName))  // Beta or QA
            {
                ftpDirectory += "/" + subDirectoryName;
            }

            foreach (string f in GetFileNamesFromFtpDirectory(ftpDirectory))
            {
                string fileName = f.ToLower();
                if (fileName.Contains(majorMinorRelease + "." + build + "_hotfix_"))
                {
                    hotfixNumber = fileName.Substring(fileName.Length - 3);  // get the last 3 characters
                }
            }

            return hotfixNumber;
        }
        //-------------------------------------------------------------------------------------------------------
        private static string[] GetMajorMinorReleaseBuildFromVersion(string version)
        {

            if (string.IsNullOrEmpty(version))
            {
                throw new Exception("Supply a value for CurrentVersion in the request.");
            }
            else if (version.Split(".").Length != 4)
            {
                throw new Exception("Invalid format for CurrentVersion (" + version + ").  Format must be Major.Minor.Release.Build");
            }
            else
            {
                string[] versionPieces = version.Split(".");

                if ((string.IsNullOrEmpty(versionPieces[0])) || (string.IsNullOrEmpty(versionPieces[1])) || (string.IsNullOrEmpty(versionPieces[2])))
                {
                    throw new Exception("Invalid format for CurrentVersion (" + version + ").  Cannot determine Major, Minor, and Release.");
                }
                else
                {
                    return versionPieces;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public static AvailableVersionsResponse GetAvailableVersions(FwApplicationConfig appConfig, FwUserSession userSession, AvailableVersionsRequest request)
        {
            AvailableVersionsResponse response = new AvailableVersionsResponse();

            try
            {
                string[] majorMinorReleaseBuild = GetMajorMinorReleaseBuildFromVersion(request.CurrentVersion);

                response.Versions.AddRange(GetAvailableVersionsFromDirectory(majorMinorReleaseBuild[0], majorMinorReleaseBuild[1], majorMinorReleaseBuild[2], request.OnlyIncludeNewerVersions));

                SystemSettingsLogic settings = new SystemSettingsLogic();
                settings.SetDependencies(appConfig, userSession);
                settings.SystemSettingsId = RwConstants.CONTROL_ID;
                if (settings.LoadAsync<SystemSettingsLogic>().Result)
                {
                    if (settings.EnableBetaUpdates.GetValueOrDefault(false))
                    {
                        response.Versions.AddRange(GetAvailableVersionsFromDirectory(majorMinorReleaseBuild[0], majorMinorReleaseBuild[1], majorMinorReleaseBuild[2], request.OnlyIncludeNewerVersions, BETA_DIRECTORY));
                    }

                    if (settings.EnableQaUpdates.GetValueOrDefault(false))
                    {
                        response.Versions.AddRange(GetAvailableVersionsFromDirectory(majorMinorReleaseBuild[0], majorMinorReleaseBuild[1], majorMinorReleaseBuild[2], request.OnlyIncludeNewerVersions, QA_DIRECTORY));
                    }
                }

                response.Versions.Sort(new AvailableVersionDateComparer());

                response.success = true;

            }
            catch (Exception e)
            {
                response.success = false;
                response.msg = e.Message;
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static BuildDocumentsResponse GetBuildDocuments(FwApplicationConfig appConfig, FwUserSession userSession, BuildDocumentsRequest request)
        {
            BuildDocumentsResponse response = new BuildDocumentsResponse();

            try
            {
                string[] majorMinorReleaseBuild = GetMajorMinorReleaseBuildFromVersion(request.CurrentVersion);

                string systemName = SYSTEM_NAME;
                string currentMajorMinorRelease = majorMinorReleaseBuild[0] + "." + majorMinorReleaseBuild[1] + "." + majorMinorReleaseBuild[2];
                string ftpDirectory = FTP_SERVER + "/" + systemName + "/" + currentMajorMinorRelease;

                foreach (string name in GetFileNamesFromFtpDirectory(ftpDirectory))
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
                            // get the date of the file
                            string fullFtpFileName = ftpDirectory + fileName.Replace(currentMajorMinorRelease + "/", "/");
                            DateTime lastModifiedDateTime = GetFileDateTimeFromFtp(fullFtpFileName);

                            BuildDocument bd = new BuildDocument();
                            bd.BuildNumber = version;
                            bd.BuildDate = lastModifiedDateTime.Date;
                            response.Documents.Add(bd);
                            response.DocumentsList.Add(version);
                        }
                    }
                }

                response.Documents.Sort(new BuildDocumentDateComparer());

                response.success = true;

            }
            catch (Exception e)
            {
                response.success = false;
                response.msg = e.Message;
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static DownloadBuildDocumentResponse DownloadBuildDocument(FwApplicationConfig appConfig, FwUserSession userSession, DownloadBuildDocumentRequest request)
        {
            DownloadBuildDocumentResponse response = new DownloadBuildDocumentResponse();

            try
            {
                string[] majorMinorReleaseBuild = GetMajorMinorReleaseBuildFromVersion(request.Version);

                string systemName = SYSTEM_NAME;
                string majorMinorRelease = majorMinorReleaseBuild[0] + "." + majorMinorReleaseBuild[1] + "." + majorMinorReleaseBuild[2];

                string remotePdfFileName = request.Version + ".pdf";
                string remotePath = FTP_SERVER + "/" + systemName + "/" + majorMinorRelease + "/v" + remotePdfFileName;

                string localPdfFile = request.Version.Replace('.', '_') + "_pdf";
                string localPath = "wwwroot/temp/downloads/" + localPdfFile;

                WebClient client = new WebClient();
                client.Credentials = new NetworkCredential(FTP_LOGIN, FTP_PASSWORD);
                client.DownloadFile(remotePath, localPath);

                response.downloadUrl = $"api/v1/download/{localPdfFile}?downloadasfilename={request.Version}.pdf";
                response.success = true;
            }
            catch (Exception e)
            {
                response.success = false;
                response.msg = e.Message;
            }


            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<bool> LogUpdateMessage(SystemUpdateHistoryLogic history, string message)
        {
            bool success = false;
            try
            {
                SystemUpdateHistoryLogLogic l = new SystemUpdateHistoryLogLogic();
                l.SetDependencies(history.AppConfig, history.UserSession);
                l.SystemUpdateHistoryId = history.SystemUpdateHistoryId;
                l.Messsage = message;
                await l.SaveAsync();
                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        private static bool IsUpgrade(string fromVersion, string toVersion)
        {
            bool isUpgrade = false;

            try
            {
                string[] fromMajorMinorReleaseBuild = GetMajorMinorReleaseBuildFromVersion(fromVersion);
                string[] toMajorMinorReleaseBuild = GetMajorMinorReleaseBuildFromVersion(toVersion);

                if ((!string.IsNullOrEmpty(fromMajorMinorReleaseBuild[0])) && (!string.IsNullOrEmpty(fromMajorMinorReleaseBuild[1])) && (!string.IsNullOrEmpty(fromMajorMinorReleaseBuild[2])) && (!string.IsNullOrEmpty(fromMajorMinorReleaseBuild[3]))
                       &&
                    (!string.IsNullOrEmpty(toMajorMinorReleaseBuild[0])) && (!string.IsNullOrEmpty(toMajorMinorReleaseBuild[1])) && (!string.IsNullOrEmpty(toMajorMinorReleaseBuild[2])) && (!string.IsNullOrEmpty(toMajorMinorReleaseBuild[3])))
                {
                    if (
                        (FwConvert.ToInt32(toMajorMinorReleaseBuild[0]) > FwConvert.ToInt32(fromMajorMinorReleaseBuild[0])) ||
                        ((FwConvert.ToInt32(toMajorMinorReleaseBuild[0]).Equals(FwConvert.ToInt32(fromMajorMinorReleaseBuild[0]))) && (FwConvert.ToInt32(toMajorMinorReleaseBuild[1]) > FwConvert.ToInt32(fromMajorMinorReleaseBuild[1]))) ||
                        ((FwConvert.ToInt32(toMajorMinorReleaseBuild[0]).Equals(FwConvert.ToInt32(fromMajorMinorReleaseBuild[0]))) && (FwConvert.ToInt32(toMajorMinorReleaseBuild[1]).Equals(FwConvert.ToInt32(fromMajorMinorReleaseBuild[1]))) && (FwConvert.ToInt32(toMajorMinorReleaseBuild[2]) > FwConvert.ToInt32(fromMajorMinorReleaseBuild[2]))) ||
                        ((FwConvert.ToInt32(toMajorMinorReleaseBuild[0]).Equals(FwConvert.ToInt32(fromMajorMinorReleaseBuild[0]))) && (FwConvert.ToInt32(toMajorMinorReleaseBuild[1]).Equals(FwConvert.ToInt32(fromMajorMinorReleaseBuild[1]))) && (FwConvert.ToInt32(toMajorMinorReleaseBuild[2]).Equals(FwConvert.ToInt32(fromMajorMinorReleaseBuild[2]))) && (FwConvert.ToInt32(toMajorMinorReleaseBuild[3]) > FwConvert.ToInt32(fromMajorMinorReleaseBuild[3])))
                       )
                    {
                        isUpgrade = true;
                    }
                }
            }
            catch (Exception e)
            {
                isUpgrade = false; // bail out
            }

            return isUpgrade;
        }
        //-------------------------------------------------------------------------------------------------------
        private static string GetMyLocalIPAddress()  // returns string.Empty if non determined
        {
            string localIp = string.Empty;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp = ip.ToString();
                }
            }
            //if (localIp.Equals(string.Empty))
            //{
            //    throw new Exception("Cannot determine local IP address.");
            //}
            return localIp;
        }
        //---------------------------------------------------------------------------------------------------
        private static async Task<bool> ConnectToUpdater(SystemUpdateHistoryLogic h, string server, int port)
        {
            bool success = false;
            await LogUpdateMessage(h, "about to send test command to updater service to validate communication");
            try
            {
                await LogUpdateMessage(h, "about to create client and connect to " + server + ":" + port.ToString());
                TcpClient client = new TcpClient(server, port);
                await LogUpdateMessage(h, "client created successfully");

                await LogUpdateMessage(h, "about to create json serializer");
                JsonSerializer serializer = new JsonSerializer();
                await LogUpdateMessage(h, "json serializer created successfully");

                //string testRequestStr = "TESTCHECK";
                string testRequestStr = "TESTCHECK\r\n";
                await LogUpdateMessage(h, "about to encode test message as bytes");
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(testRequestStr);
                await LogUpdateMessage(h, "encoded test message as bytes successfully");

                await LogUpdateMessage(h, "about to access network stream through client");
                NetworkStream stream = client.GetStream();
                await LogUpdateMessage(h, "accessed network stream through client successfully");

                await LogUpdateMessage(h, "about to set write and read timeouts");
                stream.WriteTimeout = 10000;
                stream.ReadTimeout = 10000;
                await LogUpdateMessage(h, "set write and read timeouts successfully");

                await LogUpdateMessage(h, "about to write to stream");
                stream.Write(data, 0, data.Length);
                await LogUpdateMessage(h, "wrote to stream succesfully");

                await LogUpdateMessage(h, "about to instantiate bytes array");
                data = new Byte[256];
                await LogUpdateMessage(h, "instantiated bytes array succesfully");

                await LogUpdateMessage(h, "about to read from the network stream");
                Int32 bytes = stream.Read(data, 0, data.Length);
                await LogUpdateMessage(h, "read from the network stream succesfully");

                await LogUpdateMessage(h, "about to decode response bytes into a string");
                string testResponseStr = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                await LogUpdateMessage(h, "decoded response bytes into a string succesfully");

                await LogUpdateMessage(h, "about to close network stream");
                stream.Close();
                await LogUpdateMessage(h, "closed network stream successfully");

                await LogUpdateMessage(h, "about to close client");
                client.Close();
                await LogUpdateMessage(h, "closed client successfully");

                await LogUpdateMessage(h, "successfully sent test command to updater service. Received response: " + testResponseStr);
                success = true;
            }
            catch (Exception e)
            {
                //response.msg = "There is no RentalWorksWeb Updater Service available.";
                success = false;
                await LogUpdateMessage(h, "ERROR: Could not connect to Updater Service at " + server + ":" + port.ToString());
                await LogUpdateMessage(h, "ERROR: " + e.Message);
            }
            return success;
        }
        //---------------------------------------------------------------------------------------------------
        public static GetVersionHotfixResponse GetVersionHotfix(FwApplicationConfig appConfig, FwUserSession userSession, GetVersionHotfixRequest request)
        {
            GetVersionHotfixResponse response = new GetVersionHotfixResponse();

            try
            {
                string[] majorMinorReleaseBuild = GetMajorMinorReleaseBuildFromVersion(request.Version);

                string hotfix = GetVersionHotfixFromDirectory(majorMinorReleaseBuild[0], majorMinorReleaseBuild[1], majorMinorReleaseBuild[2], majorMinorReleaseBuild[3]);

                SystemSettingsLogic settings = new SystemSettingsLogic();
                settings.SetDependencies(appConfig, userSession);
                settings.SystemSettingsId = RwConstants.CONTROL_ID;
                if (settings.LoadAsync<SystemSettingsLogic>().Result)
                {
                    if (string.IsNullOrEmpty(hotfix))
                    {
                        if (settings.EnableBetaUpdates.GetValueOrDefault(false))
                        {
                            hotfix = GetVersionHotfixFromDirectory(majorMinorReleaseBuild[0], majorMinorReleaseBuild[1], majorMinorReleaseBuild[2], majorMinorReleaseBuild[3], BETA_DIRECTORY);
                        }
                    }

                    if (string.IsNullOrEmpty(hotfix))
                    {
                        if (settings.EnableQaUpdates.GetValueOrDefault(false))
                        {
                            hotfix = GetVersionHotfixFromDirectory(majorMinorReleaseBuild[0], majorMinorReleaseBuild[1], majorMinorReleaseBuild[2], majorMinorReleaseBuild[3], QA_DIRECTORY);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(hotfix))
                {
                    response.success = true;
                    response.Hotfix = hotfix;
                }

            }
            catch (Exception e)
            {
                response.success = false;
                response.msg = e.Message;
            }


            return response;
        }
        //---------------------------------------------------------------------------------------------------
        public static async Task<ApplyUpdateResponse> ApplyUpdate(FwApplicationConfig appConfig, FwUserSession userSession, ApplyUpdateRequest request)
        {
            ApplyUpdateResponse response = new ApplyUpdateResponse();

            SystemUpdateHistoryLogic h = new SystemUpdateHistoryLogic();
            h.SetDependencies(appConfig, userSession);
            h.UsersId = userSession.UsersId;
            h.FromVersion = request.CurrentVersion;
            h.ToVersion = request.ToVersion;
            h.UpdateDateTime = DateTime.Now;
            await h.SaveAsync();

            await LogUpdateMessage(h, "Begin");

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(appConfig.DatabaseSettings.ConnectionString);

            string apiApplicationPool = string.Empty;
            string apiPath = string.Empty;
            string webApplicationPool = string.Empty;
            string webPath = string.Empty;

            if (string.IsNullOrEmpty(appConfig.ApplicationPool))
            {
                //new way
                apiApplicationPool = appConfig.ApiApplicationPool;
                apiPath = appConfig.ApiPath;
                webApplicationPool = appConfig.WebApplicationPool;
                webPath = appConfig.WebPath;
            }
            else
            {
                //old way
                apiApplicationPool = appConfig.ApplicationPool;
                apiPath = GetCurrentApiApplicationPath();
                webApplicationPool = GetCurrentWebApplicationPoolName(apiApplicationPool);
                webPath = GetCurrentWebApplicationPath();
            }


            await LogUpdateMessage(h, "updating from version: " + request.CurrentVersion);
            await LogUpdateMessage(h, "updating to version: " + request.ToVersion);
            await LogUpdateMessage(h, "sql server name: " + connectionStringBuilder.DataSource);
            await LogUpdateMessage(h, "database name: " + connectionStringBuilder.InitialCatalog);
            await LogUpdateMessage(h, "api application pool: " + apiApplicationPool);
            await LogUpdateMessage(h, "api installation path: " + apiPath);
            await LogUpdateMessage(h, "web application pool: " + webApplicationPool);
            await LogUpdateMessage(h, "web installation path: " + webPath);

            UpdaterRequest updaterRequest = new UpdaterRequest();
            updaterRequest.ToVersion = request.ToVersion;
            updaterRequest.SQLServerName = connectionStringBuilder.DataSource;
            updaterRequest.DatabaseName = connectionStringBuilder.InitialCatalog;
            updaterRequest.ApiApplicationPool = apiApplicationPool;
            updaterRequest.ApiInstallPath = apiPath;
            updaterRequest.WebApplicationPool = webApplicationPool;
            updaterRequest.WebInstallPath = webPath;

            string hotfixInstallerConnectionString = "Server=" + connectionStringBuilder.DataSource + ";Database=" + connectionStringBuilder.InitialCatalog + ";User Id=dbworks;Password=db2424;";  // user/pass hard-coded for now

            await LogUpdateMessage(h, "about to determine local IP address");
            string localIp = GetMyLocalIPAddress();
            await LogUpdateMessage(h, "local IP address is " + localIp);
            if (localIp.Equals(string.Empty))
            {
                await LogUpdateMessage(h, "could not determine local IP address. Using loopback instead.");
                localIp = LOOPBACK_IP;
                await LogUpdateMessage(h, "local IP address is " + localIp);
            }

            string updaterServer = localIp;
            int updaterPort = 18811;

            //bool doInstallHotfixes = (request.ToVersion.CompareTo(request.CurrentVersion) >= 0);  // only apply hotfixes if upgrading or refreshing current version, not downgrading
            bool doInstallHotfixes = IsUpgrade(request.CurrentVersion, request.ToVersion);  // only apply hotfixes if upgrading or refreshing current version, not downgrading

            if (string.IsNullOrEmpty(updaterRequest.ApiApplicationPool))
            {
                response.msg = "Could not determine API Application Pool from appsettings.json.";
                await LogUpdateMessage(h, "ERROR: " + response.msg);
            }
            else if (string.IsNullOrEmpty(updaterRequest.ApiInstallPath))
            {
                response.msg = "Could not determine API Installation path.";
                await LogUpdateMessage(h, "ERROR: " + response.msg);
            }
            else if (string.IsNullOrEmpty(updaterRequest.WebApplicationPool))
            {
                response.msg = "Could not determine Web Application Pool.";
                await LogUpdateMessage(h, "ERROR: " + response.msg);
            }
            else if (string.IsNullOrEmpty(updaterRequest.WebInstallPath))
            {
                response.msg = "Could not determine Web Installation path.";
                await LogUpdateMessage(h, "ERROR: " + response.msg);
            }
            else if (string.IsNullOrEmpty(updaterRequest.SQLServerName))
            {
                response.msg = "Could not determine SQL Server name from appsettings.json.";
                await LogUpdateMessage(h, "ERROR: " + response.msg);
            }
            else if (string.IsNullOrEmpty(updaterRequest.DatabaseName))
            {
                response.msg = "Could not determine Database name from appsettings.json.";
                await LogUpdateMessage(h, "ERROR: " + response.msg);
            }
            else if (string.IsNullOrEmpty(request.CurrentVersion))
            {
                response.msg = "Current Version cannot be blank.";
                await LogUpdateMessage(h, "ERROR: " + response.msg);
            }
            else if (string.IsNullOrEmpty(request.ToVersion))
            {
                response.msg = "Update To Version cannot be blank.";
                await LogUpdateMessage(h, "ERROR: " + response.msg);
            }
            else
            {
                response.success = true;

                // attempt to connect to the database with dbworks account to confirm credentials
                if (response.success)
                {
                    if (doInstallHotfixes)
                    {
                        await LogUpdateMessage(h, "about to check database connectivity before installing hotfixes");
                        try
                        {
                            // using native SqlConnection object here to bypass block on the "dbworks" login
                            using (SqlConnection conn = new SqlConnection(hotfixInstallerConnectionString))
                            {
                                conn.Open();
                                conn.Close();
                                await LogUpdateMessage(h, "successfully connected to database before installing hotfixes");
                            }
                        }
                        catch (Exception e)
                        {
                            response.success = false;
                            response.msg = "Cannot connect to the database to install hotfixes.";
                            await LogUpdateMessage(h, "ERROR: " + response.msg);
                            await LogUpdateMessage(h, "ERROR: " + e.Message);
                        }
                    }
                }


                // attempt to connect to the updater service to confirm send/receive connectivity
                // if using a specific IP address other than the loopback, and a failure occurs, then a second attempt will be made with the loopback IP before bailing out
                if (response.success)
                {
                    bool connectionEstablished = false;
                    if (!connectionEstablished)
                    {
                        connectionEstablished = await ConnectToUpdater(h, updaterServer, updaterPort);
                    }
                    if (!connectionEstablished)
                    {
                        if (!updaterServer.Equals(LOOPBACK_IP))
                        {
                            updaterServer = LOOPBACK_IP;
                            connectionEstablished = await ConnectToUpdater(h, updaterServer, updaterPort);
                        }
                    }
                    if (!connectionEstablished)
                    {
                        response.msg = "There is no RentalWorksWeb Updater Service available.";
                        response.success = false;
                    }
                }


                // if applying hotfixes, attempt to determine the minimum hotfix number
                string throughHotfixNumber = "";
                if (response.success)
                {
                    if (doInstallHotfixes)
                    {
                        await LogUpdateMessage(h, "about to check ftp for hotfix number");
                        try
                        {
                            GetVersionHotfixRequest hotfixRequest = new GetVersionHotfixRequest();
                            hotfixRequest.Version = request.ToVersion;
                            GetVersionHotfixResponse hotfixResponse = GetVersionHotfix(appConfig, userSession, hotfixRequest);
                            if ((hotfixResponse.success) && (!string.IsNullOrEmpty(hotfixResponse.Hotfix)))
                            {
                                throughHotfixNumber = hotfixResponse.Hotfix;
                                await LogUpdateMessage(h, "hotfix number is " + throughHotfixNumber);
                            }
                            else
                            {
                                response.msg = "Cannot determine minimum Hotfix for version " + request.ToVersion;
                                response.success = false;
                                await LogUpdateMessage(h, "ERROR: " + response.msg);
                            }
                        }
                        catch (Exception e)
                        {
                            response.msg = e.Message;
                            response.success = false;
                            await LogUpdateMessage(h, "ERROR: " + response.msg);
                            await LogUpdateMessage(h, "ERROR: " + e.Message);
                        }
                    }
                }

                // apply all hotfixes here
                if (response.success)
                {
                    if (doInstallHotfixes)
                    {
                        await LogUpdateMessage(h, "about to install hotfixes");
                        try
                        {
                            // using native SqlConnection object here to bypass block on the "dbworks" login
                            using (SqlConnection hotfixConn = new SqlConnection(hotfixInstallerConnectionString))
                            {
                                SqlCommand qry = new SqlCommand("fw_installhotfixes", hotfixConn);
                                qry.CommandTimeout = 1800;  // time-out at 1,800 seconds (30 minutes)  // default is 30 (30 seconds).  0 indicates never timeout
                                qry.CommandType = CommandType.StoredProcedure;
                                qry.Parameters.Add("@includepreview", SqlDbType.VarChar).Value = "O";
                                qry.Parameters.Add("@throughhotfixnumber", SqlDbType.VarChar).Value = throughHotfixNumber;
                                hotfixConn.Open();
                                await qry.ExecuteNonQueryAsync();
                                hotfixConn.Close();
                                await LogUpdateMessage(h, "successfully installed hotfixes");
                            }
                        }
                        catch (Exception e)
                        {
                            response.success = false;
                            response.msg = "Failed to install Hotfixes.";
                            await LogUpdateMessage(h, "ERROR: " + response.msg);
                            await LogUpdateMessage(h, "ERROR: " + e.Message);
                        }
                    }
                }

                // if hotfixess were installed successfully, then request the API/Web update
                if (response.success)
                {
                    await LogUpdateMessage(h, "about to send command to updater service to apply update");
                    try
                    {
                        TcpClient client = new TcpClient(updaterServer, updaterPort);
                        JsonSerializer serializer = new JsonSerializer();
                        //string updaterRequestSt = JsonConvert.SerializeObject(updaterRequest);
                        string updaterRequestSt = JsonConvert.SerializeObject(updaterRequest) + "\r\n";

                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(updaterRequestSt);
                        NetworkStream stream = client.GetStream();
                        await LogUpdateMessage(h, "about to send command: " + updaterRequestSt);
                        stream.Write(data, 0, data.Length);

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
                        await LogUpdateMessage(h, "ERROR: " + response.msg);
                        await LogUpdateMessage(h, "ERROR: " + e.Message);
                    }
                    catch (SocketException e)
                    {
                        response.msg = "There is no RentalWorksWeb Updater Service available.";
                        response.success = false;
                        await LogUpdateMessage(h, "ERROR: " + response.msg);
                        await LogUpdateMessage(h, "ERROR: " + e.Message);
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
