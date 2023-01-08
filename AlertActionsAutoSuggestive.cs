using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using System.Collections;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLearning
{
    public class AlertActionsAutoSuggestive
    {
        IWebDriver driver;

        [SetUp]
        public void setUpBrowser()
        {

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();


        }

        [Test, Category("Smoke")]
        public void testAlert() 
        {
            //open URL
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";

            //enter text 'Dima Vlas' into text field
            String inputValue = "Dima Vlas";
            driver.FindElement(By.XPath("//input[@name='enter-name']")).SendKeys(inputValue);

            //click on "Confirm" button
            driver.FindElement(By.XPath("//input[@id='confirmbtn']")).Click();

            //Get the text from the alert
            String alertText = driver.SwitchTo().Alert().Text;
            TestContext.Progress.WriteLine(alertText);

            //Verify the alert text contains input text
            StringAssert.Contains(inputValue,alertText);
            
            //click "Ok" button from the alert
            driver.SwitchTo().Alert().Accept();
        
        }

        [Test, Category("Regression")]
        public void TestAutoSuggestiveDropDowns() 
        {
            //open URL
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";

            //enter "Bel" into text field
            String inputText = "Be";
            driver.FindElement(By.XPath("//input[@id='autocomplete']")).SendKeys(inputText);

            //Select Belarus value form the dropdown
            String countryToSelect = "Belarus";

            
            IList<IWebElement> countriesList = driver.FindElements(By.XPath("//li[@class='ui-menu-item']/div"));

            foreach (IWebElement country in countriesList) {

                if (country.Text.Equals(countryToSelect)) 
                {
                    country.Click();
                }

            }

            //to verify slected value is equal to expected
            String selectedValue = driver.FindElement(By.XPath("//input[@id='autocomplete']")).GetAttribute("value");
            
            //TestContext.Progress.WriteLine(selectedValue);
            //TestContext.Progress.WriteLine(countryToSelect);


            Assert.That(selectedValue, Is.EqualTo(countryToSelect));

        }

        [Test]
        public void TestActions() 
        {
            //open url
            driver.Url = "https://rahulshettyacademy.com/";

            //hover over "More" tab
            IWebElement elementToHover = driver.FindElement(By.XPath("//a[contains(text(),'More')][1]"));
            Actions actor = new Actions(driver);
            actor.MoveToElement(elementToHover).Perform();

            //wait auntil "Part time jobs" element will be visible
           WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
           wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//ul[@class='dropdown-menu']/li[2]/a")));

            //select "Part time jobs" element
            //driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[2]/a")).Click();
            actor.MoveToElement(driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[2]/a"))).Click().Perform();


        }

        [Test]
        public void DragAndDrop() 
        {
            driver.Url = "https://demoqa.com/droppable";
            Actions actor = new Actions(driver);

            IWebElement sourceElement = driver.FindElement(By.Id("draggable"));
            IWebElement destinationElement = driver.FindElement(By.Id("droppable"));
            
            actor.DragAndDrop(sourceElement,destinationElement).Perform();


        }

        [Test]
        public void testFrame() 
        {
            //open url
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";

            //scroll down to the frame
            IWebElement scrollToFrame = driver.FindElement(By.Id("courses-iframe"));
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", scrollToFrame);

            //click on "Learning Paths" link on frame
            driver.SwitchTo().Frame("iframe-name");
            driver.FindElement(By.LinkText("Learning Paths")).Click();

            //veirfy the the "Learning Paths" page is displayed
            
            String expectedFramePageHeader = "Learning Paths";

            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("h1")));

            Thread.Sleep(1000);

            String framePageHeader = driver.FindElement(By.CssSelector("h1")).Text;

            TestContext.Progress.WriteLine(framePageHeader);

            StringAssert.AreEqualIgnoringCase(expectedFramePageHeader, framePageHeader);

            Thread.Sleep(3000);

            driver.SwitchTo().DefaultContent();

            IWebElement scrollToPageHeader = driver.FindElement(By.TagName("h1"));
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", scrollToPageHeader);

            String expectedPageHeader = "Practice Page";

            String pageHeader = driver.FindElement(By.CssSelector("h1")).Text;

            TestContext.Progress.WriteLine(pageHeader);

            StringAssert.AreEqualIgnoringCase(expectedPageHeader,pageHeader);

        }

        [TearDown]

        public void closeBrowser()
        {
            driver.Quit();
        }

    }
}
