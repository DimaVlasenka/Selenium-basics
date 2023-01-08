using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class WindowHandler
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

        [Test]
        public void testWindowHandler() 
        {
            //open url
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

            String parentWindowName = driver.CurrentWindowHandle;

            //click on blinking link
            driver.FindElement(By.ClassName("blinkingText")).Click();

            //switch to second window/tab
            //String windowNameToSwitch = driver.WindowHandles[1];
            //driver.SwitchTo().Window(windowNameToSwitch);

            IList<String> opendWindowsList = driver.WindowHandles;
            driver.SwitchTo().Window(opendWindowsList[1]);

            //take red text
            String redText = driver.FindElement(By.ClassName("red")).Text;
            TestContext.Progress.WriteLine(redText);

            //retrieve email from red text
            String[] splitedByAtArray = redText.Split("at");

            String trimmedText = splitedByAtArray[1].Trim();

            String retrievedEmail = trimmedText.Split(" ")[0];

            TestContext.Progress.WriteLine(retrievedEmail);

            //Enter email into parent window field
            driver.SwitchTo().Window(parentWindowName);
            driver.FindElement(By.Id("username")).SendKeys(retrievedEmail);



        }
    }
}
