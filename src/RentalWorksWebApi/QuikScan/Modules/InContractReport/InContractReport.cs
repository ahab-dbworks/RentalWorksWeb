using FwStandard.Models;
using FwStandard.Reporting;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using RentalWorksQuikScan.Source;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    class InContractReport : MobileModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public InContractReport(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        public class JwtResponse
        {
            public int statuscode { get; set; } = 0;
            public string access_token { get; set; } = string.Empty;
            public int expires_in = 300;
        }
        //---------------------------------------------------------------------------------------------
        public class InContractReportResponse
        {
            public string htmlReportDownloadUrl { get; set; } = string.Empty;
            public string pdfReportDownloadUrl { get; set; } = string.Empty;
        }
        //---------------------------------------------------------------------------------------------
        public async Task EmailPdfAsync(string usersid, string webusersid, string contractid, string from, string to, string cc, string subject, string body)
        {
            if (from.Length == 0)
            {
                throw new ArgumentException("From email address is required.");
            }
            string webApiBaseUrl = this.ApplicationConfig.PublicBaseUrl.TrimEnd(new char[] { '/' }) + "/"; // mv 2018-06-26 
            if (string.IsNullOrEmpty(webApiBaseUrl))
            {
                throw new Exception("Unable to send email. WebApi url has not been configured.");
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(webApiBaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string username = string.Empty;
            string password = string.Empty;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select top 1 username = userloginname, password = dbo.decrypt(userpassword)");
                    qry.Add("from webusersview with (nolock)");
                    qry.Add("where webusersid = @webusersid");
                    qry.AddParameter("@webusersid", webusersid);
                    await qry.ExecuteAsync();
                    username = qry.GetField("username").ToString().TrimEnd();
                    password = qry.GetField("password").ToString().TrimEnd();
                } 
            }
            HttpRequestMessage requestJwtToken = new HttpRequestMessage(HttpMethod.Post, "api/v1/jwt");
            requestJwtToken.Content = new StringContent("{ \"UserName\": \"" + username + "\", \"Password\": \"" + password + "\"}", 
                                    Encoding.UTF8, 
                                    "application/json");

            var apiJwtResponse = client.SendAsync(requestJwtToken).Result;

            if (apiJwtResponse.IsSuccessStatusCode)
            {
                var jsonJwtResponse = apiJwtResponse.Content.ReadAsStringAsync().Result;
                var jwtResponse = JsonConvert.DeserializeObject<JwtResponse>(jsonJwtResponse);

                HttpRequestMessage requestContractReport = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/InContractReport/render");
                requestContractReport.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.access_token);
                FwReportRenderRequest renderRequest = new FwReportRenderRequest();
                renderRequest.downloadPdfAsAttachment = false;
                renderRequest.parameters = new System.Collections.Generic.Dictionary<string, object>();
                renderRequest.parameters["ContractId"] = contractid;
                renderRequest.renderMode = "Email";
                renderRequest.email = new FwReportEmailInfo();
                renderRequest.email.from = from;
                renderRequest.email.to = to;
                renderRequest.email.cc = cc;
                renderRequest.email.subject = subject;
                renderRequest.email.body = body;
                requestContractReport.Content = new StringContent(JsonConvert.SerializeObject(renderRequest), Encoding.UTF8, "application/json");
                var apiHtmlReportResponse = client.SendAsync(requestContractReport).Result;
                if (apiHtmlReportResponse.IsSuccessStatusCode)
                {
                    var jsonContractReportResponse = apiHtmlReportResponse.Content.ReadAsStringAsync().Result;
                    var contractReportResponse = JsonConvert.DeserializeObject<InContractReportResponse>(jsonContractReportResponse);
                }
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
