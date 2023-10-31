using OpenQA.Selenium;

namespace SpecflowDemo.Steps;

[Binding]
public sealed class loginPageDefinition
{
    private IWebDriver driver;
    public loginPageDefinition(IWebDriver driver)
    {
        this.driver = driver;
    }
    private ScenarioContext scenario;
    
    [Given(@"I navigate to ""(.*)""")]
    public void GivenINavigateTo(string url)
    {
        driver.Navigate().GoToUrl(url);
    }
    
}