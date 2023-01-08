using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using AngleSharp.Text;

namespace SeleniumLearning
{
     public class E2ETest 
    {
        IWebDriver driver;

        [SetUp]
        public void setUpBrowser()
        {

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();


        }

        [Test, Category("Regression")]

        public void EndToEndFlow() {

            String[] productsToSelect = { "iphone X", "Blackberry" };

            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

            //enter username
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");

            //enter password
            driver.FindElement(By.Name("password")).SendKeys("learning");

            // check checkbox
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();

            //click on Sign In button
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            //click on Add for prodicts from productToSelect list
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

            IList<IWebElement> presentedProducts = driver.FindElements(By.TagName("app-card"));

            foreach (IWebElement presentedProduct in presentedProducts) {

                if (productsToSelect.Contains(presentedProduct.FindElement(By.CssSelector(".card-title a")).Text)) {

                    presentedProduct.FindElement(By.CssSelector("button.btn")).Click();
                }
            }

            //click on Checkout button
            driver.FindElement(By.PartialLinkText("Checkout")).Click();

            //verify the list of selected products
            IList<IWebElement> selectedProducts = driver.FindElements(By.CssSelector("h4 a"));

            String[] selectedProductText = new String[selectedProducts.Count];

            for (int i = 0; i < selectedProducts.Count; i++) {

                selectedProductText[i] = selectedProducts[i].Text;

            }

            Assert.That(selectedProductText, Is.EqualTo(productsToSelect));
            //Assert.AreEqual(productsToSelect,selectedProductText);

            //click on Checkout
            driver.FindElement(By.CssSelector(".btn-success")).Click();

            //type Bel into text fiel and select Belarus
            driver.FindElement(By.Id("country")).SendKeys("Bel");

            String countryToSelect = "Belarus";

            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='suggestions']/ul")));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.LinkText(countryToSelect)));
            driver.FindElement(By.LinkText("Belarus")).Click();

            /*IList<IWebElement> countriesList = driver.FindElements(By.XPath("//div[@class='suggestions']/ul"));

            foreach (IWebElement country in countriesList) {

                if (country.Text.Equals(countryToSelect))
                {
                    country.Click();
                    break;
                }
                
            }
            */

            String selectedCountry = driver.FindElement(By.Id("country")).GetAttribute("value");

            Assert.That(selectedCountry, Is.EqualTo(countryToSelect));

            //check checkbox
            driver.FindElement(By.XPath("//label[@for='checkbox2']")).Click();
            Assert.True(driver.FindElement(By.XPath("//input[@type='checkbox']")).Selected);
                 

            //click on Purchase
            driver.FindElement(By.XPath("//input[@value='Purchase']")).Click();

            //verify success message
            String successMessage = driver.FindElement(By.CssSelector(".alert-success")).Text;
            TestContext.Progress.WriteLine(successMessage);
            String expectedSuccessMessage = "Success! Thank you! Your order will be delivered in next few weeks";

            StringAssert.Contains(expectedSuccessMessage,successMessage);
        }

        [TearDown]
        
        public void closeBrowser() 
        {
            driver.Quit();
        }
    }
}
