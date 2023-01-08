using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    
    public class Locators
    {
        IWebDriver driver;
        

        [SetUp]
        public void setUpBrowser() {

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            //Implicit wait can be declared globaly
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                        
            driver.Manage().Window.Maximize();
                     

        }

        [Test]
        public void logInTest() {

            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            //IWebElement userNameLocator = driver.FindElement(By.Id("username"));

            //enter username
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademyWRONG");
            
            //enter password
            driver.FindElement(By.Name("password")).SendKeys("learning");

            // check checkbox
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();

            //click on Sign In button
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            //Thread.Sleep(3000);

            //Expicit wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.ClassName("alert-danger")));

            String errorMessage = driver.FindElement(By.ClassName("alert-danger")).Text;

            TestContext.Progress.WriteLine(errorMessage);

            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));

            String linkText = link.Text;

            //link.Click();
                                               
            String actualUrl = link.GetAttribute("href");
            String expectedUrl = "https://rahulshettyacademy.com/documents-request";

            Assert.AreEqual(expectedUrl, actualUrl);
            //Assert.AreEqual("Incorrect username/password.", errorMessage);

            driver.Quit();


        }

    }
}
