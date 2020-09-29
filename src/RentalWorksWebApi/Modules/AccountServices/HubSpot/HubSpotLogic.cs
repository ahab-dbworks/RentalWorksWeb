using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Logic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WebApi.Modules.AccountServices.HubSpot
{
    public class HubSpotLogic : AppBusinessLogic
    {
        public async Task<HubSpotTokensResponse> GetTokensAsync([FromBody]GetHubSpotTokensRequest request)
        {
            var client = new HttpClient();
            HubSpotTokensFormData body = new HubSpotTokensFormData();

            var url = "https://api.hubapi.com/oauth/v1/token";
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            body.properties = new Dictionary<string, string>();
            body.properties.Add("client_id", "7fd2d81a-ae52-4a60-af93-caa4d1fd5848");
            body.properties.Add("client_secret", "3fa85657-d67b-4965-bd47-929ba0604dc6");
            body.properties.Add("redirect_uri", "http://localhost:57949/webdev");
            body.properties.Add("grant_type", "authorization_code");
            body.properties.Add("code", request.authorizationCode);

            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(body.properties) };
            req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            //var jsonBody = JsonSerializer.Serialize(body);
            //var formContent = new FormUrlEncodedContent(body.properties);

            var response = await client.SendAsync(req);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonTokens = JsonConvert.DeserializeObject<HubSpotTokensResponse>(responseBody);
            //write tokens to dbo.control

            return jsonTokens;

        }
        //---------------------------------------------------------------------------------------------
        public async Task<string> GetContactsAsync([FromBody]GetHubSpotContactsRequest request)
        {
            HttpResponseMessage response;
            var client = new HttpClient();
            string accessToken = request.accessToken;

            client.BaseAddress = new Uri("https://api.hubapi.com/crm/v3/objects/contacts");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");


            var url = "/crm/v3/objects/contacts";
            response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();


            return responseBody;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<string> PostContactAsync([FromBody]PostHubSpotContactRequest request)
        {
            HubSpotContactPostRequest body = new HubSpotContactPostRequest();
            //HubSpotContactProperties properties = new HubSpotContactProperties();
            HttpResponseMessage response;
            var client = new HttpClient();
            string accessToken = request.accessToken;

            client.BaseAddress = new Uri("https://api.hubapi.com/crm/v3/objects/contacts");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            body.properties = new Dictionary<string, string>();
            body.properties.Add("firstname", request.firstname);
            body.properties.Add("lastname", request.lastname);
            body.properties.Add("email", request.email);


            var jsonBody = System.Text.Json.JsonSerializer.Serialize(body);
            var stringContent = new StringContent(jsonBody, Encoding.UTF32, "application/json");

            var url = "/crm/v3/objects/contacts";
            response = await client.PostAsync(url, stringContent);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
        //---------------------------------------------------------------------------------------------
        //private async static void WriteTokensAsync()
        //{

        //}
    }
    public class GetHubSpotTokensRequest
    {
        public string authorizationCode { get; set; } = string.Empty;
    }
    public class HubSpotTokensFormData
    {
        public Dictionary<string, string> properties { get; set; }
    }
    public class HubSpotTokensResponse
    {
        public string access_token { get; set; } = string.Empty;
        public string refresh_token { get; set; } = string.Empty;
    }
    public class GetHubSpotContactsRequest
    {
        public string accessToken { get; set; } = string.Empty;
    }
    public class PostHubSpotContactRequest
    {
        public string accessToken { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string firstname { get; set; } = string.Empty;
        public string lastname { get; set; } = string.Empty;
    }
    public class HubSpotContactPostRequest
    {
        public Dictionary<string, string> properties { get; set; }
    }

    public class HubSpotContactProperties
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
    }
}
