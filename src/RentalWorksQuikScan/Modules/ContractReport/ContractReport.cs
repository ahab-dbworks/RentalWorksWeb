using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using FwStandard.Models;
using Newtonsoft.Json;
using RentalWorksQuikScan.Source;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RentalWorksQuikScan.Modules
{
    class ContractReport
    {
        //---------------------------------------------------------------------------------------------
        public class JwtResponse
        {
            public int statuscode { get; set; } = 0;
            public string access_token { get; set; } = string.Empty;
            public int expires_in = 300;
        }

        public class ContractReportResponse
        {
            public string htmlReportDownloadUrl { get; set; } = string.Empty;
            public string pdfReportDownloadUrl { get; set; } = string.Empty;
        }


        [FwJsonServiceMethod(RequiredParameters = "contractid")]
        public void GeneratePdf(dynamic request, dynamic response, dynamic session)
        {
            string usersid         = RwAppData.GetUsersId(session);
            string contractid      = request.contractid;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"http://localhost:57949/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string username = string.Empty;
            string password = string.Empty;
            using (FwSqlConnection conn = FwSqlConnection.RentalWorks)
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, FwQueryTimeouts.Default))
                {
                    qry.Add("select top 1 username = userloginname, password = dbo.decrypt(userpassword)");
                    qry.Add("from webusersview");
                    qry.Add("where webusersid = @webusersid");
                    qry.AddParameter("@webusersid", session.security.webUser.webusersid);
                    qry.Execute();
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

                HttpRequestMessage requestContractReport = new HttpRequestMessage(HttpMethod.Post, $"api/v1/ContractReport/emailpdf/{contractid}");
                requestContractReport.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.access_token);
                requestContractReport.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                var apiHtmlReportResponse = client.SendAsync(requestContractReport).Result;
                
                if (apiHtmlReportResponse.IsSuccessStatusCode)
                {
                    var jsonContractReportResponse = apiHtmlReportResponse.Content.ReadAsStringAsync().Result;
                    var contractReportResponse = JsonConvert.DeserializeObject<ContractReportResponse>(jsonContractReportResponse);
                    response.html = contractReportResponse.htmlReportDownloadUrl;
                    response.pdf = contractReportResponse.pdfReportDownloadUrl;
                }
            }
            
        }
        
    }
}
