using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RentalWorksAPI.api.v1;
using RentalWorksAPI.api.v1.Data;
using RentalWorksAPI.api.v1.Models;
using RentalWorksTest.QuikScan.Modules;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace RentalWorksTest.QuikScan.Modules
{
    [TestFixture]
    public class StagingTestFixture
    {
        IWebDriver driver;
        Order order1;
        public static dynamic data = JsonConvert.DeserializeObject<ExpandoObject>(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, @"QuikScan\Modules\Staging\StagingTestData.json")));

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "http://localhost/qs/";
            FuncQS.driver = driver;

            dynamic webuser = LoginTestFixture.GetWebUsersRecord();

            // Create an Order
            string orderJson = JsonConvert.SerializeObject(data.order1);
            order1 = JsonConvert.DeserializeObject<Order>(orderJson);
            order1.orderdesc       = "TEST: Staging " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            order1.location        = webuser.location;
            order1.estrentfrom     = DateTime.Today.ToShortDateString();
            order1.estrentto       = DateTime.Today.AddDays(7).ToShortDateString();
            order1.billperiodstart = DateTime.Today.ToShortDateString();
            order1.webusersid      = webuser.webusersid;
            order1.deliverondate   = DateTime.Today.ToShortDateString();
            order1.pickdate        = DateTime.Today.ToShortDateString();
            dynamic processQuoteResult = OrderData.ProcessQuote(order1);
            if (processQuoteResult.errno != "0")
            {
                Assert.Fail(processQuoteResult.errmsg);
            }
            string orderid = processQuoteResult.orderid;

            List<Order> result = OrderData.GetOrder(orderid: orderid, ordertype: "O", statuses: null, rental: null, sales: null, datestamp: null);
            //Assert.Equals(1, result.Count);
            order1 = result[0];

            // Login Module
            LoginTestFixture loginTextFixture = new LoginTestFixture();
            loginTextFixture.LoginTest();
        }

        [Test]
        public void StageOrder()
        {
            
            // Home Module
            FuncQS.FindBySecId("Staging Module Button", "D8FC5192-8AC0-431D-96FA-451E70A07471", 15, FuncQS.WaitTypes.Clickable)
                .Click();

            // Staging Module
            FuncQS.FindBySecId("Staging Screen", "B78883AE-63D1-4782-81BE-D21E8D8CE981", 15, FuncQS.WaitTypes.Visible);

            // Staging Page: Staging Menu
            FuncQS.FindBySecId("Staging Page Staging Menu", "FC95EF1A-8396-42BB-A415-6ED90DE9BFBB", 15, FuncQS.WaitTypes.Visible);
            FuncQS.FindBySecId("Order Search", "3A2B6758-CC60-4CC5-BDF5-AB8F8AD912AD", 15, FuncQS.WaitTypes.Clickable)
                .Click();

            // Test Search By Deal
            FuncQS.FindByCssSelector("Search By Deal", ".option[data-value=\"deal\"]", 15, FuncQS.WaitTypes.Clickable)
                .Click();
            FuncQS.FindByCssSelector("Search Box", "[data-securityid=\"CA52EDBF-1EA2-4C9A-A479-FA1444F3E436\"] .searchbox", 15, FuncQS.WaitTypes.Clickable)
                .SendKeys(order1.deal.dealname + "\n");
            IWebElement recordCount1 = FuncQS.FindByCssSelector("Record Count", "[data-securityid=\"CA52EDBF-1EA2-4C9A-A479-FA1444F3E436\"] .recordcount", 15, FuncQS.WaitTypes.Visible);
            int valRecordCount1 = FwConvert.ToInt32(recordCount1.Text.Replace(" items", string.Empty));
            Assert.GreaterOrEqual(valRecordCount1, 1);

            // Test Search By Description
            FuncQS.FindByCssSelector("Search By Description", ".option[data-value=\"orderdesc\"]", 15, FuncQS.WaitTypes.Clickable)
                .Click();
            FuncQS.FindByCssSelector("Search Box", "[data-securityid=\"CA52EDBF-1EA2-4C9A-A479-FA1444F3E436\"] .searchbox", 15, FuncQS.WaitTypes.Clickable)
                .SendKeys(order1.orderdesc + "\n");
            IWebElement recordCount2 = FuncQS.FindByCssSelector("Record Count", "[data-securityid=\"CA52EDBF-1EA2-4C9A-A479-FA1444F3E436\"] .recordcount", 15, FuncQS.WaitTypes.Visible);
            int valRecordCount2 = FwConvert.ToInt32(recordCount1.Text.Replace(" items", string.Empty));
            Assert.GreaterOrEqual(valRecordCount2, 1);

            // Test Search By Order No
            FuncQS.FindByCssSelector("Search By Order No", ".option[data-value=\"orderno\"]", 15, FuncQS.WaitTypes.Clickable)
                .Click();
            FuncQS.FindByCssSelector("Search Box", "[data-securityid=\"CA52EDBF-1EA2-4C9A-A479-FA1444F3E436\"] .searchbox", 15, FuncQS.WaitTypes.Clickable)
                .SendKeys(order1.orderno + "\n");
            IWebElement recordCount3 = FuncQS.FindByCssSelector("Record Count", "[data-securityid=\"CA52EDBF-1EA2-4C9A-A479-FA1444F3E436\"] .recordcount", 15, FuncQS.WaitTypes.Visible);
            int valRecordCount3 = FwConvert.ToInt32(recordCount1.Text.Replace(" items", string.Empty));
            Assert.AreEqual(valRecordCount3, 1);

            // Select Order

            // Finished
            Thread.Sleep(5000);
        }

        [TearDown]
        public void EndTest()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
