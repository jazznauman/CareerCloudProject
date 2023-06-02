
using System.Configuration;
using System.IO;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace Library.Framework

{
    public class Settings

    {  private readonly JObject? AppSettings;
       

        //  public string connectionsString=> AppSettings["ConnectionStrings"]["DataConnection"].ToString() ?? string.Empty; 
    public Settings()
        {
    
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
    
            string json = File.ReadAllText(path);
   
            AppSettings = Newtonsoft.Json.Linq.JObject.Parse(json);
       
   
        }
        public  string connectionString
        {
            get
            {
                return AppSettings!["ConnectionStrings"]!["DataConnection"]!.ToString();
            }
        }


    }

}
