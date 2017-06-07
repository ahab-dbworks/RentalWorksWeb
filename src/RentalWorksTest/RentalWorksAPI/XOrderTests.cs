using Fw.Json.Services;
using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Models.OrderModels.LineItems;
using RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetailModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;

namespace RentalWorksTest.RentalWorksAPI
{
    public class XOrderTests
    {
        //----------------------------------------------------------------------------------------------------
        private readonly ITestOutputHelper output;
        public XOrderTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        //----------------------------------------------------------------------------------------------------
        // Test GET v2/order/orderstatusdetail
        //----------------------------------------------------------------------------------------------------
        public class OrderStatusDetailData
        {
            public string orderid { get; set; } = string.Empty;
        }

        public static IEnumerable<object[]> GetOrderStatusDetailData()
        {
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select top 1 orderid");
                qry.Add("from dealorder with (nolock)");
                qry.Add("where ordertype = 'O' and status = 'NEW'");
                qry.Add("order by datestamp desc");
                FwJsonDataTable dt = qry.QueryToFwJsonTable(true);
                for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
                {
                    yield return new object[] { new OrderStatusDetailData {
                        orderid = dt.GetValue(rowno, "orderid").ToString().TrimEnd()
                    } };
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetOrderStatusDetailData))]
        public void OrderStatusDetail(OrderStatusDetailData data)
        {
            string url = string.Format("v2/order/orderstatusdetail?orderid={0}", data.orderid);
            using (HttpClient client = FuncApi.CreateHttpClient())
            {
                HttpResponseMessage message = client.GetAsync(url).Result;
                if (!message.IsSuccessStatusCode)
                {
                    FuncApi.LogWebServiceError(output, message);
                }
                Assert.True(message.IsSuccessStatusCode);
                OrderStatusDetailResponse response = message.Content.ReadAsAsync<OrderStatusDetailResponse>().Result;
                Assert.NotNull(response);
                Assert.NotEqual(string.Empty, response.order.orderid);
                Assert.NotEqual(string.Empty, response.order.orderdesc);
                Assert.NotEmpty(response.order.items);
            }
        }
        //----------------------------------------------------------------------------------------------------
        // Test GET v2/order/lineitems
        //----------------------------------------------------------------------------------------------------
        [Fact]
        public void LineItems()
        {
            string url = string.Format("v2/order/lineitems?orderid={0}", "B000V5V5");
            using (HttpClient client = FuncApi.CreateHttpClient())
            {
                HttpResponseMessage message = client.GetAsync(url).Result;
                if (!message.IsSuccessStatusCode)
                {
                    FuncApi.LogWebServiceError(output, message);
                }
                Assert.True(message.IsSuccessStatusCode);
                LineItemsResponse response = message.Content.ReadAsAsync<LineItemsResponse>().Result;
                Assert.NotNull(response);
                Assert.NotEqual(string.Empty, response.order.orderid);
                Assert.NotEmpty(response.order.items);
            }
            
        }
        //----------------------------------------------------------------------------------------------------
    }
}
