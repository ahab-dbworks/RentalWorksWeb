using RentalWorksAPI.api.v2.Models.OrderModels.LineItems;
using RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetailModels;
using RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract;
using RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels;
using RentalWorksAPI.api.v2.Models.WarehouseModels.UnstageItemModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;

namespace RentalWorksTest.RentalWorksAPI
{
    public class XUnitWarehouseTests
    {
        //----------------------------------------------------------------------------------------------------
        private readonly ITestOutputHelper output;
        public XUnitWarehouseTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        //----------------------------------------------------------------------------------------------------
        [Fact]
        public void StageItem()
        {
            string url = "v2/warehouse/stageitem";
            OrderStatusDetailResponse response;
            using (HttpClient client = FuncApi.CreateHttpClient())
            {
                StageItemRequest request = new StageItemRequest();
                request.orderid = "B000V5V5";
                request.items = new List<StageItem>();
                HttpResponseMessage message = client.PostAsJsonAsync<StageItemRequest>(url, request).Result;
                if (!message.IsSuccessStatusCode)
                {
                    FuncApi.LogWebServiceError(output, message);
                }
                Assert.True(message.IsSuccessStatusCode, "Didn't get a success status code.");
                response = message.Content.ReadAsAsync<OrderStatusDetailResponse>().Result;
            }
            Assert.NotNull(response);
            Assert.NotEqual(string.Empty, response.order.orderid);
            Assert.NotEqual(string.Empty, response.order.orderdesc);
            Assert.NotEmpty(response.order.items);
        }
        //----------------------------------------------------------------------------------------------------
        [Fact]
        public void UnstageItem()
        {
            string url = "v2/warehouse/unstageitem";
            OrderStatusDetailResponse response;
            using (HttpClient client = FuncApi.CreateHttpClient())
            {
                UnstageItemRequest request = new UnstageItemRequest();
                request.orderid = "B000V5V5";
                request.items = new List<UnstageItemRequestItem>();
                HttpResponseMessage message = client.PostAsJsonAsync<UnstageItemRequest>(url, request).Result;
                if (!message.IsSuccessStatusCode)
                {
                    FuncApi.LogWebServiceError(output, message);
                }
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
        public void MoveToContract()
        {
            string url = "v2/order/movetocontract";
            MoveToContractResponse response;
            using (HttpClient client = FuncApi.CreateHttpClient())
            {
                MoveToContractRequest request = new MoveToContractRequest();
                request.orderid = "";
                request.usersid = "";
                request.items.Add(new StagedItem() { barcode="", masteritemid="", quantity=1 });
                HttpResponseMessage message = client.PostAsJsonAsync<MoveToContractRequest>(url, request).Result;
                if (!message.IsSuccessStatusCode)
                {
                    FuncApi.LogWebServiceError(output, message);
                }
                Assert.True(message.IsSuccessStatusCode);
                response = message.Content.ReadAsAsync<MoveToContractResponse>().Result;
            }
            Assert.NotNull(response);
            Assert.NotEqual(string.Empty, response.order.orderid);
            Assert.NotEmpty(response.order.items);
        }
        //----------------------------------------------------------------------------------------------------
    }
}
