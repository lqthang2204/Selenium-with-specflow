using YamlDotNet.RepresentationModel;

namespace SpecflowDemo.support;

public class ManageLocator
{
   public void readFileYAML()
   {
      String data = File.ReadAllText("E:\\SpecFlow\\SpecflowDemo\\SpecflowDemo\\Resource\\LoginPage.yaml");
      using var sr = new StringReader(data);
      var yaml = new YamlStream();
      yaml.Load(sr);
      int n = yaml.Documents.Count;
      for (int i=0;i<n;i++)
      {
         var root = (YamlMappingNode)yaml.Documents[i].RootNode;
         foreach (var e in root.Children)
         {
            Console.WriteLine($"{e.Key} {e.Value}");
         }
         Console.Write("---------------------------------------------------");
      }

   }

   
   public void Main(string[] args)
   {
      ManageLocator test = new ManageLocator();
      test.readFileYAML();
   }
    
}