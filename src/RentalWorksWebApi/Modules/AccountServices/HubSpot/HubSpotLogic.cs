using Microsoft.AspNetCore.Mvc;
using FwStandard.Modules.Administrator.SecuritySettings;
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
using FwStandard.BusinessLogic;

namespace WebApi.Modules.AccountServices.HubSpot
{
    public class HubSpotLogic : AppBusinessLogic
    {
        public async Task<GetWriteTokensResponse> GetTokensAsync([FromBody]GetHubSpotTokensRequest request)
        {
            GetWriteTokensResponse response = new GetWriteTokensResponse();
            var client = new HttpClient();
            var ssl = CreateBusinessLogic<SecuritySettingsLogic>(this.AppConfig, this.UserSession);
            var currentSecuritySettings = await ssl.GetSettingsAsync<SecuritySettingsLoader>("1");
            SecuritySettingsLoader securitySettings = currentSecuritySettings;

            var url = "https://api.hubapi.com/oauth/v1/token";
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            HubSpotTokensFormData body = new HubSpotTokensFormData();
            body.properties = new Dictionary<string, string>();
            //move HubSpot meta data to a config file 
            body.properties.Add("client_id", "7fd2d81a-ae52-4a60-af93-caa4d1fd5848");
            body.properties.Add("client_secret", "3fa85657-d67b-4965-bd47-929ba0604dc6");
            body.properties.Add("redirect_uri", "http://localhost:57949/webdev");
            body.properties.Add("grant_type", "authorization_code");
            body.properties.Add("code", request.authorizationCode);

            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(body.properties) };
            req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");


            var httpResponse = await client.SendAsync(req);
            httpResponse.EnsureSuccessStatusCode();

            var responseBody = await httpResponse.Content.ReadAsStringAsync();
            var jsonTokens = JsonConvert.DeserializeObject<HubSpotTokensResponse>(responseBody);
            //write tokens to dbo.control
            securitySettings.hubspotaccesstoken = jsonTokens.access_token;
            securitySettings.hubspotrefreshtoken = jsonTokens.refresh_token;

            var saveSettingsResponse = await ssl.SaveSettingsAsync<SecuritySettingsLoader>("1", securitySettings);
            response.message = saveSettingsResponse.Message;

            return response;

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
        public async Task<dynamic> SearchContactsWithinPeriodAsync([FromBody]SearchHubSpotContactsWithinPeriodRequest request)
        {
            //if access token is expired get a new one with refresh token.
            var client = new HttpClient();
            var deserializedResponse = "";
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {request.accessToken}");

            HubSpotSearchFilters filters = new HubSpotSearchFilters();
            filters.propertyName = "firstname";
            filters.@operator = "NEQ";
            filters.value = "Tom";

            //build funky nested arrays in objects format for searching, 
            //hubspot is setup this way so you can apply multiple filter groups to a search
            HubSpotSearchFilters[] filtersArray = new HubSpotSearchFilters[] { filters };
            HubSpotSearchFiltersObj filtersObj = new HubSpotSearchFiltersObj();
            filtersObj.filters = filtersArray;
            HubSpotSearchFiltersObj[] filtersObjArray = new HubSpotSearchFiltersObj[] { filtersObj };
            HubSpotSearchFilterGroups searchFilterGroups = new HubSpotSearchFilterGroups();
            searchFilterGroups.filterGroups = filtersObjArray;

            //build our request params
            var jsonBody = System.Text.Json.JsonSerializer.Serialize(searchFilterGroups);
            var stringContent = new StringContent(jsonBody, Encoding.UTF32, "application/json");
            var url = "https://api.hubapi.com/crm/v3/objects/contacts/search";

            //do response things
            var httpResponse = await client.PostAsync(url, stringContent);
            var responseBody = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //deserialize successful response of searched contacts and return them
            } else
            {
                DeserializedHubSpotErrorResponse errorResponse = System.Text.Json.JsonSerializer.Deserialize<DeserializedHubSpotErrorResponse>(responseBody);
                if (errorResponse.category == "EXPIRED_AUTHENTICATION")
                {
                    //get the current refresh token
                    var ssl = CreateBusinessLogic<SecuritySettingsLogic>(this.AppConfig, this.UserSession);
                    var securitySettings = await ssl.GetSettingsAsync<SecuritySettingsLoader>("1");
                    var refreshToken = securitySettings.hubspotrefreshtoken;

                    RenewAccessTokenRequest r = new RenewAccessTokenRequest();
                    r.refreshToken = refreshToken;

                    var renewResponse = await RenewAccessTokenAsync(r);

                    //if we successfully renew the access token, we search again with the new token.
                    if (renewResponse.message == "Success")
                    {
                        SearchHubSpotContactsWithinPeriodRequest newSearchReq = new SearchHubSpotContactsWithinPeriodRequest();
                        newSearchReq.accessToken = renewResponse.accessToken;
                        return await SearchContactsWithinPeriodAsync(newSearchReq);
                    }

                }
            }
            

