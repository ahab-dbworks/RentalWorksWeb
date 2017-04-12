using Fw.Json.SqlServer;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalWorksTest.QuikScan.Modules
{
    [TestFixture]
    public class LoginTestFixture
    {
        //----------------------------------------------------------------------------------------------------
        IWebDriver driver;
        public static dynamic data = JsonConvert.DeserializeObject<ExpandoObject>(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, @"QuikScan\Modules\Login\LoginTestData.json")));
        //----------------------------------------------------------------------------------------------------
        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "http://localhost/qs/";
            FuncQS.driver = driver;
        }
        //----------------------------------------------------------------------------------------------------
        public static void LoginUser()
        {
            // login screen
            FuncQS.FindBySecId("Login", "71603D65-6190-4798-B4B2-F24887842893", 15, FuncQS.WaitTypes.Clickable)
                .SendKeys(data.username);
            FuncQS.FindBySecId("Password TextBox", "E154C291-613B-4904-AE1A-69266593117D", 0, FuncQS.WaitTypes.Clickable)
                .SendKeys(data.password);
            FuncQS.FindBySecId("Login Button", "8011B1D3-E6BC-4B56-A235-57E9580C13F4", 0, FuncQS.WaitTypes.Clickable)
                .Click();    
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetWebUsersRecord()
        {
            dynamic result;
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select top 1 *");
                qry.Add("from webusersview with (nolock)");
                qry.Add("where userloginname = @userloginname");
                qry.AddParameter("@userloginname", data.username);
                result = qry.QueryToDynamicObject2();
            }
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        [Test]
        public void LoginTest()
        {
            LoginUser();    
        }
        //----------------------------------------------------------------------------------------------------
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
        //----------------------------------------------------------------------------------------------------
    }
}
