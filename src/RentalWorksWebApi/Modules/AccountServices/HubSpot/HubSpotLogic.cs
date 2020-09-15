using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.AccountServices.HubSpot
{
    public class HubSpotLogic : AppBusinessLogic
    {
        public async Task<OkObjectResult> GetContactsAsync([FromBody]GetHubSpotContactsRequest request)
        {
            HttpResponseMessage response;
            var client = new HttpClient();
            string accessToken = request.accessToken;

            client.BaseAddress = new Uri("https://api.hubapi.com/crm/v3/objects/contacts");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var url = "/crm/v3/objects/contacts";
            response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return new OkObjectResult(response);
        }

    }
    public class GetHubSpotContactsRequest
    {
        public string accessToken { get; set; } = string.Empty;
    }
}
