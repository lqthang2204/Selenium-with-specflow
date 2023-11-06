using System.Collections.Specialized;
using Google.Protobuf.WellKnownTypes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.DefaultWaitHelpers;
using SpecflowDemo.ManagementLocator;
using SpecflowDemo.support;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace SpecflowDemo.Steps;

[Binding]
public class GeneralPageDefinition
{
    private IWebDriver driver;
    public ManageLocator locator  = new ManageLocator();
    public Dictionary<String, Page> dict_page = new Dictionary<string, Page>();
    public Page page_yaml;
    public WebDriverWait wait;
    
    public GeneralPageDefinition(IWebDriver driver)
    {
        this.driver = driver;
        wait =new WebDriverWait(driver, TimeSpan.FromSeconds(10));;
    }
    
    [Given(@"I change the page spec to (.*)")]
    public void GivenIChangeThePageSpecToLoginPage(String page)
    {
        Console.WriteLine("page = "+ page);
          page_yaml = locator.readFileYAML(page, dict_page);
        Console.WriteLine("page = "+ page_yaml);
        
    }

    [Given(@"I type ""(.*)"" into element (.*)")]
    public void GivenITypeIntoElementUserField(string value, String ele)
    {
        Element element = locator.getLocator(ele, page_yaml);
        Locator loc = element.locators[0];
       
        By by = locator.getBy(loc);
        driver.FindElement(by).SendKeys(value);
    }

    [Given(@"I click element (.*)")]
    public void GivenIClickElementLoginButton(String ele)
    {
        Element element = locator.getLocator(ele, page_yaml);
        Locator loc = element.locators[0];
       
        By by = locator.getBy(loc);
        driver.FindElement(by).Click();
    }

    [Given(@"I perform (.*) action with override values")]
    public void GivenIPerformLoginActionActionWithOverrideValues(String action_id, Table table)
    {
            Actions action = locator.getAction(action_id, page_yaml);
            locator.execute_action_with_override(action, driver, wait,  table);
    }

    [Given(@"I wait for element (.*) to be (.*)")]
    public void GivenIWaitForElementNewCustomerToBeEnabled(String ele, String status)
    {
        Element element = locator.getLocator(ele, page_yaml);
        Locator loc = element.locators[0];
       
        By by = locator.getBy(loc);
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        locator.checkStatus(by, status, wait);
    }

    [Given(@"I wait (.*) seconds")]
    public void GivenIWaitSeconds(int seconds)
    {
       Thread.Sleep(seconds*1000);
    }

    [Given(@"I verify the text for element (.*) is ""(.*)""")]
    public void GivenIVerifyTheTextForElementSearchFieldIs(string ele, String text)
    {
        Element element = locator.getLocator(ele, page_yaml);
        Locator loc = element.locators[0];
       
        By by = locator.getBy(loc);
        var result = driver.FindElement(by).Text;
       Assert.Equal(text, result);
    }

    [Given(@"I perform (.*) action")]
    public void GivenIPerformLoginActionAction(String action_id)
    {
        Actions action = locator.getAction(action_id, page_yaml);
        locator.execute_action(action, driver, wait);
        
    }
}