using System;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using WebDriverManager.DriverConfigs.Impl;

namespace SpecflowDemo.Hooks
{
   
    [Binding]
    public sealed class Hooks
    {
        private readonly IObjectContainer _container;
        //public IWebDriver driver;
        public Hooks(IObjectContainer container)
        {
            _container = container;
        }
        
       
        [BeforeScenario]
        public void getDriver()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            Console.WriteLine("starting open browser .....");
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            _container.RegisterInstanceAs<IWebDriver>(driver);
            

        }

        [AfterScenario]
        public void afterScenario()
        {
            var driver = _container.Resolve<IWebDriver>();
            if (driver!=null)
            {
                driver.Quit();
            }
        }
        
        
    }
    
}