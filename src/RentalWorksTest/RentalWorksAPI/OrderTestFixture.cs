using NUnit.Framework;
using RentalWorksAPI.api.v2.Models.OrderModels.LineItems;
using RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetailModels;
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
    class OrderTestFixture
    {
        //----------------------------------------------------------------------------------------------------
        [Test]
        public void OrderStatusDetail()
        {
            string orderid = "B000V5V5";
            string url = "v2/order/orderstatusdetail?orderid=" + orderid;
            using (HttpClient client = FuncApi.CreateHttpClient())
            {
                HttpResponseMessage message = client.GetAsync(url).Result;
                if (message.IsSuccessStatusCode)
                {

                    OrderStatusDetailResponse response = message.Content.ReadAsAsync<OrderStatusDetailResponse>().Result;
                    Assert.IsNotNull(response, "Response is null");
                    Assert.IsNotEmpty(response.order.orderid, "orderid is empty");
                    Assert.IsNotEmpty(response.order.orderdesc, "orderdesc is empty");
                    Assert.NotZero(response.order.items.Count, "item count is 0");
                }
            }
        }
        //----------------------------------------------------------------------------------------------------
        [Test]
        public void LineItems()
        {
            string orderid = "B000V5V5";
            string url = "v2/order/lineitems?orderid=" + orderid;
            using (HttpClient client = FuncApi.CreateHttpClient())
            {
                HttpResponseMessage message = client.GetAsync(url).Result;
                if (message.IsSuccessStatusCode)
                {
                    LineItemsResponse response = message.Content.ReadAsAsync<LineItemsResponse>().Result;
                    Assert.IsNotNull(response, "Response is null");
                    Assert.IsNotEmpty(response.order.orderid, "orderid is empty");
                    Assert.NotZero(response.order.items.Count, "item count is 0");
                }
            }
                
        }
        //----------------------------------------------------------------------------------------------------
    }
}
