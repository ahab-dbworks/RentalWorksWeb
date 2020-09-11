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
        //-------------------------------------------------------------------------------------------------------
        private static List<AvailableVersion> GetAvailableVersionsFromDirectory(string currentMajor, string currentMinor, string currentRelease, bool onlyIncludeNewerVersions, string subDirectoryName = "")  // Beta, QA
        {
            List<AvailableVersion> versions = new List<AvailableVersion>();

            string systemName = "RentalWorksWeb";
            string currentMajorMinorRelease = currentMajor + "." + currentMinor + "." + currentRelease;
            string ftpDirectory = "ftp://ftp.dbworks.com/" + systemName + "/" + currentMajorMinorRelease;

            if (!string.IsNullOrEmpty(subDirectoryName))  // Beta or QA
            {
                ftpDirectory += "/" + subDirectoryName;
            }
            string currentDirectory = ftpDirectory.Substring(ftpDirectory.LastIndexOf('/') + 1).ToLower();

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
                if ((fileName.EndsWith("zip")) && (fileName.Contains("/" + systemName.ToLower() + "_")))
                {
                    string version = fileName;
                    //version = version.Replace(currentMajorMinorRelease + "/" + systemName.ToLower() + "_", "");
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
                        //string fullFtpFileName = ftpDirectory + fileName.Replace(currentMajorMinorRelease, "");
                        string fullFtpFileName = ftpDirectory + fileName.Replace(currentDirectory, "");
                        FtpWebRequest ftpRequest2 = (FtpWebRequest)WebRequest.Create(fullFtpFileName);
                        ftpRequest2.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                        ftpRequest2.Credentials = new NetworkCredential("update", "update");
                        FtpWebResponse ftpResponse2 = (FtpWebResponse)ftpRequest2.GetResponse();
                        DateTime lastModifiedDateTime = ftpResponse2.LastModified.AddHours(-7);  // probably not right
                        ftpResponse2.Close();

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
                        //string systemName = "RentalWorksWeb";
                        //string currentMajorMinorRelease = versionPieces[0] + "." + versionPieces[1] + "." + versionPieces[2];
                        //string ftpDirectory = "ftp://ftp.dbworks.com/" + systemName + "/" + currentMajorMinorRelease;
                        //FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpDirectory);
                        //ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                        //ftpRequest.Credentials = new NetworkCredential("update", "update");
                        //FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                        //Stream responseStream = ftpResponse.GetResponseStream();
                        //StreamReader reader = new StreamReader(responseStream);
                        //string names = reader.ReadToEnd();
                        //
                        //reader.Close();
                        //ftpResponse.Close();
                        //
                        //foreach (string name in names.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList())
                        //{
                        //    string fileName = name.ToLower();
                        //    if ((fileName.EndsWith("zip")) && (fileName.Contains("/rentalworksweb_")))
                        //    {
                        //        string version = fileName;
                        //        version = version.Replace(currentMajorMinorRelease + "/rentalworksweb_", "");
                        //        version = version.Replace(".zip", "");
                        //        version = version.Replace("_", ".");
                        //
                        //        //bool includeVersion = ((!request.OnlyIncludeNewerVersions) || (version.CompareTo(request.CurrentVersion) > 0));
                        //        bool includeVersion = true;
                        //
                        //        if (request.OnlyIncludeNewerVersions)
                        //        {
                        //            string[] thisVersionPieces = version.Split(".");
                        //
                        //            if (FwConvert.ToInt32(thisVersionPieces[3]) <= FwConvert.ToInt32(versionPieces[3]))
                        //            {
                        //                includeVersion = false;
                        //            }
                        //        }
                        //
                        //
                        //        if (includeVersion)
                        //        {
                        //            // get the date of the file
                        //            string fullFtpFileName = ftpDirectory + fileName.Replace(currentMajorMinorRelease, "");
                        //            FtpWebRequest ftpRequest2 = (FtpWebRequest)WebRequest.Create(fullFtpFileName);
                        //            ftpRequest2.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                        //            ftpRequest2.Credentials = new NetworkCredential("update", "update");
                        //            FtpWebResponse ftpResponse2 = (FtpWebResponse)ftpRequest2.GetResponse();
                        //            DateTime lastModifiedDateTime = ftpResponse2.LastModified.AddHours(-7);  // probably not right
                        //            ftpResponse2.Close();
                        //
                        //            AvailableVersion v = new AvailableVersion();
                        //            v.text = $"{version} &nbsp;&nbsp;&nbsp; (Released: {lastModifiedDateTime.Date.ToString("MM/dd/yyyy")})";
                        //            v.value = version;
                        //            v.Version = version;
                        //            v.VersionDate = lastModifiedDateTime.Date;
                        //
                        //            response.Versions.Add(v);
                        //        }
                        //    }
                        //
                        //}

                        response.Versions.AddRange(GetAvailableVersionsFromDirectory(versionPieces[0], versionPieces[1], versionPieces[2], request.OnlyIncludeNewerVersions));

                        SystemSettingsLogic settings = new SystemSettingsLogic();
                        settings.SetDependencies(appConfig, userSession);
                        settings.SystemSettingsId = RwConstants.CONTROL_ID;
                        if (settings.LoadAsync<SystemSettingsLogic>().Result)
                        {
                            if (settings.EnableBetaUpdates.GetValueOrDefault(false))
                            {
                                response.Versions.AddRange(GetAvailableVersionsFromDirectory(versionPieces[0], versionPieces[1], versionPieces[2], request.OnlyIncludeNewerVersions, "Beta"));
                            }

                            if (settings.EnableQaUpdates.GetValueOrDefault(false))
                            {
                                response.Versions.AddRange(GetAvailableVersionsFromDirectory(versionPieces[0], versionPieces[1], versionPieces[2], request.OnlyIncludeNewerVersions, "QA"));
                            }
                        }

                        response.Versions.Sort(new AvailableVersionDateComparer());

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
                                    // get the date of the file
                                    string fullFtpFileName = ftpDirectory + fileName.Replace(currentMajorMinorRelease + "/", "/");
                                    FtpWebRequest ftpRequest2 = (FtpWebRequest)WebRequest.Create(fullFtpFileName);
                                    ftpRequest2.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                                    ftpRequest2.Credentials = new NetworkCredential("update", "update");
                                    FtpWebResponse ftpResponse2 = (FtpWebResponse)ftpRequest2.GetResponse();
                                    DateTime lastModifiedDateTime = ftpResponse2.LastModified.AddHours(-7);  // probably not right
                                    ftpResponse2.Close();

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
                        response.msg = e.ToString();
                    }
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static DownloadBuildDocumentResponse DownloadBuildDocument(FwApplicationConfig appConfig, FwUserSession userSession, DownloadBuildDocumentRequest request)
        {
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

            string[] fromVersionPieces = fromVersion.Split(".");
            string[] toVersionPieces = toVersion.Split(".");

            if ((fromVersionPieces.Length.Equals(4)) && (toVersionPieces.Length.Equals(4)))
            {
                if ((!string.IsNullOrEmpty(fromVersionPieces[0])) && (!string.IsNullOrEmpty(fromVersionPieces[1])) && (!string.IsNullOrEmpty(fromVersionPieces[2])) && (!string.IsNullOrEmpty(fromVersionPieces[3]))
                    &&
                    (!string.IsNullOrEmpty(toVersionPieces[0])) && (!string.IsNullOrEmpty(toVersionPieces[1])) && (!string.IsNullOrEmpty(toVersionPieces[2])) && (!string.IsNullOrEmpty(toVersionPieces[3])))
                {
                    try
                    {
                        if (
                            (FwConvert.ToInt32(toVersionPieces[0]) > FwConvert.ToInt32(fromVersionPieces[0])) ||
                            ((FwConvert.ToInt32(toVersionPieces[0]).Equals(FwConvert.ToInt32(fromVersionPieces[0]))) && (FwConvert.ToInt32(toVersionPieces[1]) > FwConvert.ToInt32(fromVersionPieces[1]))) ||
                            ((FwConvert.ToInt32(toVersionPieces[0]).Equals(FwConvert.ToInt32(fromVersionPieces[0]))) && (FwConvert.ToInt32(toVersionPieces[1]).Equals(FwConvert.ToInt32(fromVersionPieces[1]))) && (FwConvert.ToInt32(toVersionPieces[2]) > FwConvert.ToInt32(fromVersionPieces[2]))) ||
                            ((FwConvert.ToInt32(toVersionPieces[0]).Equals(FwConvert.ToInt32(fromVersionPieces[0]))) && (FwConvert.ToInt32(toVersionPieces[1]).Equals(FwConvert.ToInt32(fromVersionPieces[1]))) && (FwConvert.ToInt32(toVersionPieces[2]).Equals(FwConvert.ToInt32(fromVersionPieces[2]))) && (FwConvert.ToInt32(toVersionPieces[3]) > FwConvert.ToInt32(fromVersionPieces[3])))
                           )
                        {
                            isUpgrade = true;
                        }
                    }
                    catch (Exception) { }  // bail out
                }
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
