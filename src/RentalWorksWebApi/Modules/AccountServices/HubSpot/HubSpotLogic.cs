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

namespace WebApi.Modules.AccountServices.HubSpot
{
    public class HubSpotLogic : AppBusinessLogic
    {
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


            var jsonBody = JsonSerializer.Serialize(body);
            var stringContent = new StringContent(jsonBody, Encoding.UTF32, "application/json");

            var url = "/crm/v3/objects/contacts";
            response = await client.PostAsync(url, stringContent);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

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
