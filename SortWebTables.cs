using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using AngleSharp.Text;
using System.Collections;

namespace SeleniumLearning
{
     public class SortWebTables 
    {
        IWebDriver driver;

        [SetUp]
        public void setUpBrowser()
        {

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();


        }

        [Test]

        public void SortTable() {
                                
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";

            SelectElement dropdown = new SelectElement(driver.FindElement(By.Id("page-menu")));

            dropdown.SelectByValue("20");

            ArrayList initialArray = new ArrayList();

            //Get all items names into arrayList A
            IList <IWebElement> InitialVegiesFruits = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement vegie in InitialVegiesFruits) { 
            
                initialArray.Add(vegie.Text);
            
            }

            //Sort the given arrayList
            initialArray.Sort();

            //Click on column header to sort list from UI //css - th[aria-label*='Veg/fruit name'] 
            //xpath //th[contains(@aria-label,'Veg/fruit name')]
            driver.FindElement(By.XPath("//span[text()='Veg/fruit name']")).Click();

            //get all items into new arrayList B
            ArrayList sortedUIArray = new ArrayList();

            IList<IWebElement> SortedVegiesFruits = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement vegie in SortedVegiesFruits)
            {

               sortedUIArray.Add(vegie.Text);

            }

            //compare arrayList A and arrayList B
            Assert.That(initialArray, Is.EqualTo(sortedUIArray));
            //Assert.AreEqual(initialArray, sortedUIArray);




        }
    }
}
