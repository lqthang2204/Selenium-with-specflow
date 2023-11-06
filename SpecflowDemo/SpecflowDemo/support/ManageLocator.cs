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
   public string PathYAML = System.IO.Directory.GetParent(@"../../../").FullName + Path.DirectorySeparatorChar;
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
         Console.WriteLine("path "+ PathYAML);
         p = deserializer.Deserialize<Page>(new StringReader(File.ReadAllText(PathYAML+"\\Resource\\"+page+".yaml")));
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
   public Actions getAction(String action_id, Page page)
   {
      var list_action = page.actions;
      for (int i=0; i<list_action.Count;i++)
      {
         if (list_action[i].id == action_id)
         {
            Actions action = list_action[i];
            return action;
         }  
      }
      throw new NotFoundException("not found action in file YAMl");
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
           throw new NotFoundException($"not found status {status}");
        }

    }

   public void execute_action(Actions actions, IWebDriver driver, WebDriverWait wait)
   {
      List<ActionElements> list_action_elements = actions.action_elements;
      for (int i = 0; i < list_action_elements.Count; i++)
      {
         ActionElements action_element = list_action_elements[i];
         Element ele = action_element.element;
         Locator loc = ele.locators[0];
         By by = getBy(loc);
         action_type(action_element, by, driver, wait);
      }
   }

   public void action_type(ActionElements action_element, By by, IWebDriver driver, WebDriverWait wait)
   {
      string type = action_element.input_type;
      wait = check_time_out(action_element, wait, driver);
      switch (type)
      {
         case "text":
            checkStatus(by, "DISPLAYED", wait);
            driver.FindElement(by).SendKeys(action_element.info_type);
            break;
         case "click":
            checkStatus(by, "ENABLED", wait);
            driver.FindElement(by).Click();
            break;
         default:
            throw new NotFoundException($"not found input type {type}");
      }
   }

   public WebDriverWait check_time_out(ActionElements action_element, WebDriverWait wait, IWebDriver driver)
   {
      try
      {
          long timeout = action_element.timeout;
          wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));
          return wait;
      }
      catch (Exception e)
      {
         return wait;
      }
      
   }
   
   public void execute_action_with_override(Actions actions, IWebDriver driver, WebDriverWait wait, Table table)
   {
      List<ActionElements> list_action_elements = actions.action_elements;
      for (int i = 0; i < list_action_elements.Count; i++)
      {
         ActionElements action_element = list_action_elements[i];
         Element ele = action_element.element;
         foreach (TableRow row in table.Rows)
         {
            if (ele.id == row["Field"])
            {
               action_element.info_type = row["Value"];
               break;
            }
         }
         Locator loc = ele.locators[0];
         By by = getBy(loc);
         action_type(action_element, by, driver, wait);
      }
   }

}