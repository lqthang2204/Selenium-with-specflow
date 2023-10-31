namespace SpecflowDemo.ManagementLocator;

public class Element
{

    public String id { get; set; }
    public String description { get; set; }
    public List<Locator> locators { get; set; }
}