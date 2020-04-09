using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Threading.Tasks;
using WebApi.Logic;
using Newtonsoft.Json;
using Microsoft.Web.Administration;

namespace WebApi.Modules.Utilities.Update
{

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
        public static async Task<ApplyUpdateResponse> ApplyUpdate(FwApplicationConfig appConfig, FwUserSession userSession, ApplyUpdateRequest request)
        {
            ApplyUpdateResponse response = new ApplyUpdateResponse();

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(appConfig.DatabaseSettings.ConnectionString);

            UpdaterRequest uRequest = new UpdaterRequest();
            uRequest.ToVersion = request.ToVersion;
            uRequest.SQLServerName = connectionStringBuilder.DataSource;
            uRequest.DatabaseName = connectionStringBuilder.InitialCatalog;
            uRequest.ApiApplicationPool = GetCurrentApiApplicationPoolName();
            uRequest.ApiInstallPath = GetCurrentApiApplicationPath();
            uRequest.WebApplicationPool = GetCurrentWebApplicationPoolName();
            uRequest.WebInstallPath = GetCurrentWebApplicationPath();

            ////temporary
            //uRequest.ApiApplicationPool = "rentalworkswebapi";
            //uRequest.ApiInstallPath = @"c:\inetpub\wwwroot\rentalworkswebapi";
            //uRequest.WebApplicationPool = "rentalworksweb";
            //uRequest.WebInstallPath = @"c:\inetpub\wwwroot\rentalworksweb";


            if (string.IsNullOrEmpty(uRequest.ApiApplicationPool))
            {
                response.msg = "Could not determine API Application Pool.";
            }
            else if (string.IsNullOrEmpty(uRequest.ApiInstallPath))
            {
                response.msg = "Could not determine API Installation path.";
            }
            else if (string.IsNullOrEmpty(uRequest.WebApplicationPool))
            {
                response.msg = "Could not determine Web Application Pool.";
            }
            else if (string.IsNullOrEmpty(uRequest.WebInstallPath))
            {
                response.msg = "Could not determine Web Installation path.";
            }
            else if (string.IsNullOrEmpty(uRequest.SQLServerName))
            {
                response.msg = "Could not determine SQL Server name.";
            }
            else if (string.IsNullOrEmpty(uRequest.DatabaseName))
            {
                response.msg = "Could not determine Database name.";
            }
            else
            {
                try
                {
                    string server = "127.0.0.1";
                    int port = 8081;

                    TcpClient client = new TcpClient(server, port);
                    JsonSerializer serializer = new JsonSerializer();
                    string message = JsonConvert.SerializeObject(uRequest);

                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);


                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);
                    response.success = true;

                    // once this command is sent to the updater service, this API service will be bounced
                    // the following "Read" command is here temporarily to cause a delay

                    data = new Byte[256];
                    string responseData = string.Empty;

                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                    Console.WriteLine("Response received: {0}", responseData);

                    response = JsonConvert.DeserializeObject<ApplyUpdateResponse>(responseData);

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
                webAppPath = apiAppPath.Substring(0, apiAppPath.Length - 3 - 1);
            }
            return webAppPath;
        }
        //-------------------------------------------------------------------------------------------------------
        private static string GetCurrentApiApplicationPoolName()
        {
            ServerManager manager = new ServerManager();
            string DefaultSiteName = "Default Web Site";// System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName();
            Site defaultSite = manager.Sites[DefaultSiteName];
            string appVirtualPath = GetCurrentApiApplicationPath();// HttpRuntime.AppDomainAppVirtualPath;

            string appPoolName = string.Empty;
            foreach (Application app in defaultSite.Applications)
            {
                string appPath = app.Path;
                if (appPath == appVirtualPath)
                {
                    appPoolName = app.ApplicationPoolName;
                }
            }

            return appPoolName;
        }
        //-------------------------------------------------------------------------------------------------------
        private static string GetCurrentWebApplicationPoolName()
        {
            string apiAppPoolName = GetCurrentApiApplicationPoolName();  // "rentalworkswebapi"
            string webAppPoolName = "";
            if (apiAppPoolName.ToLower().EndsWith("api"))
            {
                webAppPoolName = apiAppPoolName.Substring(0, apiAppPoolName.Length - 3 - 1);
            }
            return webAppPoolName;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
