using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class SeleniumFirst
    {
        IWebDriver driver;

        [SetUp]

        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            
            //new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            //driver = new EdgeDriver();
            
            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //driver = new FirefoxDriver();

            driver.Manage().Window.Maximize();

        
        }

        [Test]

        public void Test1() 
        {
            driver.Url= "https://rahulshettyacademy.com/loginpagePractise/";
            String actualTitle = driver.Title;
            String actualUrl = driver.Url;

            //String expectedTitle = "";
            TestContext.Progress.WriteLine(actualTitle);
            TestContext.Progress.WriteLine(actualUrl);
            //driver.Close();
            //driver.Quit();
            
        }
    }
}
