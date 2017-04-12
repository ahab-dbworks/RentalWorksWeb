using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RentalWorksTest.QuikScan
{
    public static class FuncQS
    {
        public enum WaitTypes { None, Visible, Clickable }
        //----------------------------------------------------------------------------------------------------
        public static IWebDriver driver { get; set; } = null;
        //----------------------------------------------------------------------------------------------------
        public static void WaitForAjaxToFinish(double timeout)
        {
            // wait until Ajax Loading messages disappears
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Message = String.Format("AJAX loading message is still showing after {0} second(s).", timeout);
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("[data-securityid=\"BE1CC122-874E-4B8D-BE02-08F62813E62D\"]")));
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Finds an IWebElement using a CSS attribute selector for data-securityid=""
        /// </summary>
        /// <param name="securityid">The security id guid.</param>
        /// <param name="timeout">Seconds to wait for the element to appear.</param>
        /// <returns></returns>
        public static IWebElement FindBySecId(string description, string securityid, double timeout, WaitTypes waitType)
        {
            string cssselector = "[data-securityid=\"" + securityid + "\"]";
            IWebElement element = FindByCssSelector(description, cssselector, timeout, waitType);
            return element;
        }
        //----------------------------------------------------------------------------------------------------
        public static IWebElement FindByCssSelector(string description, string cssselector, double timeout, WaitTypes waitType)
        {
            FuncQS.WaitForAjaxToFinish(15);
            By locator = By.CssSelector(cssselector);
            IWebElement element = null;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            switch (waitType)
            {
                case WaitTypes.None:
                    element = driver.FindElement(locator);
                    break;
                case WaitTypes.Visible:
                    wait.Message = String.Format("Element not found.\nDescription:{0}\nCssSelector:{1}\nTimeout:{2}", description, cssselector, timeout);
                    element = wait.Until<IWebElement>((d) =>
                    {
                        IWebElement el = null;
                        ReadOnlyCollection<IWebElement> elements = driver.FindElements(locator);
                        if (elements.Count == 1)
                        {
                            if (elements[0].Displayed)
                            {
                                el = elements[0];
                            }
                        }
                        else if (elements.Count > 1)
                        {
                            Assert.Fail("Multiple elements were found.\nDescription:{0}\nCssSelector:{1}\nTimeout:{2}", description, cssselector, timeout);
                        }

                        return el;
                    });
                    break;
                case WaitTypes.Clickable:
                    element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                    break;

            }
            Assert.IsNotNull(element, "{0} was not found after {1} second(s).", description, timeout);
            return element;
        }
        //----------------------------------------------------------------------------------------------------
        //public static void TextBoxSendKeys(string securityid, string text)
        //{
        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        //    IWebElement txt = wait.Until<IWebElement>(d => d.FindElement(By.CssSelector("[data-securityid=\"" + securityid + "\"]")));
        //    txt.SendKeys(text);
        //}
        //----------------------------------------------------------------------------------------------------
        //public static void ClickButton(string securityid, string text)
        //{
        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        //    IWebElement button = wait.Until<IWebElement>(d => d.FindElement(By.CssSelector("[data-securityid=\"" + securityid + "\"]")));
        //    button.Click();
        //}
        //----------------------------------------------------------------------------------------------------
    }
}
