using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SpecflowDemo.ManagementLocator;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SpecflowDemo.support;

public class ManageLocator
{
   public Page readFileYAML(String page, Dictionary<String, Page> dic_page)
   
   {
      Page p;
      if (dic_page.ContainsKey(page))
      {
         p = dic_page[page];
      }
      else
      {
         var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();
         p = deserializer.Deserialize<Page>(new StringReader(File.ReadAllText("E:\\SpecFlow\\SpecflowDemo\\SpecflowDemo\\Resource\\"+page+".yaml")));
         dic_page.Add(page, p);
      }
      return p;


   }

   public Element getLocator(String id, Page page)
   {
      var list_element = page.elements;
      for (int i=0; i<list_element.Count;i++)
      {
         if (list_element[i].id == id)
         {
           Element element = list_element[i];
           return element;
         }  
      }

      return null;
   }

   public By getBy(Locator locator)
   {
      switch (locator.type) {
         case "XPATH":
            return By.XPath(locator.value);
         case "CSS":
            return By.CssSelector(locator.value);
         case "CLASS_NAME":
            return By.ClassName(locator.value);
         case "LINK_TEXT":
            return By.LinkText(locator.value);
         case "PARTIALLINK_TEXT":
            return By.PartialLinkText(locator.value);
         case "ID":
            return By.Id(locator.value);
         case "NAME":
            return By.Name(locator.value);
         default:
            throw new NotFoundException($"the locator type {locator.type}  is not supported.");
      }
   }

   public void checkStatus(By by, String status, WebDriverWait wait) {
        try {
            switch (status) {
                case "DISPLAYED":
                   wait.Until(ExpectedConditions.ElementIsVisible(by));
                    break;
                case "ENABLED":
                   wait.Until(ExpectedConditions.ElementToBeClickable(by));
                   break;
                
            }
        } catch (Exception e) {

        }

    }
   


}