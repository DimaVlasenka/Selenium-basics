using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLearning
{
    public class DropDownAndRadioButton
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

        public void dropDown() {

            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

            IWebElement dropdownField = driver.FindElement(By.CssSelector("select.form-control"));

            SelectElement dropDownPicker = new SelectElement(dropdownField);

            dropDownPicker.SelectByText("Teacher");
            Thread.Sleep(2000);
            dropDownPicker.SelectByValue("consult");
            Thread.Sleep(2000);
            dropDownPicker.SelectByIndex(0);

        }

        [Test]
        public void radioButton() {
            
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

            IList <IWebElement> radioButtonsList = driver.FindElements(By.CssSelector("input[type='radio']"));

            foreach (IWebElement radioButton in radioButtonsList)
            {
                if (radioButton.GetAttribute("value").Equals("user"))
                {
                    radioButton.Click();
                }
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("okayBtn")));

            driver.FindElement(By.Id("okayBtn")).Click();

            Boolean isSelected = driver.FindElement(By.CssSelector("input[value='user']")).Selected;

            Assert.True(isSelected);
            //Assert.That(isSelected,Is.True);

            TestContext.Progress.WriteLine(isSelected);


        }
    
    }
}
