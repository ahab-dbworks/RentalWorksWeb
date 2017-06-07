using Fw.Json.Services;
using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatusModels;
using RentalWorksAPI.api.v2.Models.InventoryModels.WarehouseAddToOrder;
using RentalWorksAPI.api.v2.Models.OrderModels.LineItems;
using RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetailModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;

namespace RentalWorksTest.RentalWorksAPI
{
    public class XInventoryTests
    {
        //----------------------------------------------------------------------------------------------------
        private readonly ITestOutputHelper output;
        public XInventoryTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        //----------------------------------------------------------------------------------------------------
        public class ItemStatusData
        {
            public string barcode { get; set; } = string.Empty;
            public string serialno { get; set; } = string.Empty;
            public string rfid { get; set; } = string.Empty;
            public int days { get; set; } = 0;
        }

        public static IEnumerable<object[]> GetItemStatusData()
        {
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select top 1 barcode");
                qry.Add("from rentalitem with (nolock)");
                qry.Add("where barcode <> ''");
                FwJsonDataTable dt = qry.QueryToFwJsonTable(true);
                for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
                {
                    yield return new object[] { new ItemStatusData {
                        barcode = dt.GetValue(rowno, "barcode").ToString().TrimEnd()
                    } };
                }
            }

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select top 1 mfgserial");
                qry.Add("from rentalitem with (nolock)");
                qry.Add("where mfgserial <> ''");
                FwJsonDataTable dt = qry.QueryToFwJsonTable(true);
                for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
                {
                    yield return new object[] { new ItemStatusData {
                        serialno = dt.GetValue(rowno, "mfgserial").ToString().TrimEnd()
                    } };
                }
            }

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select top 1 rfid");
                qry.Add("from rentalitem with (nolock)");
                qry.Add("where rfid <> ''");
                FwJsonDataTable dt = qry.QueryToFwJsonTable(true);
                for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
                {
                    yield return new object[] { new ItemStatusData {
                        rfid = dt.GetValue(rowno, "rfid").ToString().TrimEnd()
                    } };
                }
            }
        }

        [Theory]
        [MemberData("GetItemStatusData", new object[] {})]
        public void ItemStatus(ItemStatusData data)
        {
            string url = string.Format("v2/inventory/itemstatus?barcode={0}&serialno={1}&rfid={2}&days={3}", data.barcode, data.serialno, data.rfid, data.days);
            using (HttpClient client = FuncApi.CreateHttpClient())
            {
                HttpResponseMessage message = client.GetAsync(url).Result;
                if (!message.IsSuccessStatusCode)
                {
                    FuncApi.LogWebServiceError(output, message);
                }
                Assert.True(message.IsSuccessStatusCode);
                ItemStatusResponse response = message.Content.ReadAsAsync<ItemStatusResponse>().Result;
                Assert.NotNull(response);
                //Assert.NotEmpty(response.items);
            }

        }
        //----------------------------------------------------------------------------------------------------
        public class WarehouseAddToOrderData
        {
            public string warehouseid { get; set; } = string.Empty;
        }

        public static IEnumerable<object[]> GetWarehouseAddToOrderData()
        {
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select top 1 warehouseid");
                qry.Add("from warehouse with (nolock)");
                FwJsonDataTable dt = qry.QueryToFwJsonTable(true);
                for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
                {
                    yield return new object[] { new WarehouseAddToOrderData {
                        warehouseid = dt.GetValue(rowno, "warehouseid").ToString().TrimEnd()
                    } };
                }
            }  
        }

        [Theory]
        [MemberData(nameof(GetWarehouseAddToOrderData))]
        public void WarehouseAddToOrder(WarehouseAddToOrderData data)
        {
            string url = "v2/inventory/warehouseaddtoorder?warehouseid=" + data.warehouseid;
            WarehouseAddToOrderResponse response;
            using (HttpClient client = FuncApi.CreateHttpClient())
            {
                HttpResponseMessage message = client.GetAsync(url).Result;
                if (!message.IsSuccessStatusCode)
                {
                    FuncApi.LogWebServiceError(output, message);
                }
                Assert.True(message.IsSuccessStatusCode);
                response = message.Content.ReadAsAsync<WarehouseAddToOrderResponse>().Result;
            }
            Assert.NotNull(response);
            Assert.NotEmpty(response.items);
            foreach (WarehouseAddToOrderItem item in response.items)
            {
                Assert.NotEqual(string.Empty, item.department);
                Assert.NotEqual(string.Empty, item.departmentid);
                Assert.NotEqual(string.Empty, item.master);
                Assert.NotEqual(string.Empty, item.masterid);
                Assert.NotEqual(string.Empty, item.masterno);
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
}
