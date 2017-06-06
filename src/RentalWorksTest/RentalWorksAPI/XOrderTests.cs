using RentalWorksAPI.api.v2.Models.OrderModels.LineItems;
using RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetailModels;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace RentalWorksTest.RentalWorksAPI
{
    public class XOrderTests
    {
        //----------------------------------------------------------------------------------------------------
        [Fact]
        public void OrderStatusDetail()
        {
            string orderid = "B000V5V5";
            string url = "v2/order/orderstatusdetail?orderid=" + orderid;
            OrderStatusDetailResponse response;
            using (HttpClient client = FuncApi.CreateHttpClient())
            {
                HttpResponseMessage message = client.GetAsync(url).Result;
                Assert.True(message.IsSuccessStatusCode);
                response = message.Content.ReadAsAsync<OrderStatusDetailResponse>().Result;
            }
            Assert.NotNull(response);
            Assert.NotEqual(string.Empty, response.order.orderid);
            Assert.NotEqual(string.Empty, response.order.orderdesc);
            Assert.NotEmpty(response.order.items);
        }
        //----------------------------------------------------------------------------------------------------
        [Fact]
        public void LineItems()
        {
            string orderid = "B000V5V5";
            string url = "v2/order/lineitems?orderid=" + orderid;
            LineItemsResponse response;
            using (HttpClient client = FuncApi.CreateHttpClient())
            {
                HttpResponseMessage message = client.GetAsync(url).Result;
                Assert.True(message.IsSuccessStatusCode);
                response = message.Content.ReadAsAsync<LineItemsResponse>().Result;
            }
            Assert.NotNull(response);
            Assert.NotEqual(string.Empty, response.order.orderid);
            Assert.NotEmpty(response.order.items);
        }
        //----------------------------------------------------------------------------------------------------
    }
}