            //if the access token is expired, use the refresh token for a new one
            

            return deserializedResponse;
            //time supplied in hubspot filter call must be epoch time
            // body example for filtered contacts request 
            //{
            //    "filterGroups": [{"filters": [{"value": "1600977557", "propertyName": "createdate", "operator": "LT"}]}],
            //"limit": 0,
            //"after": 0
            // }
        }
        //---------------------------------------------------------------------------------------------
        //public async Task<string> SyncContactsAsync([FromBody]GetHubSpotContactsRequest request)
        //{
        //    //be sure to log hubspot contacts_id in source_id on dbo.contact, we use this to see if the duplicate is a new contact or the same. 
        //    //check against email addresses to verify duplicates, i.e there could be many john smiths in one system.
        //}
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
        public async Task<DeleteHubSpotTokens> DeleteTokensAsync()
        {
            DeleteHubSpotTokens response = new DeleteHubSpotTokens();
            var ssl = CreateBusinessLogic<SecuritySettingsLogic>(this.AppConfig, this.UserSession);
            var currentSecuritySettings = await ssl.GetSettingsAsync<SecuritySettingsLoader>("1");
            currentSecuritySettings.hubspotaccesstoken = "";
            currentSecuritySettings.hubspotrefreshtoken = "";

            await ssl.SaveSettingsAsync<SecuritySettingsLoader>("1", currentSecuritySettings);

            response.message = "Success";
            return response;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<RenewAccessTokenResponse> RenewAccessTokenAsync([FromBody]RenewAccessTokenRequest request)
        {
            var client = new HttpClient();
            RenewAccessTokenResponse response = new RenewAccessTokenResponse();
            HubSpotTokensFormData body = new HubSpotTokensFormData();
            var ssl = CreateBusinessLogic<SecuritySettingsLogic>(this.AppConfig, this.UserSession);
            var securitySettings = await ssl.GetSettingsAsync<SecuritySettingsLoader>("1");

            body.properties = new Dictionary<string, string>();
            body.properties.Add("client_id", "7fd2d81a-ae52-4a60-af93-caa4d1fd5848");
            body.properties.Add("client_secret", "3fa85657-d67b-4965-bd47-929ba0604dc6");
            body.properties.Add("grant_type", "refresh_token");
            body.properties.Add("refresh_token", request.refreshToken);

            var url = "https://api.hubapi.com/oauth/v1/token";
            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(body.properties) };
            req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var httpResponse = await client.SendAsync(req);
            httpResponse.EnsureSuccessStatusCode();

            var responseBody = await httpResponse.Content.ReadAsStringAsync();
            var jsonTokens = JsonConvert.DeserializeObject<HubSpotTokensResponse>(responseBody);

            securitySettings.hubspotaccesstoken = jsonTokens.access_token;
            await ssl.SaveSettingsAsync<SecuritySettingsLoader>("1", securitySettings);

            response.message = "Success";
            response.accessToken = jsonTokens.access_token;

            return response;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<GetHubSpotRefreshTokenBool> GetRefreshTokenBoolAsync()
        {
            GetHubSpotRefreshTokenBool response = new GetHubSpotRefreshTokenBool();
            var ssl = CreateBusinessLogic<SecuritySettingsLogic>(this.AppConfig, this.UserSession);
            var currentSecuritySettings = await ssl.GetSettingsAsync<SecuritySettingsLoader>("1");

            if (!string.IsNullOrEmpty(currentSecuritySettings.hubspotrefreshtoken))
            {
                response.hasRefreshToken = true;
            } else
            {
                response.hasRefreshToken = false;
            }
            return response;
        }
    }
    //---------------------------------------------------------------------------------------------
    public class GetHubSpotTokensRequest
    {
        public string authorizationCode { get; set; } = string.Empty;
    }
    //---------------------------------------------------------------------------------------------
    public class HubSpotTokensFormData
    {
        public Dictionary<string, string> properties { get; set; }
    }
    //---------------------------------------------------------------------------------------------
    public class HubSpotTokensResponse
    {
        public string access_token { get; set; } = string.Empty;
        public string refresh_token { get; set; } = string.Empty;
    }
    //---------------------------------------------------------------------------------------------
    public class GetWriteTokensResponse
    {
        public string message { get; set; } = string.Empty;
    }
    //---------------------------------------------------------------------------------------------
    public class GetHubSpotContactsRequest
    {
        public string accessToken { get; set; } = string.Empty;
    }
    //---------------------------------------------------------------------------------------------
    public class PostHubSpotContactRequest
    {
        public string accessToken { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string firstname { get; set; } = string.Empty;
        public string lastname { get; set; } = string.Empty;
    }
    //---------------------------------------------------------------------------------------------
    public class HubSpotContactPostRequest
    {
        public Dictionary<string, string> properties { get; set; }
    }
    //---------------------------------------------------------------------------------------------
    public class HubSpotContactProperties
    {
        public string firstname { get; set; } = string.Empty;
        public string lastname { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
    }
    //---------------------------------------------------------------------------------------------
    public class GetHubSpotRefreshTokenBool
    {
        public bool hasRefreshToken { get; set; }
    }
    //---------------------------------------------------------------------------------------------
    public class DeleteHubSpotTokens
    {
        public string message { get; set; } = string.Empty;
    }
    //---------------------------------------------------------------------------------------------
    public class RenewAccessTokenResponse
    {
        public string message { get; set; } = string.Empty;
        public string accessToken { get; set; } = string.Empty;
    }
    //---------------------------------------------------------------------------------------------
    public class RenewAccessTokenRequest
    {
        public string refreshToken { get; set; } = string.Empty;
    }
    //---------------------------------------------------------------------------------------------
    public class SearchHubSpotContactsWithinPeriodRequest
    {
        public string accessToken { get; set; } = string.Empty;
        public int? lastSyncEpoch { get; set; }
    }
    //---------------------------------------------------------------------------------------------
    public class HubSpotSearchFilters
    {
        public string propertyName { get; set; } = string.Empty;
        public string @operator { get; set; } = string.Empty;
        public string value { get; set; } = string.Empty;
    }
    //---------------------------------------------------------------------------------------------
    public class HubSpotSearchFiltersObj
    {
        public HubSpotSearchFilters[] filters { get; set; }
    }
    //---------------------------------------------------------------------------------------------
    public class HubSpotSearchFilterGroups
    {
        public HubSpotSearchFiltersObj[] filterGroups { get; set; }
    }
    //---------------------------------------------------------------------------------------------
    public class DeserializedHubSpotErrorResponse
    {
        public string status { get; set; } = string.Empty;
        public string category { get; set; } = string.Empty;
    }
    //---------------------------------------------------------------------------------------------
    public class HubSpotSearchedContactResultsDetails
    {
        public string createdAt { get; set; } = string.Empty;
        public bool archived { get; set; } = false;
        public string id { get; set; } = string.Empty;
        public HubSpotSearchedContactDetailsProperties[] properties { get; set; }
        public string updatedAt { get; set; } = string.Empty;
    }
    //---------------------------------------------------------------------------------------------
    public class HubSpotSearchedContactDetailsProperties
    {
        public string company { get; set; } = string.Empty;
        public string createdate { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string firstname { get; set; } = string.Empty;
        public string lastmodifieddate { get; set; } = string.Empty;
        public string lastname { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string website { get; set; } = string.Empty;
    }
    //---------------------------------------------------------------------------------------------
    public class HubSpotSearchedContactsResponse
    {
        public int total { get; set; }
        public HubSpotSearchedContactResultsDetails[] results { get; set; }
    }
}
