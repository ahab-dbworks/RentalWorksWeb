using NUnit.Framework;
using RentalWorksAPI.api.v2.Models.OrderModels.LineItems;
using RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetailModels;
using RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RentalWorksTest.RentalWorksAPI
{
    [TestFixture]
    class WarehouseTestFixture
    {
        [Test]
        public void StageItem()
        {
            string url = "v2/warehouse/stageitem";
            using (HttpClient client = FuncApi.CreateHttpClient())
            {
                StageItemRequest request = new StageItemRequest();
                request.orderid = "";
                request.items = new List<StageItem>();
                HttpResponseMessage message = client.PostAsJsonAsync<StageItemRequest>(url, request).Result;
                Assert.IsTrue(message.IsSuccessStatusCode, "Didn't get a success status code.");
                OrderStatusDetailResponse response = message.Content.ReadAsAsync<OrderStatusDetailResponse>().Result;
                Assert.IsNotNull(response, "Response is null");
                Assert.IsNotEmpty(response.order.orderid, "orderid is empty");
                Assert.IsNotEmpty(response.order.orderdesc, "orderdesc is empty");
                Assert.NotZero(response.order.items.Count, "item count is 0");
            }
                
        }

        [Test]
        public void LineItems()
        {
            string orderid = "B000V5V5";
            string url = "v2/order/lineitems?orderid=" + orderid;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage message = client.GetAsync(url).Result;
                Assert.IsTrue(message.IsSuccessStatusCode);
                LineItemsResponse response = message.Content.ReadAsAsync<LineItemsResponse>().Result;
                Assert.IsNotNull(response, "Response is null");
                Assert.IsNotEmpty(response.order.orderid, "orderid is empty");
                Assert.NotZero(response.order.items.Count, "item count is 0");
            }
        }
    }
}
