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
    class OutContractReport
    {
        //---------------------------------------------------------------------------------------------
        public class JwtResponse
        {
            public int statuscode { get; set; } = 0;
            public string access_token { get; set; } = string.Empty;
            public int expires_in = 300;
        }

        public class OutContractReportResponse
        {
            public string htmlReportDownloadUrl { get; set; } = string.Empty;
            public string pdfReportDownloadUrl { get; set; } = string.Empty;
        }

        [FwJsonServiceMethod(RequiredParameters = "contractid,from,to,subject,body")]
        public void EmailPdf(dynamic request, dynamic response, dynamic session)
        {
            string usersid = RwAppData.GetUsersId(session);
            string contractid = request.contractid;
            string webApiBaseUrl = Fw.Json.ValueTypes.FwApplicationConfig.CurrentSite.WebApi.Url.TrimEnd(new char[] { '/' }) + "/"; // mv 2018-06-26 

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(webApiBaseUrl);
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

                HttpRequestMessage requestContractReport = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/OutContractReport/emailpdf/{contractid}");
                requestContractReport.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.access_token);
                EmailPdfRequest emailPdfRequest = new EmailPdfRequest();
                emailPdfRequest.from = request.from;
                emailPdfRequest.to = request.to;
                emailPdfRequest.subject = request.subject;
                emailPdfRequest.body = request.body;
                string strEmailPdfRequest = JsonConvert.SerializeObject(emailPdfRequest);
                requestContractReport.Content = new StringContent(strEmailPdfRequest, Encoding.UTF8, "application/json");
                var apiHtmlReportResponse = client.SendAsync(requestContractReport).Result;
                
                if (apiHtmlReportResponse.IsSuccessStatusCode)
                {
                    var jsonContractReportResponse = apiHtmlReportResponse.Content.ReadAsStringAsync().Result;
                    var contractReportResponse = JsonConvert.DeserializeObject<OutContractReportResponse>(jsonContractReportResponse);
                    response.html = contractReportResponse.htmlReportDownloadUrl;
                    response.pdf = contractReportResponse.pdfReportDownloadUrl;
                }
            }
            
        }
        
    }
}
