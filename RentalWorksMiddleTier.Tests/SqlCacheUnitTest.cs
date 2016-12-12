using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RentalWorksMiddleTier.Models;

namespace RentalWorksMiddleTier.Tests
{
    [TestClass]
    public class SqlCacheUnitTest
    {

        // still working on this test mv 2016-08-05
        [TestMethod]
        public async Task TestGetData()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add( new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            SqlCacheModels.GetDataRequest request = new SqlCacheModels.GetDataRequest();
            request.table = "warehouse";
            request.uniqueids = new List<string> { "warehouseid", "whcode" };
            request.uniqueidvalues = new List<string> { "B0029AY6", "LA" };
            request.columns = new List<string> { "warehouse", "attention", "warehouseid", "whcode" };
            string jsonRequest = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(jsonRequest);
            content.Headers.Add("Accept", "application/json");
            content.Headers.Add("Content-Type", "application/json");
            HttpResponseMessage msg = await client.PostAsync("http://localhost/RentalWorksMiddleTier/sqlcache/getdata", content);
            string jsonResponse = await msg.Content.ReadAsStringAsync();
            SqlCacheModels.GetDataResponse response = JsonConvert.DeserializeObject<SqlCacheModels.GetDataResponse>(jsonResponse);
        }
    }
}
